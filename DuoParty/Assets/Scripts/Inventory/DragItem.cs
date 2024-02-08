using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Image;
    public Vector3 _oldPosition;
    [HideInInspector] public Transform _parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("StartDrag");
        _parentAfterDrag = transform.parent;
        _oldPosition = transform.position;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_parentAfterDrag.tag == Image.tag && _parentAfterDrag.gameObject.layer == Image.gameObject.layer)
        {
            Debug.Log("EndDrag");
            transform.SetParent(_parentAfterDrag);
            transform.position = transform.parent.position;
            Image.raycastTarget = true;
        }
        else
        {
            Debug.Log("EndDrag");
            transform.position = _oldPosition;
            Image.raycastTarget = true;
        }
    }
}
