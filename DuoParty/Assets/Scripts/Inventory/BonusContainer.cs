using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusContainer : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    public bool hasItem;
    public TypeOfBonus bonus;
    public Case caseRef;
    public int number = 0;
    [SerializeField] private TextMeshProUGUI numberText;

    private void Start()
    {
        itemImage.color = Color.grey;
        updateText();
    }

    public void addItem()
    {
        itemImage.color = Color.white;
        hasItem = true;
        number++;
        updateText();
    }

    public void removeItem()
    {
        itemImage.color = Color.grey;
        number--;
        updateText();
        if (number == 0)
        {
            hasItem = false;
        }
    }

    private void updateText()
    {
        numberText.text = number.ToString();
    }

}

public enum TypeOfBonus
{
    none,
    hammer,
    accessCard,
}
