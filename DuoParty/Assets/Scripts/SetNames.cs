using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SetNames : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Color green;
    [SerializeField] private Color red;

    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private string sceneToLoad;

    private int turn;
    private void Start()
    {
        turn = 0;

        title.text = "Player 1 name";
        title.color = green;
    }

    public void SetName()
    {
        if(inputField.text != "")
        {
            
            if(turn == 0)
            {
                PlayerPrefs.SetString("Player1Name", inputField.text);
            }
            else if(turn == 1)
            {
                PlayerPrefs.SetString("Player2Name", inputField.text);
            }

            title.text = "Player 2 name";
            title.color = red;
            inputField.text = "";
            if(turn == 1)
            {
                SceneManager.LoadScene(sceneToLoad);
                return; 
            }
            turn++;
            
        }
        
    }

    public void Enterbutton(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            SetName();
        }
    }
}
