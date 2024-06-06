using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour
{
    void Awake()
    {
        Destroy(this.gameObject, 3);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            collider2D.GetComponent<PlayerStats>().TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
