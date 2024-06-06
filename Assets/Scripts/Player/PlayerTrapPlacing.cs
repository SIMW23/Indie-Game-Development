using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTrapPlacing : MonoBehaviour
{
    public float placementDistance = 2f;
    public Color availableColor;
    public Color nonAvailableColor;

    public TrapSO trapSO;
    Transform player;
    Vector3 placementPosition;
    float groundYPosition = 0f;

    private GameObject currentPreview;
    private GameObject currentPrefab;
    private bool isPlacing = false;
    private bool canPlace = false;

    public static event Action<int> onUpdatePoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isPlacing && this.GetComponent<PlayerController>().IsPlaceTrapMode)
        {
            UpdatePreview();
        }
        else if((!this.GetComponent<PlayerController>().IsPlaceTrapMode) && trapSO != null)
        {
            Destroy(currentPrefab);
            Destroy(currentPreview);
            trapSO = null;
        }

        if(!GetComponent<PlayerController>().IsPlaceTrapMode)
            isPlacing = false;
    }

    public void UpdateTrap(TrapSO trap)
    {
        trapSO = trap;
        StartPlacing(trapSO);
    }

    public void StartPlacing(TrapSO trapSO)
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        currentPrefab = trapSO.Trap;
        
        currentPreview = new GameObject("Preview", typeof(SpriteRenderer));
        currentPreview.GetComponent<SpriteRenderer>().sprite = trapSO.Trap.GetComponent<SpriteRenderer>().sprite;
        currentPreview.transform.localScale = trapSO.Trap.transform.localScale;
        isPlacing = true;
    }

    void CalculateAvailablePosition()
    {
        // Calculate position in front of the player with fixed Y position
        placementPosition = player.position + player.right * (player.transform.localScale.x > 0 ? placementDistance : -placementDistance);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3, 1<<3);
        if(hit.collider != null)
            groundYPosition = hit.point.y + 0.01f;

        placementPosition.y = groundYPosition + currentPrefab.transform.localScale.y/2; // Lock the Y position to ground level
        placementPosition.z = 0;
    }

    void UpdatePreview()
    {
        CalculateAvailablePosition();
        currentPreview.transform.position = placementPosition;

        // Check for valid placement
        Collider2D hitCollider = Physics2D.OverlapBox(placementPosition, currentPrefab.transform.localScale, 0, 1<<8);
        if ((hitCollider == null || hitCollider.tag == currentPrefab.tag) && PointManager.Instance.TotalPoint >= trapSO.Cost)
        {
            canPlace = true;
            currentPreview.GetComponent<SpriteRenderer>().color = availableColor;
        }
        else
        {
            canPlace = false;
            currentPreview.GetComponent<SpriteRenderer>().color = nonAvailableColor;
        }
    }

    public void PlaceObject()
    {
        //Not enough point
        if(trapSO == null || PointManager.Instance.TotalPoint < trapSO.Cost || !canPlace)
            return;

        if(trapSO == TrapTurretButton.Instance.trapTurret  && TrapTurretButton.Instance.IsCoolDown)
            return;

        CalculateAvailablePosition();

        // Check for valid placement
        Collider2D hitCollider = Physics2D.OverlapCircle(placementPosition, 0.5f);
        if (hitCollider == null || hitCollider.tag != currentPrefab.tag)
        {
            Instantiate(currentPrefab, placementPosition, Quaternion.identity);
            onUpdatePoint?.Invoke(-trapSO.Cost);

            if(trapSO == TrapTurretButton.Instance.trapTurret && !TrapTurretButton.Instance.IsCoolDown)
                TrapTurretButton.Instance.SetCoolDown();
        }
    }
}
