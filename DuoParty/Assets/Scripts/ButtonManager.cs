using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    public AudioSource _audioSource;
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
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}