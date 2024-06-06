using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public float currentHP;
    public float maxHP = 200;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (currentHP > damage)
        {
            currentHP -= damage;
        }
        else
        {
            currentHP = 0f;
            Destroy(this.gameObject);
        }
    }
}
