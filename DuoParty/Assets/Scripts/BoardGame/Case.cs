using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    [SerializeField] bool isInteractible = true;

    public bool isSpawn;
    public bool isEnd;
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

    [SerializeField] private string color;
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
        GetComponent<SpriteRenderer>().sprite = keySprite;
    }

    public void SetVaccineRed()
    {
        isVaccineRed = true;
        GetComponent<SpriteRenderer>().sprite = vaccineSpriteRed;
    }
    public void SetVaccineGreen()
    {
        isVaccineGreen = true;
        GetComponent<SpriteRenderer>().sprite = vaccineSpriteGreen;
    }
    public void SetSpawnGreen()
    {
        GetComponent<SpriteRenderer>().sprite = spawnSpriteGreen;
        color = "Green";
        isSpawn = true;
    }
    public void SetSpawnRed()
    {
        GetComponent<SpriteRenderer>().sprite = spawnSpriteRed;
        color = "Red";
        isSpawn = true;
    }
    public void SetEndGreen()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteGreen;
        color = "Green";
    }
    public void SetEndRed()
    {
        GetComponent<SpriteRenderer>().sprite = endSpriteRed;
        color = "Red";
    }
}
