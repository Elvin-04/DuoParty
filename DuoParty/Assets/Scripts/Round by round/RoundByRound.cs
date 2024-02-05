using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundByRound : MonoBehaviour
{

    [SerializeField] private Color grayColor;

    [SerializeField] private List<Transform> playerOneSprites;
    [SerializeField] private List<Transform> playerTwoSprites;

    public static RoundByRound instance {  get; private set; }

    //Turn 0 = player 1
    //Turn 1 = player 2
    public int turn {  get; private set; }

    private void Start()
    {
        instance = this;
        turn = 0;

        SwitchTurn();
    }


    //Next player's turn
    public void SwitchTurn()
    {
        //TODO : change isInterractible of the cards
        if(turn == 0) 
        {
            turn = 1;
            foreach (var sprite in playerOneSprites)
            {
                ChangeColor(sprite, Color.white);
            }
            foreach (var sprite in playerTwoSprites)
            {
                ChangeColor(sprite, grayColor);
            }
        }
        else
        {
            turn = 0;
            foreach (var sprite in playerTwoSprites)
            {
                ChangeColor(sprite, Color.white);
            }
            foreach (var sprite in playerOneSprites)
            {
                ChangeColor(sprite, grayColor);
            }
        }
    }

    public void StopAllCardsPlays()
    {
        foreach (var sprite in playerOneSprites)
        {
            ChangeColor(sprite, grayColor);
        }
        foreach (var sprite in playerTwoSprites)
        {
            ChangeColor(sprite, grayColor);
        }
    }

    private void ChangeColor(Transform obj,  Color color)
    {
        if(obj.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
        {
            sprite.color = color;
        }
        else if(obj.TryGetComponent<Image>(out Image img) && img.color.a != 0)
        {
            img.color = color;

            if (color == Color.white)
                img.raycastTarget = true;
            else
                img.raycastTarget = false;

        }

        if(obj.TryGetComponent<Button>(out Button button))
        {
            if (color == Color.white)
                button.interactable = true;
            else
                button.interactable = false;
        }
    }

}
