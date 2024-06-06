using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealTurretButton : MonoBehaviour
{
    private static HealTurretButton instance;
    public static HealTurretButton Instance
    {
        get
        {
            if (instance == null)
            {
                // Search for an existing instance
                instance = FindObjectOfType<HealTurretButton>();

                // If none exists, create a new GameObject and add this component to it
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(HealTurretButton).ToString());
                    instance = singleton.AddComponent<HealTurretButton>();

                    // Make sure the GameObject persists across scenes
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    public TrapSO healTurret;
    [field: SerializeField] public bool IsCoolDown { get; set; } = false;
    Image cooldownImage;
    float cooldownTime;

    void Awake()
    {
        cooldownImage = GetComponent<Image>();
        cooldownTime = healTurret.CooldownTime;
        cooldownImage.fillAmount = 0;
    }

    void Update()
    {
        if (IsCoolDown)
        {
            cooldownImage.fillAmount -= 1 / cooldownTime * Time.deltaTime;

            if (cooldownImage.fillAmount <= 0)
            {
                cooldownImage.fillAmount = 0;
                IsCoolDown = false;
            }
        }
    }

    public void SetCoolDown()
    {
        if (!IsCoolDown)
        {
            IsCoolDown = true;
            cooldownImage.fillAmount = 1;
        }
    }
}
