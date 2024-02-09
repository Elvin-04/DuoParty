using UnityEngine;
using UnityEngine.UI;

public class BonusContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public bool hasItem;
    public TypeOfBonus bonus = TypeOfBonus.none;
    public Case caseRef;
    private Image defaultSprite;

    private void Start()
    {
        defaultSprite = GetComponent<Image>();
    }

    public void addItem(GameObject currentCase)
    {
        hasItem = true;

        if (currentCase.GetComponent<Case>().isHammer)
        {
            bonus = TypeOfBonus.hammer;
            itemImage.sprite = caseRef.GetHammerSprite();
        }
        else if(currentCase.GetComponent<Case>().isAccessCard)
        {
            bonus = TypeOfBonus.accessCard;
            itemImage.sprite= caseRef.GetAccessCardSprite();
        }
            
    }

    public void removeItem()
    {
        itemImage.sprite = defaultSprite.sprite;
        hasItem = false;
    }


}

public enum TypeOfBonus
{
    none,
    hammer,
    accessCard,
}
