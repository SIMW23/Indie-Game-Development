using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFloor : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float triggerRate = 1.5f;
    bool canTrigger = true;

    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 0.5f), 
                                   new Vector2(transform.localScale.x - 0.1f, transform.localScale.y + 0.5f), 0, 1<<6);

        if(enemies != null)
        {
            if(canTrigger)
            {
                StartCoroutine(ResetAttack());
                foreach(Collider2D col in enemies)
                {
                    col.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
                }
            }
        }
    }

    IEnumerator ResetAttack()
    {
        canTrigger = false;
        yield return new WaitForSeconds(1 / triggerRate);
        canTrigger = true;
    }
}
