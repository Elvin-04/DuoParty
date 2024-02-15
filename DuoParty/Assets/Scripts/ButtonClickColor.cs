using UnityEngine;
using UnityEngine.UI;

public class ButtonClickColor : MonoBehaviour
{

    public Color WantedColor;
    public Button _button;

    public void ChangeButtonColor()
    {
        ColorBlock cb = _button.colors;
        cb.normalColor = WantedColor;
        cb.highlightedColor = WantedColor;
        cb.pressedColor = WantedColor;
        _button.colors = cb;
    }

}
