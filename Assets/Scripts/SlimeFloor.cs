using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFloor : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float slowRate = 0.5f;
    float tempSpeed = 0;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyStats>().GetSlowedEffect(slowRate);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyStats>().EraseSlowEffect();
        }
    }
}
