using UnityEngine;

public class OneCardPerRound : MonoBehaviour
{
    [SerializeField] private RoundByRound roundByRound;
    [SerializeField] private WinLoseCondition winLoseCondition;


    public void StopAllCards()
    {
        roundByRound.StopAllCardsPlays();
        if (!winLoseCondition.PathFinding(winLoseCondition.redStartCase, winLoseCondition.redEndCase) /*|| !winLoseCondition.PathFinding(winLoseCondition.greenStartCase, winLoseCondition.greenEndCase)*/)
        {
            print("pas trouver ( perdu )");
        }
        else print("trouver");
    }


}
