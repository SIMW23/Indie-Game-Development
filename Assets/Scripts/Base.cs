using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public int enemyNumbers = 0;
    public int levelLimit;
    public bool gameOver = false;
    [SerializeField] private GameObject gameOverMenu;

    public Text crystalText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        crystalText.text = "Enemy infiltrated: " + enemyNumbers.ToString() + "/" + levelLimit.ToString();
        GameOver();
    }

    void GameOver()
    {
        if (enemyNumbers == levelLimit)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }
}
