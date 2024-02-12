using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

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

    public List<Texts> tutoSteps;
    private int currentIndex = 0;

    private int textIndexInStep = 0;

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
        UpdateTuto();
    }

    public void UpdateTuto()
    {
        if(currentIndex < tutoSteps.Count)
        {
            if (tutoSteps[currentIndex].isTextInstruction)
            {
                bubble.SetActive(true);
                scientist.SetActive(true);
                bubbleText.text = tutoSteps[currentIndex].texts[textIndexInStep];
                if(textIndexInStep + 1 < tutoSteps[currentIndex].texts.Count)
                {
                    textIndexInStep++;
                }
                else
                {
                    textIndexInStep = 0;
                    currentIndex++;
                }
            }
            else if (tutoSteps[currentIndex].isPlayerAction)
            {
                bubble.SetActive(false);
                scientist.SetActive(false);
            }
        }
        else
        {

        }



        //if (currentIndex < tutoSteps.Count && tutoSteps[currentIndex].isTextInstruction && localIndex < tutoSteps[currentIndex].texts.Count)
        //{
        //    bubble.SetActive(true);
        //    scientist.SetActive(true);
        //    bubbleText.text = tutoSteps[currentIndex].texts[localIndex];
        //    localIndex++;
        //}
        
        //else
        //{
        //    localIndex = 0;
        //    bubble.SetActive(false);
        //    scientist.SetActive(false);
        //}
    }

    public void EnterButton(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            UpdateTuto();
        }
    }
}

[System.Serializable]
public struct Texts
{
    public bool isTextInstruction;
    public bool isPlayerAction;
    public bool isFocusText;
    public List<string> texts;
    public List<FocusText> focusTexts;
}

[System.Serializable]
public struct FocusText
{
    public Vector2 position;
    public string text;
    public FocusZoneShape shape;
}

public enum FocusZoneShape
{
    SQUARE,
    SPHERE,
}
