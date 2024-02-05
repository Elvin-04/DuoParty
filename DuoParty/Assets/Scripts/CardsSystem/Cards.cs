using UnityEngine;


[CreateAssetMenu]
public class Cards : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public cardTypes cardType;
    public cardcolors cardColor;
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    private void Awake()
    {
        switch (cardType)
        {
            case (cardTypes.corridor):
                {
                    canMoveUp = true;
                    canMoveDown = true;
                    canMoveLeft = false;
                    canMoveRight = false;
                    break;
                }
            case (cardTypes.corner):
                {
                    canMoveUp = false;
                    canMoveDown = true;
                    canMoveLeft = false;
                    canMoveRight = true;
                    break;
                }
            case (cardTypes.tPath):
                {
                    canMoveUp = true;
                    canMoveDown = true;
                    canMoveLeft = false;
                    canMoveRight = true;
                    break;
                }
            case (cardTypes.cross):
                {
                    canMoveUp = true;
                    canMoveDown = true;
                    canMoveLeft = true;
                    canMoveRight = true;
                    break;
                }
        }
        
    }

    public void TurnRight()
    {
        bool temp = canMoveUp;
        canMoveUp = canMoveLeft;
        canMoveLeft = canMoveDown;
        canMoveDown = canMoveRight;
        canMoveRight = temp;
    }

    public void TurnLeft()
    {
        bool temp = canMoveUp;
        canMoveUp = canMoveRight;
        canMoveRight = canMoveDown;
        canMoveDown = canMoveLeft;
        canMoveLeft = temp;
    }

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
    redAndGreen = 2,
    none = 3,

}