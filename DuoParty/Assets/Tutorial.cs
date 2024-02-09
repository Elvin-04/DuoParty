using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Case redSpawn, greenSpawn;

    [SerializeField] private GameObject redPawn, greenPawn;

    [SerializeField] private Deck redDeck, greenDeck;

    [SerializeField] private Cards crossCard;

    private void Start()
    {
        redSpawn.SetSpawnRed();
        greenSpawn.SetSpawnGreen();

        redPawn.transform.position = redSpawn.transform.position;
        redPawn.GetComponent<PlayerMovement>().actCase = redSpawn;

        greenPawn.transform.position = greenSpawn.transform.position;
        greenPawn.GetComponent<PlayerMovement>().actCase = greenSpawn;

        redDeck.deckCard.Clear();
        greenDeck.deckCard.Clear();

        redDeck.deckCard.Add(crossCard);
        greenDeck.deckCard.Add(crossCard);

        redDeck.hand.AddCard(crossCard);
        greenDeck.hand.AddCard(crossCard);
    }
}
