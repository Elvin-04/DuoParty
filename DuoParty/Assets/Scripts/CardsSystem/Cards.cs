using UnityEngine;


[CreateAssetMenu]
public class Cards : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public cardTypes cardType;
    public cardcolors cardColor;

}

public enum cardTypes
{
    corridor = 0,
    corner = 1,
    tPath = 2,
    cross = 3,

}

public enum cardcolors
{
    red = 0,
    green = 1,

}