using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Cards> deckCard;
    [SerializeField] private Cards corridorCard;
    [SerializeField] private Cards cornerCard;
    [SerializeField] private Cards tPathCard;
    [SerializeField] private Cards TwoColorsCross;
    [SerializeField] private Cards TwoCorners;
    private int corridorLeft = 13;
    private int cornerLeft = 8;
    private int tPathLeft = 9;
    private int doubleCornerLeft = 3;
    private int twoColorsCrossLeft = 3;
    private int cardNumber;
    [SerializeField] private Hand hand;


    void Start()
    {
        cardNumber = corridorLeft + cornerLeft + tPathLeft + doubleCornerLeft + twoColorsCrossLeft - 1;
        
        //add all cards to the player deck
        for (int i = cardNumber; i > -1; i--)
        {
            int randomFactor = Random.Range(0, i);
            // add corridor
            if (randomFactor >= 0 && randomFactor < corridorLeft && corridorLeft > 0)
            {
                deckCard[i] = corridorCard;
                corridorLeft--;

            }
            // add corner
            else if ((randomFactor > corridorLeft &&  randomFactor < (corridorLeft + cornerLeft) && cornerLeft > 0)
                || (corridorLeft <= 0 && tPathLeft <= 0 && cornerLeft > 0))
            {
                deckCard[i] = cornerCard;
                cornerLeft--;
            }
            // add double coner 
            else if (randomFactor > (corridorLeft + cornerLeft) && randomFactor < (corridorLeft + cornerLeft + doubleCornerLeft) && doubleCornerLeft > 0)
            {
                deckCard[i] = TwoColorsCross;
                doubleCornerLeft--;
            }
            // add two colors cross
            else if (randomFactor > (corridorLeft + cornerLeft + doubleCornerLeft) && randomFactor < (corridorLeft + cornerLeft + doubleCornerLeft + twoColorsCrossLeft) && twoColorsCrossLeft > 0)
            {
                deckCard[i] = TwoCorners;
                twoColorsCrossLeft--;
            }
            // add T path
            
            else
            {
                deckCard[i] = tPathCard;
                tPathLeft--;
            }
        }
        PullCard();
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
