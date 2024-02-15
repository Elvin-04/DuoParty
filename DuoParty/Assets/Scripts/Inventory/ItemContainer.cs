using UnityEditor.U2D;
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


    private void Start()
    {
        itemImage.color = Color.grey;
    }


    public void addItem()
    {
        if (!hasItem)
        {
            itemImage.color = Color.white;
        }
        hasItem = true;
    }

    
}
