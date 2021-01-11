using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    void Attack();
    void Die();
    void TakeDamage();
    void OnDeadDropLoot();
}