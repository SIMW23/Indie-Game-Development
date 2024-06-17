using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private GameObject LevelSelectScreen;
    [SerializeField] private AudioClip sfxClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MainMenuActive()
    {
        MainMenuScreen.SetActive(true);
        LevelSelectScreen.SetActive(false);
        AudioManager.Instance.PlaySFX(sfxClick);
    }

    public void LevelSelectActive()
    {
        LevelSelectScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
        AudioManager.Instance.PlaySFX(sfxClick);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}
