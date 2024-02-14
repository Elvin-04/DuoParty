using UnityEngine;

public class OneCardPerRound : MonoBehaviour
{
    [SerializeField] private RoundByRound roundByRound;
    [SerializeField] private WinLoseCondition winLoseCondition;


    public void StopAllCards()
    {
        roundByRound.StopAllCardsPlays();
        /*if (!winLoseCondition.PathFinding(winLoseCondition.redStartCase, winLoseCondition.redEndCase, "Red") || !winLoseCondition.PathFinding(winLoseCondition.greenStartCase, winLoseCondition.greenEndCase, "Green"))
        {
            winLoseCondition.Lose();
        }*/
        
    }


}
