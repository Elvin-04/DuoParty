using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public bool isKeysContenair;
    public bool hasItem;

    public void addItem(GameObject currentCase)
    {
        itemImage.sprite = currentCase.GetComponent<SpriteRenderer>().sprite;
        hasItem = true;
    }
}
