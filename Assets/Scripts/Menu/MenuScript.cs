using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private AudioClip buttonSFX;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneByInt()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void ToggleLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        AudioManager.Instance.PlaySFX(buttonSFX);
    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        AudioManager.Instance.PlaySFX(buttonSFX);
    }
}
