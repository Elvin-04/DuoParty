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
        Debug.Log("WIN");
    }

    public void Lose()
    {
        Debug.Log("LOSE");
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
