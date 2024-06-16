using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public float currentHP;
    public float maxHP = 200;

    public Slider healthBar;
    public Text healthText;
     
    private string healthString;

    [SerializeField] private AudioClip playerDamage;
    [SerializeField] private GameObject gameOverMenu;

    void Start()
    {
        currentHP = maxHP;
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        healthBar.value = currentHP / maxHP;
        healthText.text = currentHP.ToString() + "/" + maxHP.ToString(); 
    }

    public void TakeDamage(float damage)
    {
        if (currentHP > damage)
        {
            currentHP -= damage;
            AudioManager.Instance.PlaySFX(playerDamage);
        }
        else
        {
            currentHP = 0f;
            Destroy(this.gameObject);
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }
}
