using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] bool isInteractible = true;
    [SerializeField] private bool isSpawn;
    [SerializeField] private bool isEnd;
    [SerializeField] private string color;
    public Cards card;

    public Path greenPath;
    public Path redPath;

    public Path GetPathByColor(string color)
    {
        switch (color)
        {
            case "Green":
                return greenPath;
            case "Red":
                return redPath;
            default:
                return greenPath;
        }
    }

    public bool GetInteractible()
    {
        return isInteractible;
    }

    public bool GetSpawn()
    {
        return isSpawn;
    }

    public bool GetEnd()
    {
        return isEnd;
    }

    public string GetColor()
    {
        return color;
    }

    public Cards GetCard()
    {
        return card;
    }

    public void AddCard(Cards _card)
    {
        card = _card;

        gameObject.GetComponent<SpriteRenderer>().sprite = card.cardImage;

        greenPath = card.instantiatedGreenPath; 
        redPath = card.instantiatedRedPath;
        _card.ResetRotation();

        if (card.cardColor == cardcolors.red)
        { color = "Red"; }
        if (card.cardColor == cardcolors.green)
        { color = "Green"; }
        if(card.cardColor == cardcolors.redAndGreen)
        { color = "RedAndGreen"; }
    }


}

[System.Serializable]
public struct Path
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
}
