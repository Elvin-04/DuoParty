using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public bool isKeysContenair;
    public bool hasItem;
    [SerializeField] private Sprite KeySprite;
    [SerializeField] private Sprite VaccineSpriteRed;
    [SerializeField] private Sprite VaccineSpriteGreen;
    [SerializeField] private string color;

    public void addItem(GameObject currentCase)
    {
        if (isKeysContenair)
        {
            itemImage.sprite = KeySprite;
        }
        else if(color == "Red")
        {
            itemImage.sprite = VaccineSpriteRed;
        }
        else
        {
            itemImage.sprite = VaccineSpriteGreen;
        }
        hasItem = true;
    }

    
}
