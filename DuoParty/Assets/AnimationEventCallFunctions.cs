using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCallFunctions : MonoBehaviour
{
    private ButtonManager buttonManager;

    private void Start()
    {
        buttonManager = transform.parent.GetComponent<ButtonManager>();
    }

    public void OpenCreditPanel()
    {
        buttonManager.Credit();
    }

    public void CloseCreditPanel()
    {
        buttonManager.CreditClose();
    }

    public void OpenPlayPanel()
    {
        buttonManager.PlayPanel();
    }

    public void ClosePlayPanel()
    {
        buttonManager.PlayPanel();
    }

    public void OpenInformationPanel()
    {
        buttonManager.InformationPanel();
    }

    public void CloseinformationPanel()
    {
        buttonManager.InformationPanel();
    }
}
