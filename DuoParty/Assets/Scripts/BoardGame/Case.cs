using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private string color;
    public Cards card;

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
    [SerializeField] private Sprite doorTraps;

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
        isSpawn = true;
        isInteractible = false;
    }
    public void SetSpawnRed()
    {
        GetComponent<SpriteRenderer>().sprite = spawnSpriteRed;
        SetRedPath();
        color = "Red";
        isSpawn = true;
        isInteractible = false;
    }
    public void SetEndGreen()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteGreen;
        SetGreenPath();
        color = "Green";
    }
    public void SetEndRed()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteRed;
        SetRedPath();
        color = "Red";
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
}

[System.Serializable]
public struct Path
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
}
    
