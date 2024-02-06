using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Cards card;
    public Image cardImage;
    public bool isDefundHand;
    public float rotation;
    [SerializeField] public GameObject defundHand;
    [SerializeField] private Deck deck;


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
        rotation = 0f;
        card = null;
        cardImage.sprite = null;
        cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0f);
        cardImage.transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, 0f));
        if (isDefundHand)
        {
            if (defundHand.GetComponent<DefundHand>().defundDeckCard.Count > 0)
                AddCard(defundHand.GetComponent<DefundHand>().RemoveDefundCard());
        }
        else
            deck.PullCard();
    }
      /*****************************************/
     /*              turn cards               */
    /*****************************************/
    public void TrunCardRight()
    {
        if (card != null)
        {
            card.TurnRight();
            rotation = (rotation == 0f ? 270f : rotation -= 90f);
            cardImage.transform.Rotate(0f, 0f, -90f);
        }
        
    }

    public void TrunCardLeft()
    {
        if (card != null)
        {
            card.TurnLeft();
            rotation = (rotation == 270 ? 0f : rotation += 90f);
            cardImage.transform.Rotate(0f, 0f, 90f);
        }

    }

      /*****************************************/
     /*              turn cards               */
    /*****************************************/

    public void PlayCard()
    {
        RemoveCard();
    }
}
