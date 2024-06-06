using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTurret : MonoBehaviour
{
    [SerializeField] int healAmount = 30;
    bool canHealAgain = true;
    [SerializeField] float healCooldown = 10f;
    PlayerStats playerStats;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && canHealAgain)
        {
            StartCoroutine(Heal());
        }
    }
    IEnumerator Heal()
    {
        playerStats.currentHP += healAmount;
        Debug.Log(playerStats.currentHP);
        if (playerStats.currentHP > playerStats.maxHP)
        {
            playerStats.currentHP = playerStats.maxHP;
        }
        canHealAgain = false;
        yield return new WaitForSeconds(healCooldown);
        canHealAgain = true;
        Debug.Log("Can heal again");
    }
}
