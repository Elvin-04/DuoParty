using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Cards card;
    public Image cardImage;

    private void Start()
    {
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0f);
    }

    public void AddCard(Cards newCard)
    {
        card = newCard;
        cardImage.sprite = newCard.cardImage;
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 255f);
    }

    public void RemoveCard()
    {
        card = null;
        cardImage.sprite = null;
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0f);
    }

    public void PlayCard()
    {
        // add code to play card here
        RemoveCard();
    }
}
