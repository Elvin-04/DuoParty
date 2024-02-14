using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Case redSpawn, greenSpawn;

    [SerializeField] private GameObject redPawn, greenPawn;

    [SerializeField] private Deck redDeck, greenDeck;

    [SerializeField] private Cards crossCard;

    [SerializeField] private GameObject darkBackground;
    [SerializeField] private GameObject bubble;
    [SerializeField] private TextMeshProUGUI bubbleText;
    [SerializeField] private GameObject scientist;

    [SerializeField] private GameObject sphereFocus;

    [SerializeField] private float focusSpeed;

    [SerializeField] private GameObject ScientistAndBubble;

    [Header("Scientist positions")]
    [SerializeField] private Vector2 leftPosition;
    [SerializeField] private Vector2 rightPosition;

    public List<Texts> tutoSteps;
    private int currentIndex = 0;

    private int localIndex = 0;

    private Vector2 focusZoneDestination;
    private bool moveFocusZone;

    private Vector2 focusZoneScaleDest;
    private bool scaleFocusZone;

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

        sphereFocus.SetActive(false);

        bubble.SetActive(false);
        scientist.SetActive(false);
        darkBackground.GetComponent<Animator>().SetBool("Dark", true);
        StartCoroutine(DemiSecDelay());
    }

     private void Update()
    {
        if(moveFocusZone)
        {
            sphereFocus.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(sphereFocus.GetComponent<RectTransform>().anchoredPosition, focusZoneDestination, focusSpeed * Time.deltaTime);
            if (Vector2.Distance(sphereFocus.GetComponent<RectTransform>().anchoredPosition, focusZoneDestination) <= 0.05f)
            {
                moveFocusZone = false;
            }
        }

        if (scaleFocusZone)
        {
            sphereFocus.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(sphereFocus.GetComponent<RectTransform>().sizeDelta, focusZoneScaleDest, focusSpeed * Time.deltaTime);
            if (Vector2.Distance(sphereFocus.GetComponent<RectTransform>().sizeDelta, focusZoneScaleDest) <= 0.05f)
            {
                scaleFocusZone = false;
            }
        }
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
                sphereFocus.SetActive(false);
                bubbleText.text = tutoSteps[currentIndex].texts[localIndex];

                if (localIndex + 1 < tutoSteps[currentIndex].texts.Count)
                {
                    localIndex++;
                }
                else
                {
                    localIndex = 0;
                    currentIndex++;
                }
            }
            else if (tutoSteps[currentIndex].isFocusText)
            {
                bubble.SetActive(true);
                scientist.SetActive(true);
                bubbleText.text = tutoSteps[currentIndex].focusTexts[localIndex].text;

                if (tutoSteps[currentIndex].focusTexts[localIndex].scientistPosition == ScientistPosition.LEFT)
                    ScientistAndBubble.GetComponent<RectTransform>().anchoredPosition = leftPosition;
                else
                    ScientistAndBubble.GetComponent<RectTransform>().anchoredPosition = rightPosition;


                if (sphereFocus.activeSelf)
                {
                    moveFocusZone = true;
                    focusZoneDestination = tutoSteps[currentIndex].focusTexts[localIndex].position;
                    scaleFocusZone = true;
                    focusZoneScaleDest = tutoSteps[currentIndex].focusTexts[localIndex].scale;
                }
                else
                {
                    sphereFocus.SetActive(true);
                    sphereFocus.GetComponent<RectTransform>().anchoredPosition = tutoSteps[currentIndex].focusTexts[localIndex].position;
                    sphereFocus.GetComponent<RectTransform>().sizeDelta = tutoSteps[currentIndex].focusTexts[localIndex].scale;
                }


                if (localIndex + 1 < tutoSteps[currentIndex].focusTexts.Count)
                {
                    localIndex++;
                }
                else
                {
                    localIndex = 0;
                    currentIndex++;
                }



            }
        }
        else
        {

        }
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
    public Vector2 scale;
    public string text;
    public ScientistPosition scientistPosition;
}


public enum ScientistPosition
{
    LEFT,
    RIGHT,
}
