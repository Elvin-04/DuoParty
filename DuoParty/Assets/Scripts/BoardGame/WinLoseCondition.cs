using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Case redStartCase;
    public Case redEndCase;
    public Case greenStartCase;
    public Case greenEndCase;

    public bool greenPathExist = true;
    public bool redPathExist = true;


    private void Start()
    {
        redStartCase = grid.GetSpawnOfColor(cardcolors.red);
        redEndCase = grid.GetEndOfColor(cardcolors.red);
        greenStartCase = grid.GetSpawnOfColor(cardcolors.green);
        greenEndCase = grid.GetEndOfColor(cardcolors.green);

        greenPathExist = true;
        redPathExist = true;
}

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

    private List<Case> GetNotVerifiedCases(List<Case> nextVerificationList, List<Case> verifiedCases, List<Case> inVerification)
    {
        List<Case> result = new List<Case>();
        foreach (Case _case in nextVerificationList)
        {
            if (_case != null && !verifiedCases.Contains(_case) && !inVerification.Contains(_case) && !_case.GetCard())
            {
                result.Add(_case);
            }
        }
        return result;
    }



    public bool PathFinding(Case start, Case end)
    {

        List<Case> open = new List<Case>();
        HashSet<Case> closed = new HashSet<Case>();
        open.Add(start);

        while (open.Count > 0)
        {
            Case currentCase = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < currentCase.fCost || open[i].fCost == currentCase.fCost && open[i].hCost < currentCase.hCost)
                {
                    currentCase = open[i];
                }
            }
            open.Remove(currentCase); 
            closed.Add(currentCase);

            if(currentCase == end)
            {
                return true;
            }

            foreach (Case neighbour in currentCase.cases)
            {
                if(neighbour == null || closed.Contains(neighbour) || neighbour.GetCard() != null)
                    continue;

                int newMovementCostToNeighbour = currentCase.gCost + GetDistance(currentCase, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.parent = currentCase;

                    if (!open.Contains(neighbour))
                        open.Add(neighbour);
                }
            }

            
        }


        return false;
    }

    private int GetDistance(Case caseA, Case caseB)
    {
        if (caseA == null || caseB == null)
            return 0;
        int distX = caseA.x - caseB.x;
        int distY = caseA.y - caseB.y;
        if (distX > distY)
            return (2 * distY + (distX - distY));

        return (2 * distX + (distY - distX));
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

          /**********************************************/
         /*********** conditions de défaite ************/
        /**********************************************/

    }



}
