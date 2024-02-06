using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private Cards card;
    [SerializeField] private cardcolors color;
    [SerializeField] private Case up;
    [SerializeField] private Case right;
    [SerializeField] private Case down;
    [SerializeField] private Case left;
    public List<Case> cases;

    private void Start()
    {

        RaycastHit2D hit;

        for (int i = -1; i < 2; i += 2)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, i), 1.5f);
            if (hit && hit.collider.gameObject.GetComponent<Case>())
            {
                if(i == -1)
                    down = hit.collider.gameObject.GetComponent<Case>();
                else
                    up = hit.collider.gameObject.GetComponent<Case>();
            }
        }

        for (int i = -1; i < 2; i += 2)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(i, 0), 1.5f);
            if (hit && hit.collider.gameObject.GetComponent<Case>())
            {
                if (i == -1)
                    left = hit.collider.gameObject.GetComponent<Case>();
                else
                    right = hit.collider.gameObject.GetComponent<Case>();
            }
        }


        cases.Add(up);
        cases.Add(right);
        cases.Add(down);
        cases.Add(left); 
        
    }

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

    public cardcolors GetColor()
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
    }
    
}
