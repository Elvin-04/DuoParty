using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragnDrop : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GraphicRaycaster m_Raycaster;
    private bool draging = false;
    [SerializeField] private GameObject DragNDrop;
    [SerializeField] private GameObject cardHand;
    [SerializeField] private GameObject redTrashHand;
    [SerializeField] private GameObject greenTrashHand;
    [SerializeField] private OneCardPerRound stopAll;

    [SerializeField] private Sprite porte_blinde;

    private void Update()
    {
        //get mouse pos
        Vector3 mousePos = Input.mousePosition;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = mousePos;

        DragNDrop.transform.position = mousePos;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerEventData, results);

        // if the mouse is in an element
        if (results.Count > 0)
        {
            GameObject result = results[0].gameObject;

            // drag and drop
            if (draging)
            {
                result.transform.SetParent(DragNDrop.transform);
                DragNDrop.transform.position = mousePos;
                // rotate dragged card
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    cardHand.GetComponent<Hand>().TrunCardLeft();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    cardHand.GetComponent<Hand>().TrunCardRight();
                }
            }

            // stop drag and drop
            else if (!draging && cardHand != null && result.name == cardHand.GetComponent<Hand>().cardImage.name)
            {
                result.transform.SetParent(cardHand.transform);
            }
            // dragging card
            if (result.tag == "Card" && Input.GetMouseButtonDown(0) && result.GetComponentInParent<Hand>().card != null && !draging)
            {
                draging = true;
                cardHand = result.transform.parent.gameObject;
            }
            else if (draging && Input.GetMouseButtonUp(0) && result.tag == "Card") 
            {
                draging = false;
                // discard a card
                if (results.Count > 1 && results[1].gameObject.tag == "Trash")
                {
                    PlaceInSecondHand();
                    stopAll.StopAllCards();
                }

                // place card in game
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    //drop the card
                    if (hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && _case.GetInteractible() && hit.collider.gameObject.GetComponent<Case>().GetCard() == null
                        && !_case.isKey && !_case.isVaccineGreen && !_case.isVaccineRed)
                    {
                        if (_case.isArmouredDoor)
                        {
                            PlaceInSecondHand();
                            _case.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                            _case.LockMovements();
                        }
                        else if (_case.isBomb)
                        {
                            PlaceInSecondHand();
                            _case.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                            _case.LockMovements();
                            if(_case.up.GetCard() == null)
                            {
                                _case.up.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                                _case.up.LockMovements();
                            }
                            if (_case.down.GetCard() == null)
                            {
                                _case.down.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                                _case.down.LockMovements();
                            }
                            if (_case.right.GetCard() == null)
                            {
                                _case.right.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                                _case.right.LockMovements();
                            }
                            if (_case.left.GetCard() == null)
                            {
                                _case.left.GetComponent<SpriteRenderer>().sprite = porte_blinde;
                                _case.left.LockMovements();
                            }

                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<Case>().AddCard(cardHand.GetComponent<Hand>().card);
                            hit.collider.gameObject.transform.Rotate(0f, 0f, cardHand.GetComponent<Hand>().rotation);
                            cardHand.GetComponent<Hand>().RemoveCard();
                            stopAll.StopAllCards();
                        }
                    }
                }
                // return image in his place
                CardReturn(result.gameObject);
            }
        }
    }


    private void CardReturn(GameObject card)
    {
        card.transform.SetParent(cardHand.transform);
        card.transform.position = new Vector2(cardHand.transform.position.x, cardHand.transform.position.y);
    }

    private void PlaceInSecondHand()
    {
        switch (cardHand.gameObject.tag)
        {
            case ("Player1"):
                {
                    greenTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card);
                    cardHand.GetComponent<Hand>().RemoveCard();
                    break;
                }
            case ("Player2"):
                {
                    redTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card);
                    cardHand.GetComponent<Hand>().RemoveCard();
                    break;
                }
        }
    }
}
