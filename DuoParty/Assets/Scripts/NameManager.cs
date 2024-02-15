using TMPro;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Name;
    [SerializeField] private TextMeshProUGUI player2Name;

    void Awake()
    {
        player1Name.text = PlayerPrefs.GetString("Player1Name", "Player 1");
        player2Name.text = PlayerPrefs.GetString("Player2Name", "Player 2");
    }

}
