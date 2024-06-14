using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy stats")]
    public EnemyType enemyType;
    [field:SerializeField] public SpawnPosition SpawnPosition { get; set; }
    public float maxHP;
    public float knockbackForce;
    public float speed;
    private float tempSpeed = 0;
    public LayerMask attackLayers;
    public Transform attackPoint;
    public float detectRadius = 1f;
    public float attackRadius = 0.85f;
    public float attackTime = 1;
    public int points;
    public bool isDead = false;

    [Header("Ranged Components")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 3;

    Rigidbody2D rb;
    Transform playerPos;
    float currentHP;
    float attackTimeCounter = 0;
    bool isBeingKnocked = false;
    bool isFacingLeft = false;
    bool isFacingRight = true;
    bool isDetectingObjective = false;
    bool isAttacking = false;


    public static event Action<int> onUpdatePoint;

    SpriteRenderer spriteRenderer;
    Color newColor;

    Base baseCrystal;

    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        SetEnemyType();

        if(SpawnPosition == SpawnPosition.RightScreen)
            SetFacingLeft();
        else
            SetFacingRight();

        baseCrystal = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
    }

    void Update()
    {
        if(!isBeingKnocked)
            DetectObjective();

        if(!isBeingKnocked && !isDetectingObjective && !isAttacking)
        {
            Move();
            return;
        }
    }

    void SetFacingRight()
    {
        if(!isAttacking)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            isFacingRight = true;
            isFacingLeft = false;
        }
    }

    void SetFacingLeft()
    {
        if(!isAttacking)
        {
            transform.localRotation = new Quaternion(0, 180, 0, 0);
            isFacingRight = false;
            isFacingLeft = true;
        }
    }

    void Move()
    {
        if(playerPos == null)
            return;
        
        attackTimeCounter = 0;

        if(transform.position.x - playerPos.position.x >= 0)
        {
            SpawnPosition = SpawnPosition.RightScreen;
        }
        else
        {
            SpawnPosition = SpawnPosition.LeftScreen;
        }

        if(SpawnPosition == SpawnPosition.LeftScreen)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y);
            SetFacingRight();
        }
        else if(SpawnPosition == SpawnPosition.RightScreen)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y);
            SetFacingLeft();
        }
    }

    void DetectObjective()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectRadius, attackLayers);

        if(col == null)
        {
            isDetectingObjective = false;
            return;
        }
        
        if(col.CompareTag("Player"))
        {
            rb.velocity = new Vector2(0,0);

            isDetectingObjective = true;
            if(transform.position.x - col.transform.position.x > 0)
                SetFacingLeft();
            else
                SetFacingRight();
        }

        attackTimeCounter += Time.deltaTime;

        if(attackTimeCounter >= attackTime)
        {
            Attack();
        }
    }

    void SetEnemyType()
    {
        switch(enemyType)
        {
            case EnemyType.Ranged:
                maxHP = 20;
                speed = 1.5f;
                knockbackForce = 8;
                points = 100;
                break;
            case EnemyType.Regular:
                maxHP = 30;
                speed = 2;
                knockbackForce = 8;
                points = 100;
                break;
            case EnemyType.Tank:
                maxHP = 50;
                speed = 1;
                knockbackForce = 5;
                points = 200;
                break;
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        attackTimeCounter = 0;

        switch(enemyType)
        {
            case EnemyType.Ranged:
                RangedAttack();
                break;
            default:
                MeleeAttack();
                break;
        }
    }

    void RangedAttack()
    {
        if(playerPos == null)
            return;
            
        Vector3 directionToThePlayer = (playerPos.position - transform.position).normalized;
        Vector2 shootDirection;

        if(directionToThePlayer.x < 0)
        {
            shootDirection = Vector2.left;
        }
        else
        {
            shootDirection = Vector2.right;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // if (shootDirection == Vector2.left)
        // {
        //     projectile.transform.localScale = new Vector3(-1, 1, 1);
        // }
        // else
        // {
        //     projectile.transform.localScale = new Vector3(1, 1, 1);
        // }
    }

    void MeleeAttack()
    {
        Collider2D attackCol = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayers);
        if(attackCol != null)
            attackCol.GetComponent<IDamageable>().TakeDamage(10);
        else
        {
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHP > damage)
        {
            currentHP -= damage;
        }
        else
        {
            onUpdatePoint?.Invoke(points);
            currentHP = 0f;
            isDead = true;
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage, Transform damageSource)
    {
        if (currentHP > damage)
        {
            currentHP -= damage;
        }
        else
        {
            onUpdatePoint?.Invoke(points);
            currentHP = 0f;
            isDead = true;
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        StartCoroutine(BeingKnockedBack(damageSource));
    }

    IEnumerator BeingKnockedBack(Transform damageSource)
    {
        isBeingKnocked = true;

        if(transform.position.x - damageSource.position.x > 0)
            rb.AddRelativeForce(new Vector2(knockbackForce, 2), ForceMode2D.Impulse);
        else
            rb.AddRelativeForce(new Vector2(-knockbackForce, 2), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        isBeingKnocked = false;
    }

    public void GetSlowedEffect(float slowRate)
    {
        tempSpeed = speed;
        speed *= slowRate;
    }

    public void EraseSlowEffect()
    {
        speed = tempSpeed;
        tempSpeed = 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            baseCrystal.enemyNumbers++;
            isDead = true;
            this.gameObject.SetActive(false);
        }
    }
}

public enum EnemyType
{
    Regular,
    Tank,
    Ranged
}

[System.Serializable]
public enum SpawnPosition
{
    RightScreen,
    LeftScreen
}

