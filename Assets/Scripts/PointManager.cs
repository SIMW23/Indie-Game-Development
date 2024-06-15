using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PointManager : MonoBehaviour
{
    private static PointManager instance;
    public static PointManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Search for an existing instance
                instance = FindObjectOfType<PointManager>();

                // If none exists, create a new GameObject and add this component to it
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(PointManager).ToString());
                    instance = singleton.AddComponent<PointManager>();

                    // Make sure the GameObject persists across scenes
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    [SerializeField] private Text pointText;
    [SerializeField] private int totalPoint = 0;
    public int TotalPoint => totalPoint;

    void Awake()
    {
        UpdatePoint(totalPoint);
    }

    void OnEnable()
    {
        EnemyStats.onUpdatePoint += UpdatePoint;
        PlayerTrapPlacing.onUpdatePoint += UpdatePoint;
    }

    void OnDisable()
    {
        EnemyStats.onUpdatePoint -= UpdatePoint;
        PlayerTrapPlacing.onUpdatePoint -= UpdatePoint;
    }

    public void UpdatePoint(int points)
    {
        totalPoint += points;
        pointText.text = "Points: " + totalPoint.ToString();
    }
}
