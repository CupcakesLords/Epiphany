using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyObject : ScriptableObject
{
    public float AttackCDR;
    public float Damage;
    public float MaxHealthPoint;
}
