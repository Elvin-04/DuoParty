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

    [SerializeField] private InventoryManager player1inventory;
    [SerializeField] private InventoryManager player2inventory;

    private BonusContainer bonusContainer;

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
                if (result.tag == "Card")
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        cardHand.GetComponent<Hand>().TrunCardLeft();
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        cardHand.GetComponent<Hand>().TrunCardRight();
                    }
                }
            }

            // dragging card
            if ( Input.GetMouseButtonDown(0) && ((result.tag == "Card" && result.GetComponentInParent<Hand>().card != null) || (result.tag == "BonusSlot" && result.GetComponentInParent<BonusContainer>().hasItem)) /*&& !draging*/)
            {
                draging = true;
                cardHand = result.transform.parent.gameObject;
                if(result.tag == "BonusSlot")
                {
                    bonusContainer = result.GetComponentInParent<BonusContainer>();
                }
            }
            else if (draging && Input.GetMouseButtonUp(0) && (result.tag == "Card" || result.tag == "BonusSlot")) 
            {
                draging = false;
                // discard a card
                if (results.Count > 1 && results[1].gameObject.tag == "Trash")
                {
                    switch (cardHand.gameObject.tag)
                    {
                        case ("Player1"):
                        {
                            greenTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card, cardHand.GetComponent<Hand>());
                            cardHand.GetComponent<Hand>().RemoveCard();
                            break;
                        }
                        case ("Player2"):
                        {
                            redTrashHand.GetComponent<Hand>().AddCard(cardHand.GetComponent<Hand>().card, cardHand.GetComponent<Hand>());
                            cardHand.GetComponent<Hand>().RemoveCard();
                            break;
                        }
                    }
                    stopAll.StopAllCards();
                }

                // place card in game
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (result.tag == "Card" && hit.collider != null && hit.collider.TryGetComponent<Case>(out Case _case) && _case.GetInteractible() && hit.collider.gameObject.GetComponent<Case>().GetCard() == null
                    && !_case.isKey && !_case.isVaccineGreen && !_case.isVaccineRed)
                    {
                        if (_case.isHammer || _case.isAccessCard)
                        {
                            AddBonusToPlayer(cardHand.gameObject.tag, _case);
                        }
                        hit.collider.gameObject.GetComponent<Case>().AddCard(cardHand.GetComponent<Hand>().card);
                        hit.collider.gameObject.transform.Rotate(0f, 0f, cardHand.GetComponent<Hand>().rotation);
                        cardHand.GetComponent<Hand>().RemoveCard();
                        stopAll.StopAllCards();
                    }
                    else if (result.tag == "BonusSlot" && hit.collider != null && hit.collider.TryGetComponent<Case>(out _case) && _case.GetInteractible() && (_case.GetCard() != null || _case.GetArmouredDoor()))
                    {
                        if (_case.GetCard() != null && bonusContainer.bonus == TypeOfBonus.hammer)
                        {
                            _case.ResetCard();
                            bonusContainer.removeItem();
                            print("ca marsh");
                        }
                        else if (_case.GetArmouredDoor() && bonusContainer.bonus == TypeOfBonus.accessCard)
                        {
                            _case.isArmouredDoor = false;
                            bonusContainer.removeItem();
                            print("sa marche aussi");
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


    private void AddBonusToPlayer(string tag, Case currentCase)
    {
        switch(tag)
        {
            case ("Player1"):
                player1inventory.AddItemInInventory(currentCase.gameObject);
                break;

            case ("Player2"):
                player2inventory.AddItemInInventory(currentCase.gameObject);
                break;

        }
    }

}
