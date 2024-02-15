using System.Collections;
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

    [SerializeField] private GameObject player1Quest;
    [SerializeField] private GameObject player2Quest;

    [SerializeField] private Sprite Check;
    [SerializeField] private Image itemCheck1;
    [SerializeField] private Image itemCheck2;
    
    


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
        StartCoroutine(InitialiseStartsAndEndCases());

        greenPathExist = true;
        redPathExist = true;
    }

    private IEnumerator InitialiseStartsAndEndCases()
    {
        yield return new WaitForSeconds(1f);

        redStartCase = grid.GetSpawnOfColor(cardcolors.red);
        redEndCase = grid.GetEndOfColor(cardcolors.red);
        greenStartCase = grid.GetSpawnOfColor(cardcolors.green);
        greenEndCase = grid.GetEndOfColor(cardcolors.green);

        yield return null;
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

    public bool PathFinding(Case start, Case end, string newColor)
    {

        List<Case> open = new List<Case>(); // cases that will be examinate
        HashSet<Case> closed = new HashSet<Case>(); // cases already examinate
        open.Add(start); // the start case to the examination list

        while (open.Count > 0) // while all cases are not examinate
        {
            Case currentCase = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < currentCase.fCost || open[i].fCost == currentCase.fCost && open[i].hCost < currentCase.hCost) // search the less costly case to reach
                {
                    currentCase = open[i];
                }
            }
            // count this case as an examinate
            open.Remove(currentCase); 
            closed.Add(currentCase);

            // if its the end
            if(currentCase == end)
            {
                return true;
            }

            Path currentCasePath = currentCase.GetPathByColor(newColor);
            foreach (Case neighbour in currentCase.cases) // search in all neighbour cases 
            {
                if (neighbour == null || closed.Contains(neighbour)) continue;

                Path neighbourPath = neighbour.GetPathByColor(newColor);

                bool canReachNeighbourCase = false;

                // verifie if from the current case you can reach the neigbour case
                if ((currentCase.left == neighbour && (neighbourPath.canMoveRight || neighbour.GetCard() == null) && (currentCasePath.canMoveLeft || currentCase.GetCard() == null))
                || (currentCase.right == neighbour && (neighbourPath.canMoveLeft || neighbour.GetCard() == null) && (currentCasePath.canMoveRight || currentCase.GetCard() == null))
                || (currentCase.up == neighbour && (neighbourPath.canMoveDown || neighbour.GetCard() == null) && (currentCasePath.canMoveUp || currentCase.GetCard() == null))
                || (currentCase.down == neighbour && (neighbourPath.canMoveUp || neighbour.GetCard() == null) && (currentCasePath.canMoveDown || currentCase.GetCard() == null)))
                {
                    canReachNeighbourCase = true;
                }
                    

                if (!canReachNeighbourCase) // if the neighbour is not reachable
                {
                    continue;
                }
                    

                int newMovementCostToNeighbour = currentCase.gCost + GetDistance(currentCase, neighbour); // calculate the new movement cost to this neighbour
                if (newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour)) 
                {
                    // update the g cost and the h cost of this neigbour
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, end);
                    neighbour.parent = currentCase;
                    // if this neigbour is not in the open list add him to it
                    if (!open.Contains(neighbour))
                        open.Add(neighbour);
                }
            }
        }
        // no path as been founded
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

        if (hit1.collider != null && hit1.collider.GetComponent<Case>() && hit1.collider.GetComponent<Case>().GetEnd() && hit1.collider.GetComponent<Case>().GetEColor() == cardcolors.red)
        {
            player1Quest.SetActive(true);
            if (player1.GetComponent<PlayerMovement>().HasAllItems())
            {
                player1Finish = true;
                itemCheck1.sprite = Check;
            }

        }
        else
        {
            player1Finish = false;
            player1Quest.SetActive(false);
        }

        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(player2.transform.position.x, player2.transform.position.y), Vector2.zero);

        if (hit2.collider != null && hit2.collider.GetComponent<Case>() && hit2.collider.GetComponent<Case>().GetEnd() && hit2.collider.GetComponent<Case>().GetEColor() == cardcolors.green)
        {
            player2Quest.SetActive(true);
            if (player2.GetComponent<PlayerMovement>().HasAllItems())
            {
                player2Finish = true;
                itemCheck2.sprite = Check;
            }

        }
        else
        {
            player2Finish = false;
            player2Quest.SetActive(false);
        }

        if (player1Finish && player2Finish)
        {
            Win();
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(true);
            GameObject.Find("EndMenu").transform.Find("VictoryText").gameObject.SetActive(true);
        }

          /**********************************************/
         /*********** condition de victoire ************/
        /**********************************************/

 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

          /**********************************************/
         /*********** conditions de défaite ************/
        /**********************************************/
        
        if (player1Deck.deckCard.Count == 0 && player1DefundHand.defundHand.GetComponent<DefundHand>().defundDeckCard.Count == 0 && player1Hand.card == null && player1DefundHand.card == null
         && player2Deck.deckCard.Count == 0 && player2DefundHand.defundHand.GetComponent<DefundHand>().defundDeckCard.Count == 0 && player2Hand.card == null && player1DefundHand.card == null)
        {
            Lose();
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(true);
            GameObject.Find("EndMenu").transform.Find("DefeatText").gameObject.SetActive(true);
        }

          /**********************************************/
         /*********** conditions de défaite ************/
        /**********************************************/

    }



}
