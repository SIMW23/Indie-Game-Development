using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turretBullet;
    [SerializeField] private float range = 15f;
    [SerializeField] float fireRate = 0.7f;
    [SerializeField] float bulletSpeed = 3;
    private float fireCountdown = 0f;
    [SerializeField] GameObject projectile;
    private Transform target;
    private bool isCoolingDown = false;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public void Update()
    {
        DetectEnemy();
    }

    void DetectEnemy()
    {
        if(isCoolingDown)
            return;

        if(Physics2D.OverlapCircleAll(transform.position, range, 1<<6) != null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, 1<<6);
            float closestDistance = Mathf.Infinity;
            Transform closestEnemyPos = null;

            for(int i = 0; i < enemies.Length; i++)
            {
                if(transform.position.x - enemies[i].transform.position.x < closestDistance)
                {
                    closestDistance = transform.position.x - enemies[i].transform.position.x;
                    closestEnemyPos = enemies[i].transform;
                }
            }

            StartCoroutine(Shoot(closestEnemyPos));
        }
    }

    IEnumerator Shoot(Transform enemyPos)
    {
        if(enemyPos == null)
            yield break;

        isCoolingDown = true;

        bool enemyIsOnTheRight;

        if(transform.position.x - enemyPos.position.x > 0)
            enemyIsOnTheRight = false;
        else
            enemyIsOnTheRight = true;

        GameObject bullet = Instantiate(turretBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce((enemyIsOnTheRight ? Vector2.right : Vector2.left) * bulletSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1 / fireRate);
        isCoolingDown = false;
        Destroy(bullet, 5);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
