using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects", fileName = "Trap")]
public class TrapSO : ScriptableObject
{
    [field:SerializeField] public GameObject Trap { get; set; }
    [field:SerializeField] public int Cost { get; set; }
    [field:SerializeField] public float CooldownTime { get; set; }
}
