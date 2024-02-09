using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonShowArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    private void Start()
    {
        image.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("1");
        image.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("2");
        image.gameObject.SetActive(false);
    }
}
