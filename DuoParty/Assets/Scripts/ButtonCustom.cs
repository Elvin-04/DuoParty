using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class ButtonCustom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool _interactable = true; 
    public bool _interactableImage = false;


    [Header("Sprites")]
    [SerializeField] private Sprite _disable;
    private Sprite m_enable;

    [Header("Colors")]
    [SerializeField] private Color _enterColor;
    [SerializeField] private Color _exitColor;
    [SerializeField] private Color _clickColor;

    [Space(15)]
    [FormerlySerializedAs("onClick")]
    [SerializeField] private ButtonClickedEvent _onClick = new ButtonClickedEvent();

    [Space(15)]
    [FormerlySerializedAs("onEnter")]
    [SerializeField] private ButtonClickedEvent _onEnter = new ButtonClickedEvent();

    [Space(15)]
    [FormerlySerializedAs("onExit")]
    [SerializeField] private ButtonClickedEvent _onExit = new ButtonClickedEvent();
    private void Start()
    {
        m_enable = GetComponent<Image>().sprite;
    }

    private void Update()
    {
        Interactable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_interactable)
        {
            _onEnter.Invoke();
            if (GetComponentInChildren<TMP_Text>() != null)
                GetComponentInChildren<TMP_Text>().color = _enterColor;
        }
        if (_interactableImage)
        {
            _onEnter.Invoke();
            if (GetComponentInChildren<Image>() != null)
                GetComponentInChildren<Image>().color = _enterColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_interactable)
        {
            _onExit.Invoke();
            if (GetComponentInChildren<TMP_Text>() != null)
                GetComponentInChildren<TMP_Text>().color = _exitColor;
        }
        if (_interactableImage)
        {
            _onExit.Invoke();
            if (GetComponentInChildren<Image>() != null)
                GetComponentInChildren<Image>().color = _exitColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_interactable)
        {
            _onClick.Invoke();
            if (GetComponentInChildren<TMP_Text>() != null)
                GetComponentInChildren<TMP_Text>().color = _clickColor;
        }
    }

    private void Interactable()
    {
        if (!_interactable)
            GetComponent<Image>().sprite = _disable;
        else
            GetComponent<Image>().sprite = m_enable;
    }
}
