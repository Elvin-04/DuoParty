using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] string _coordinatesLength;
    [SerializeField] string _coordinatesWidth;

    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private bool isKey;
    [SerializeField] private bool isAccessCard;
    [SerializeField] private bool isHammer;
    [SerializeField] private bool isBomb;
    [SerializeField] private bool isArmouredDoor;

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

    public bool GetKey()
    {
        return isKey;
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
}
