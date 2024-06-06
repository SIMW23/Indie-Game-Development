using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public List<CollisionTypes> collisionTypesIgnore;
    void Awake()
    {
        foreach(CollisionTypes col in collisionTypesIgnore)
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, (int)col);
        }
    }
}

[HideInInspector]
public enum CollisionTypes
{
    Enemy = 6,
    Player = 7,
    Crystal = 8
}
