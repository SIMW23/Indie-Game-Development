using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    
    // Start is called before the first frame update
    void Start()
    {
        int levelCompleted = PlayerPrefs.GetInt("levelCompleted", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {

            if(i + 1 > levelCompleted)  levelButtons[i].interactable = false;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        //button.interactable = true;
    }

}
