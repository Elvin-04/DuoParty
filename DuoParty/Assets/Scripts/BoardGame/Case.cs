using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private Cards card;
    [SerializeField] private string color;

    public bool GetInteractible()
    {
        return isInteractible;
    }

    public bool GetSpawn()
    {
        return isSpawn;
    }

    public bool GetEnd()
    {
        return isEnd;
    }

    public string GetColor()
    {
        return color;
    }

    public Cards GetCard()
    {
        return card;
    }

    public void AddCard(Cards _card)
    {
        card = _card;
        gameObject.GetComponent<SpriteRenderer>().sprite = card.cardImage;
        if (card.cardColor == cardcolors.red)
        { color = "Red"; }
        if (card.cardColor == cardcolors.green)
        { color = "Green"; }
        if(card.cardColor == cardcolors.redAndGreen)
        {
            
        }
    }

}
