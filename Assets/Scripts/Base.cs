using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Base : MonoBehaviour
{
    public int enemyNumbers = 0;
    public int levelLimit;
    public bool gameOver = false;
    [SerializeField] private GameObject gameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
