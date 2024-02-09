using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Case redSpawn, greenSpawn;

    [SerializeField] private GameObject redPawn, greenPawn;

    [SerializeField] private Deck redDeck, greenDeck;

    [SerializeField] private Cards crossCard;

    [SerializeField] private GameObject darkBackground;
    [SerializeField] private List<GameObject> focusZones;
    [SerializeField] private GameObject bubble;
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private GameObject scientist;

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

        redDeck.hand.AddCard(crossCard);
        greenDeck.hand.AddCard(crossCard);

        bubble.SetActive(false);
        scientist.SetActive(false);
        darkBackground.GetComponent<Animator>().SetBool("Dark", true);
        StartCoroutine(DemiSecDelay());
    }

    IEnumerator DemiSecDelay()
    {
        yield return new WaitForSeconds(0.5f);
        bubble.SetActive(true);
        scientist.SetActive(true);
    }
}
