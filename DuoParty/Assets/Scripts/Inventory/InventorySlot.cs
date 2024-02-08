using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragItem DragItem = dropped.GetComponent<DragItem>();
        DragItem._parentAfterDrag = transform;
    }

}
