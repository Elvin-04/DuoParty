using System.Collections.Generic;
using Unity.VisualScripting;
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
            else if (!draging && cardHand != null)
            {
                result.transform.SetParent(cardHand.transform);
            }
            // dragging card
            if (result.tag == "Card" && Input.GetMouseButtonDown(0) && result.GetComponentInParent<Hand>().card != null)
            {
                draging = true;
                cardHand = result.transform.parent.gameObject;
            }
            else if (draging && Input.GetMouseButtonUp(0)) 
            {
                draging = false;
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
