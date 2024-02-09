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

    [SerializeField] private Animator _animator;
    private void Start()
    {
        _audioSource.Play();
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
}