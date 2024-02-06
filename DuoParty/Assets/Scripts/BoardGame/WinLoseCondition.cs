using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseCondition : MonoBehaviour
{
    [Header("Players Infos")]
    [SerializeField] private GameObject player1;
    [SerializeField] private Deck player1Deck;
    [SerializeField] private Hand player1DefundHand;
    [SerializeField] private Hand player1Hand;
    [SerializeField] private GameObject player2;
    [SerializeField] private Deck player2Deck;
    [SerializeField] private Hand player2DefundHand;
    [SerializeField] private Hand player2Hand;

    [Header("Other Infos")]
    [SerializeField] private GridManager grid;
    public bool player1Finish;
    public bool player2Finish;


    public void Win()
    {
        //TODO  Switch scene to win scene
        Debug.Log("WIN");
    }

    public void Lose()
    {
        Debug.Log("LOSE");
        //TODO  switch scene to game over scene
    }

    private bool PathToExitExist(cardcolors color)
    {
        Case startCase = grid.GetSpawnOfColor(color);
        Case EndCase = grid.GetEndOfColor(color);
        List<Case> nullCases = new List<Case>();
        List<Case> inVerification = new List<Case>();
        List<Case> verifiedCases = new List<Case>();
        List<Case> nextVerificationList = new List<Case>();

        // initialisation
        foreach (Case _case in startCase.cases)
        {
            if (_case == null || _case.GetCard() != null)
                nullCases.Add(_case);
            else
                inVerification.Add(_case);
        }

        // verification
         for (int i = 0; i < 15; i++)
         {
            if (inVerification.Count == 0)
            {
                print("fin");
                return false;
            }
                
            foreach (Case _case in inVerification)
            {
                foreach (Case _nextCase in _case.cases)
                {
                    Debug.Log("vérifie : " + _case);
                    if (_case.gameObject.name == EndCase.gameObject.name)
                        return true;

                    else if (_case == null || _case.GetCard() != null)
                        nullCases.Add(_case);
                    else
                        nextVerificationList.Add(_case);

                    verifiedCases.Add(_case);
                }
            }
            inVerification.Clear();

            foreach (Case _case in nextVerificationList)
            {
                bool isNotVerified = true;
                foreach (Case _case2 in verifiedCases)
                {
                    if (_case == _case2)
                    {
                        isNotVerified = false;
                    }
                }
                if (isNotVerified)
                    inVerification.Add(_case);
            }
            nextVerificationList.Clear();
         }
 

        return false;
    }



    private void Update()
    {

           /**********************************************/
          /*********** condition de victoire ************/
         /**********************************************/

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(player1.transform.position.x, player1.transform.position.y), Vector2.zero);

        if (hit1.collider != null && hit1.collider.GetComponent<Case>() && hit1.collider.GetComponent<Case>().GetEnd() && hit1.collider.GetComponent<Case>().GetColor() == cardcolors.red)
        {
            player1Finish = true;
        }
        else player1Finish = false;

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(player2.transform.position.x, player2.transform.position.y), Vector2.zero);

        if (hit2.collider != null && hit2.collider.GetComponent<Case>() && hit2.collider.GetComponent<Case>().GetEnd() && hit2.collider.GetComponent<Case>().GetColor() == cardcolors.green)
        {
            player2Finish = true;
        }
        else player2Finish = false;

        if (player1Finish && player2Finish)
        {
            Debug.Log("WIN");
        }

          /**********************************************/
         /*********** condition de victoire ************/
        /**********************************************/

 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

          /**********************************************/
         /*********** conditions de défaite ************/
        /**********************************************/
        
        if (player1Deck.deckCard.Count == 0 && player1DefundHand.defundHand.GetComponent<DefundHand>().defundDeckCard.Count == 0 && player1Hand.card == null && player1DefundHand.card == null
         && player2Deck.deckCard.Count == 0 && player2DefundHand.defundHand.GetComponent<DefundHand>().defundDeckCard.Count == 0 && player2Hand.card == null && player1DefundHand.card == null)
        {
            Lose();
        }

        if (PathToExitExist(cardcolors.red))
        {
            Debug.Log("trouver");
        }
        else
            Debug.Log("pas trouver");

          /**********************************************/
         /*********** conditions de défaite ************/
        /**********************************************/

    }

}
