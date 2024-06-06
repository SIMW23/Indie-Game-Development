using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour
{
    Vector2 startPos;
    public int pixelDistToDetect = 20;
    bool fingerDown;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform attackRange;
    [SerializeField] private float atkRadius;
    [SerializeField] private Animator animController;
    [SerializeField] private int dmg = 35;

    public void Update()
    {
        if (IsPointerOverGameObject())
            return;

        CheckFingerDown();
        ResetTouch();
        if (fingerDown)
        {
            if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                Debug.Log("Swiped up");
                fingerDown = false;
                ColliderCheck(SwipeDirection.UP);
            }
            else if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe left");
                ColliderCheck(SwipeDirection.LEFT);
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe right");
                ColliderCheck(SwipeDirection.RIGHT);
            }
        }
    }

    private bool IsPointerOverGameObject()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return true;
            }
        }
        return false;
    }

    void CheckFingerDown()
    {
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
    }
    void ResetTouch()
    {
        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
            Debug.Log("touch reset");
        }
    }

    void ColliderCheck(SwipeDirection direction)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        ContactFilter2D contacts = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = enemyMask
        };

        Vector2 atkDirection = Vector2.zero;
        Collider2D[] attack = null;

        if (direction == SwipeDirection.RIGHT)
        {
            atkDirection = Vector2.right;
            attack = Physics2D.OverlapCircleAll(transform.position + Vector3.right, atkRadius, enemyMask);
        }
        if (direction == SwipeDirection.LEFT)
        {
            atkDirection = Vector2.left;
            attack = Physics2D.OverlapCircleAll(transform.position + Vector3.left, atkRadius, enemyMask);
        }
        if (direction == SwipeDirection.UP)
        {
            atkDirection = Vector2.up;
            attack = Physics2D.OverlapCircleAll(transform.position + Vector3.up, atkRadius, enemyMask);
        }
                
        if (attack.Length > 0)
        {
            for (int i = 0; i < attack.Length; i++)
            {
                attack[i].GetComponent<EnemyStats>().TakeDamage(dmg);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkRadius);
    }
}

public enum SwipeDirection
{
    UP, LEFT, RIGHT, DOWN
}

