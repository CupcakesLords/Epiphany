using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    public HeroObject Data;

    [HideInInspector]
    public int HealthPoint;
    [HideInInspector]
    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = Data.MaxHealthPoints;
        HealthPoint = Data.MaxHealthPoints;
        InputManager.Instance.HP_Bar.GetComponent<HPBar>().SetHealth(CurrentHealth, HealthPoint);
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth = CurrentHealth - dmg;
        
        if(CurrentHealth <= 0)
        {
            gameObject.GetComponent<Hero>().Die();
            InputManager.Instance.HP_Bar.GetComponent<HPBar>().SetHealth(CurrentHealth, HealthPoint);
            return;
        }
        gameObject.GetComponent<Hero>().TakeDamage();
        InputManager.Instance.HP_Bar.GetComponent<HPBar>().SetHealth(CurrentHealth, HealthPoint);
    }

    public void HealToFull()
    {
        CurrentHealth = HealthPoint;
        InputManager.Instance.HP_Bar.GetComponent<HPBar>().SetHealth(CurrentHealth, HealthPoint);
    }

    public bool IsDead()
    {
        return (CurrentHealth <= 0);
    }
}
