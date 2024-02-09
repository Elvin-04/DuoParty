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

    public void OpenQuitPanel()
    {
        buttonManager.QuittPanel();
    }

    public void CloseQuitPanel()
    {
        buttonManager.QuittPanel();
    }

    public void OpenPlayPanel()
    {
        buttonManager.PlayPanel();
    }

    public void ClosePlayPanel()
    {
        buttonManager.PlayPanel();
    }

    public void OpenSettingPanel()
    {
        buttonManager.SettingPanel();
    }

    public void CloseSettingPanel()
    {
        buttonManager.SettingPanel();
    }
}
