using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rouge : MonoBehaviour, Hero
{
    public RougeObject Data;

    public GameObject Projectile;

    Animator animator;
    Rigidbody2D body;

    int Damage = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        InputManager.Instance.SetHero(this, transform, Data);
        Damage = (int)Data.AttackDamage;
    }

    public void Auto()
    {
        GameObject projectileObject = Instantiate(Projectile, body.position, Quaternion.identity);

        DoDamage DMG = projectileObject.GetComponent<DoDamage>();

        if (DMG != null)
        {
            DMG.SetDamage(Damage);
        }
    }

    public void Die()
    {
        
    }

    public void Resurrect()
    {
        
    }

    public void Skill()
    {
        
    }

    public void TakeDamage()
    {
        
    }

    public void Ultimate()
    {
        
    }
}
