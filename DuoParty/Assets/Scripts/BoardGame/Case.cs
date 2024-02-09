using System;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private Cards card;
    [SerializeField] private cardcolors eColor;
    [SerializeField] public Case up;
    [SerializeField] public Case right;
    [SerializeField] public Case down;
    [SerializeField] public Case left;
    private Sprite defaultSprite;
    public List<Case> cases;

    [SerializeField] private string color;

    public Path greenPath;
    public Path redPath;

    public bool isKey;
    public bool isVaccineRed;
    public bool isVaccineGreen;
    public bool isAccessCard;
    public bool isHammer;
    public bool isBomb;
    public bool isArmouredDoor;

    [SerializeField] private Sprite keySprite;
    [SerializeField] private Sprite vaccineSpriteRed;
    [SerializeField] private Sprite vaccineSpriteGreen;
    [SerializeField] private Sprite spawnSpriteGreen;
    [SerializeField] private Sprite spawnSpriteRed;
    [SerializeField] private Sprite endSpriteGreen;
    [SerializeField] private Sprite endSpriteRed;
    [SerializeField] private Sprite hammerSprite;
    [SerializeField] private Sprite accessCardSprite;

    [SerializeField] private Cards neutralCross;
    [SerializeField] private Cards redCross;
    [SerializeField] private Cards greenCross;

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

    public Sprite GetHammerSprite()
    {
        return hammerSprite;
    }

    public Sprite GetAccessCardSprite()
    {
        return accessCardSprite;
    }

    private void Start()
    {

        defaultSprite = GetComponent<SpriteRenderer>().sprite;

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
    

    
    public bool IsEmpty()
    {
        if (!isSpawn && !isVaccineRed && !isVaccineGreen && !isEnd && !isKey && !isAccessCard && !isHammer && !isBomb && !isArmouredDoor)
        {
            return true;
        }
        else
        {
            return false;
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

    public void RemoveBonus()
    {
        if(isVaccineGreen)
        {
            AddCard(greenCross);
            isVaccineGreen = false;
        }
        else if (isVaccineRed)
        {
            AddCard(redCross);
            isVaccineRed = false;
        }
        else if (isKey)
        {
            AddCard(neutralCross);
            isKey = false;
        }
    }
    
    public bool GetKey()
    {
        return isKey;
    }
    public bool GetVaccineGreen()
    {
        return isVaccineGreen;
    }
    public bool GetVaccineRed()
    {
        return isVaccineRed;
    }
    public bool GetAccessCard()
    {
        return isAccessCard;
    }
    public bool GetHammer()
    {
        return isHammer;
    }
    public bool GetBomb()
    {
        return isBomb;
    }
    public bool GetArmouredDoor()
    {
        return isArmouredDoor;
    }

    public string GetCoordinatesLength()
    {
        return _coordinatesLength;
    }

    public string GetCoordinatesWidth()
    {
        return _coordinatesWidth;
    }

    public void SetKey()
    {
        isKey = true;
        SetBothPath();
        GetComponent<SpriteRenderer>().sprite = keySprite;
    }

    public void SetVaccineRed()
    {
        isVaccineRed = true;
        SetRedPath();
        GetComponent<SpriteRenderer>().sprite = vaccineSpriteRed;
    }
    public void SetVaccineGreen()
    {
        isVaccineGreen = true;
        SetGreenPath();
        GetComponent<SpriteRenderer>().sprite = vaccineSpriteGreen;
    }
    public void SetSpawnGreen()
    {
        GetComponent<SpriteRenderer>().sprite = spawnSpriteGreen;
        SetGreenPath();
        color = "Green";
        eColor = cardcolors.green;
        isSpawn = true;
        isInteractible = false;
    }
    public void SetSpawnRed()
    {
        GetComponent<SpriteRenderer>().sprite = spawnSpriteRed;
        SetRedPath();
        color = "Red";
        eColor = cardcolors.red;
        isSpawn = true;
        isInteractible = false;
    }
    public void SetEndGreen()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteGreen;
        SetGreenPath();
        color = "Green";
        eColor = cardcolors.green;
        isEnd = true;
    }
    public void SetEndRed()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteRed;
        SetRedPath();
        color = "Red";
        eColor = cardcolors.red;
        isEnd = true;
    }

    private void SetRedPath()
    {
        redPath.canMoveLeft = true;
        redPath.canMoveRight = true;
        redPath.canMoveUp = true;
        redPath.canMoveDown = true;
    }

    private void SetGreenPath()
    {
        greenPath.canMoveLeft = true;
        greenPath.canMoveRight = true;
        greenPath.canMoveUp = true;
        greenPath.canMoveDown = true;
    }

    private void SetBothPath()
    {
        SetGreenPath();
        SetRedPath();
    }

    private void ResetPath()
    {
        redPath.canMoveLeft = false;
        redPath.canMoveRight = false;
        redPath.canMoveUp = false;
        redPath.canMoveDown = false;

        greenPath.canMoveLeft = false;
        greenPath.canMoveRight = false;
        greenPath.canMoveUp = false;
        greenPath.canMoveDown = false;
    }

    public void ResetCard()
    {
        card = null;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        ResetPath();
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
    
