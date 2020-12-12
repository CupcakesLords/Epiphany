using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hero
{
    void Auto();
    void Skill();
    void Ultimate();
    void TakeDamage();
    void Die();
    void Resurrect();
}
