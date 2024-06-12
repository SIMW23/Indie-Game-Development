using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private AudioClip sfxClick;

    private bool isOpeningThePauseMenu = false;
    public bool IsOpeningThePauseMenu => isOpeningThePauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsPage.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToggleOnThePauseMenu()
    {
        isOpeningThePauseMenu = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ToggleOffThePauseMenu()
    {
        isOpeningThePauseMenu = false;
        pauseMenu.SetActive(false);
        settingsPage.SetActive(false);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        isOpeningThePauseMenu = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.PlaySFX(sfxClick);
    }

    public void Settings()
    {
        settingsPage.SetActive(true);
        pauseMenu.SetActive(false);
        AudioManager.Instance.PlaySFX(sfxClick);
    } 

    public void Back()
    {
        settingsPage.SetActive(false);
        pauseMenu.SetActive(true);
        AudioManager.Instance.PlaySFX(sfxClick);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlaySFX(sfxClick);
        //SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
