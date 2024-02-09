using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu, _endMenu;
    public AudioSource _audioSource;
    private bool _pauseActive;
    private bool _endActive;
    private bool _quittActive;
    private bool _playActive;
    private bool _settingActive;


    [SerializeField] private Animator _animator;
    private void Start()
    {
        //_audioSource.Play();
        Screen.SetResolution(1920, 1080, true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PauseMenu()
    {
        if (!_pauseActive) 
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(true);
            _pauseActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("PauseMenu").gameObject.SetActive(false);
            _pauseActive = false;
        }
    }
    public void EndMenu()
    {
        if (!_pauseActive)
        {
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(true);
            _pauseActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("EndMenu").gameObject.SetActive(false);
            _pauseActive = false;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void Zoom()
    {
        _animator.SetBool("Zoomed", !_animator.GetBool("Zoomed"));
    }

    public void ZoomPlay()
    {
        _animator.SetBool("ZoomedPlay", !_animator.GetBool("ZoomedPlay"));
    }

    public void ZoomSetting()
    {
        _animator.SetBool("ZoomedSetting", !_animator.GetBool("ZoomedSetting"));
    }

    public void QuittPanel()
    {
        if (!_quittActive)
        {
            GameObject.Find("MenuManager").transform.Find("QuittMenu").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(false);
            _quittActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("QuittMenu").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(true);
            _quittActive = false;
        }

    }
    public void PlayPanel()
    {
        if (!_playActive)
        {
            GameObject.Find("MenuManager").transform.Find("MainMenu").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(false);
            _playActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("MainMenu").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(true);
            _playActive = false;
        }
    }

    public void SettingPanel()
    {
        if (!_settingActive)
        {
            GameObject.Find("MenuManager").transform.Find("Settings").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(false);

            _settingActive = true;
        }
        else
        {
            GameObject.Find("MenuManager").transform.Find("Settings").gameObject.SetActive(false);
            GameObject.Find("MenuManager").transform.Find("GameName").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("QuittPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("PlayPanel").gameObject.SetActive(true);
            GameObject.Find("MenuManager").transform.Find("SettingPanel").gameObject.SetActive(true);
            _settingActive = false;
        }
    }
}
