using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Cards> deckCard;
    [SerializeField] private Cards corridorCard;
    [SerializeField] private Cards cornerCard;
    [SerializeField] private Cards tPathCard;
    private int corriderLeft = 13;
    private int cornerLeft = 8;
    private int tPathLeft = 9;
    private int cardNumber;
    [SerializeField] private Hand hand;


    void Start()
    {
        cardNumber = corriderLeft + cornerLeft + tPathLeft - 1;
        
        //add all cards to the player deck
        for (int i = cardNumber; i > -1; i--)
        {
            int randomFactor = Random.Range(0, i);
            if (randomFactor >= 0 && randomFactor < corriderLeft && corriderLeft > 0)
            {
                deckCard[i] = corridorCard;
                corriderLeft--;

            }
            else if ((randomFactor > corriderLeft &&  randomFactor < (corriderLeft + cornerLeft) && cornerLeft > 0)
                || (corriderLeft <= 0 && tPathLeft <= 0 && cornerLeft > 0))
            {
                deckCard[i] = cornerCard;
                cornerLeft--;
            }
            else
            {
                deckCard[i] = tPathCard;
                tPathLeft--;
            }
        }
    }

    public void PullCard()
    {
        if (deckCard.Count > 0 && hand.card == null)
        {
            hand.AddCard(deckCard[deckCard.Count - 1]);
            
            deckCard[deckCard.Count - 1] = null;
            deckCard.RemoveAt(deckCard.Count - 1);
        }
    }
}
