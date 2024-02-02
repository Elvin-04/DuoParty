using System.Collections.Generic;
using UnityEngine;

public class DefundHand : MonoBehaviour
{
    public List<Cards> defundDeckCard;

    public void AddDefundCard(Cards card)
    {
        defundDeckCard.Add(card);
    }

    public Cards RemoveDefundCard()
    {
        Cards card = defundDeckCard[defundDeckCard.Count - 1];
        defundDeckCard.RemoveAt(defundDeckCard.Count - 1);
        return card;
    }
}