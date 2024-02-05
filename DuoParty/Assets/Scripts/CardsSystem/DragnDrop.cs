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

    private void Update()
    {
        //get mouse pos
        Vector3 MousePos = Input.mousePosition;
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = MousePos;

        DragNDrop.transform.position = MousePos;

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
                DragNDrop.transform.position = MousePos;
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
                    switch(cardHand.gameObject.tag)
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

                CardReturn(result.gameObject);
            }
        }
    }

    private void CardReturn(GameObject card)
    {
        card.transform.SetParent(cardHand.transform);
        card.transform.position = new Vector2(cardHand.transform.position.x, cardHand.transform.position.y);
    }

}
