using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image checkImage;
    public bool isKeysContenair;
    public bool hasItem;
    [SerializeField] private Sprite KeySprite;
    [SerializeField] private Sprite VaccineSpriteRed;
    [SerializeField] private Sprite VaccineSpriteGreen;
    [SerializeField] private Sprite Check;
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
            checkImage.sprite = Check;
        }
        hasItem = true;
    }

    
}
