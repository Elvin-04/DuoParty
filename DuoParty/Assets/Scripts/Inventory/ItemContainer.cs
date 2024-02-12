using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public bool isKeysContenair;
    public bool hasItem;
    [SerializeField] private Sprite KeySprite;

    public void addItem(GameObject currentCase)
    {
        if (isKeysContenair)
        {
            itemImage.sprite = KeySprite;
        }
        else
        {
            itemImage.sprite = currentCase.GetComponent<SpriteRenderer>().sprite;
        }
        hasItem = true;
    }

    
}
