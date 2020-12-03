using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroObject : ScriptableObject
{
    public int MaxHealthPoints;
    public int MaxManaPoints;
    public float AttackCDR;
    public float SkillCDR;
    public float UltimateCDR;
    public float MoveSpeed;
}
