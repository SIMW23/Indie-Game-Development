using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public float speed = 2.5f;
    public float jumpHeight = 15f;

    [Header("Attack")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float attackDamage;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRadius;

    [Header("Switch Modes Components")]
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject placeTrapUI;

    [HideInInspector] public bool Dead = false;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isAttackMode = true;
    private bool isPlaceTrapMode = false;
    public bool IsPlaceTrapMode => isPlaceTrapMode;
       
    public void Update()
    {
        if (joystick.Horizontal >= .2f)
            horizontal = speed;
        else if (joystick.Horizontal <= -.2f)
            horizontal = -speed;
        else
            horizontal = 0f;

        CheckForFlip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "JumpPad")
        {
            Debug.Log("collided");
            rb.velocity = new Vector2(rb.velocity.y, jumpHeight);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        //Debug.Log("Fluping the player");

        isFacingRight = !isFacingRight;

        Vector2 playerScale = this.transform.localScale;

        playerScale.x = playerScale.x * -1;

        this.transform.localScale = playerScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    //flip players
    void CheckForFlip()
    {
        if (joystick.Horizontal > 0.05f)
        {
            if (isFacingRight == false)
                Flip();
        }
        else if (joystick.Horizontal < -0.05f)
        {
            if (isFacingRight == true)
                Flip();
        }
    }

    public void SwitchMode()
    {
        if(isAttackMode && !isPlaceTrapMode)
        {
            isAttackMode = false;
            isPlaceTrapMode = true;
            attackButton.SetActive(false);
            placeTrapUI.SetActive(true);
        }
        else if(!isAttackMode && isPlaceTrapMode)
        {
            isAttackMode = true;
            isPlaceTrapMode = false;
            attackButton.SetActive(true);
            placeTrapUI.SetActive(false);
        }
    }

    public void Attack()
    {
        if(!isAttackMode)
            return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, enemyMask);

        if(enemies != null)
        {
            foreach(Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().TakeDamage(attackDamage, transform);
            }
        }
    }
}