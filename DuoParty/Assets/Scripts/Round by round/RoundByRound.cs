using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoundByRound : MonoBehaviour
{
    [SerializeField] private Transform playerOne;
    [SerializeField] private Transform playerTwo;

    [SerializeField] private Color grayColor;

    private List<SpriteRenderer> playerOneSprites;
    private List<SpriteRenderer> playerTwoSprites;

    public static RoundByRound instance {  get; private set; }

    //Turn 0 = player 1
    //Turn 1 = player 2
    public int turn {  get; private set; }

    private void Start()
    {
        instance = this;
        turn = 0;
        playerOneSprites = playerOne.GetComponentsInChildren<SpriteRenderer>().ToList();
        playerTwoSprites = playerTwo.GetComponentsInChildren<SpriteRenderer>().ToList();

        SwitchTurn();
    }

    private void Update()
    {
        //Temp (test)
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchTurn();
        }
        //////////////////////////
    }


    //Next player's turn
    private void SwitchTurn()
    {
        //TODO : change isInterractible of the cards
        if(turn == 0) 
        {
            turn = 1;
            foreach (var sprite in playerOneSprites)
            {
                sprite.color = Color.white;
            }
            foreach (var sprite in playerTwoSprites)
            {
                sprite.color = grayColor;
            }
        }
        else
        {
            turn = 0;
            foreach (var sprite in playerTwoSprites)
            {
                sprite.color = Color.white;
            }
            foreach (var sprite in playerOneSprites)
            {
                sprite.color = grayColor;
            }
        }
    }
}
