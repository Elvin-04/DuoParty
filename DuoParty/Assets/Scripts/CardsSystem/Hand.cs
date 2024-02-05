using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Cards card;
    public Image cardImage;
    public bool isDefundHand;
    [SerializeField] private GameObject defundHand;

    private void Start()
    {
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0f);
    }

    public void AddCard(Cards newCard)
    {
        if (isDefundHand && card != null)
        {
            defundHand.GetComponent<DefundHand>().AddDefundCard(card);
        }
        card = newCard;
        cardImage.sprite = newCard.cardImage;
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 255f);
    }

    public void RemoveCard()
    {
        card = null;
        cardImage.sprite = null;
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0f);
        if (isDefundHand && defundHand.GetComponent<DefundHand>().defundDeckCard.Count > 0)
        {
            AddCard(defundHand.GetComponent<DefundHand>().RemoveDefundCard());
        }
    }

    public void PlayCard()
    {
        // add code to play card here
        RemoveCard();
    }
}
