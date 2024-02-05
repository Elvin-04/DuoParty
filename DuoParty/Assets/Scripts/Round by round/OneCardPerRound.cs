using UnityEngine;

public class OneCardPerRound : MonoBehaviour
{
    [SerializeField] private RoundByRound roundByRound;


    public void StopAllCards()
    {
        roundByRound.StopAllCardsPlays();
    }

}
