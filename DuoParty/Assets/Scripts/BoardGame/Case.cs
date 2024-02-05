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
    public bool isVaccine;
    public bool isAccessCard;
    public bool isHammer;
    public bool isBomb;
    public bool isArmouredDoor;

    [SerializeField] private Sprite keySprite;
    [SerializeField] private Sprite vaccineSprite;

    [SerializeField] private string color;
    public bool IsEmpty()
    {
        if(!isSpawn && !isVaccine && !isEnd && !isKey && !isAccessCard && !isHammer && !isBomb && !isArmouredDoor)
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
    public bool GetVaccine()
    {
        return isVaccine;
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

    public void Setkey()
    {
        isKey = true;
        GetComponent<SpriteRenderer>().sprite = keySprite;
    }

    public void Setvaccine()
    {
        isKey = true;
        GetComponent<SpriteRenderer>().sprite = vaccineSprite;
    }
}
