using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private Cards card;
    [SerializeField] private cardcolors eColor;
    [SerializeField] public Case up;
    [SerializeField] public Case right;
    [SerializeField] public Case down;
    [SerializeField] public Case left;
    public List<Case> cases;

    [SerializeField] private string color;

    public Path greenPath;
    public Path redPath;

    [Header("For the pathfinding")]
    public int x;
    public int y;
    public Case parent;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get { return gCost + hCost; }
    }

    private void Start()
    {

        RaycastHit2D hit;
        Physics2D.queriesStartInColliders = false;

        for (int i = -1; i < 2; i += 2)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, i), 1.5f);
            if (hit && hit.collider.gameObject.GetComponent<Case>())
            {
                if (i == -1)
                    down = hit.collider.gameObject.GetComponent<Case>();
                else
                    up = hit.collider.gameObject.GetComponent<Case>();
            }
            hit = Physics2D.Raycast(transform.position, new Vector2(i, 0), 1.5f);
            if (hit && hit.collider.gameObject.GetComponent<Case>())
            {
                if (i == -1)
                    left = hit.collider.gameObject.GetComponent<Case>();
                else
                    right = hit.collider.gameObject.GetComponent<Case>();
            }
        }

        Physics2D.queriesStartInColliders = true;
        cases.Add(up);
        cases.Add(right);
        cases.Add(down);
        cases.Add(left);
    }
    

    public Path GetPathByColor(string color)
    {
        switch (color)
        {
            case "Green":
                return greenPath;
            case "Red":
                return redPath;
            default:
                return greenPath;
        }
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

    public string GetColor()
    {
        return color;
    }

    public cardcolors GetEColor()
    {
        return eColor;
    }

    public Cards GetCard()
    {
        return card;
    }

    public void AddCard(Cards _card)
    {
        card = _card;

        gameObject.GetComponent<SpriteRenderer>().sprite = card.cardImage;

        greenPath = card.instantiatedGreenPath; 
        redPath = card.instantiatedRedPath;
        _card.ResetRotation();

        if (card.cardColor == cardcolors.red)
        { color = "Red"; }
        if (card.cardColor == cardcolors.green)
        { color = "Green"; }
        if(card.cardColor == cardcolors.redAndGreen)
        { color = "RedAndGreen"; }
    }


}

[System.Serializable]
public struct Path
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;



    public void PrintPaths()
    {
        Debug.Log("up " + canMoveUp);
        Debug.Log("right " + canMoveRight);
        Debug.Log("down " + canMoveDown);
        Debug.Log("left " + canMoveLeft);
    }
}
