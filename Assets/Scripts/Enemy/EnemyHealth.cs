using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyObject Data;

    [HideInInspector]
    public int HealthPoint;
    [HideInInspector]
    public int CurrentHealth;

    void Start()
    {
        CurrentHealth = (int)Data.MaxHealthPoint;
        HealthPoint = (int)Data.MaxHealthPoint;
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth = CurrentHealth - dmg;

        if (CurrentHealth <= 0)
        {
            gameObject.GetComponent<Enemy>().Die();
            return;
        }
        gameObject.GetComponent<Enemy>().TakeDamage();
        
    }

    public bool IsDead()
    {
        return (CurrentHealth <= 0);
    }
}
