using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerigel : MonoBehaviour, Enemy
{
    public GameObject satellite0;
    public GameObject satellite1;
    public GameObject satellite2;

    public int Damage;

    Animator animator;

    float Change = 3f;
    int preChoice = 0;

    public GameObject temp;

    void Start()
    {
        animator = GetComponent<Animator>();
        Damage = 1; SetChildDamage();
    }

    void SetChildDamage()
    {
        DoDamage DMG = satellite0.GetComponent<DoDamage>();

        if (DMG != null)
        {
            DMG.SetDamage(Damage);
        }
        DMG = satellite1.GetComponent<DoDamage>();

        if (DMG != null)
        {
            DMG.SetDamage(Damage);
        }
        DMG = satellite2.GetComponent<DoDamage>();

        if (DMG != null)
        {
            DMG.SetDamage(Damage);
        }
    }

    private void Update()
    {
        Change -= Time.deltaTime;
        if (Change <= 0)
        {
            int choice = Random.Range(1, 4); 
            if (choice == 1)
            {
                animator.Play("Base Layer.Idle", 0, 0);
            }
            else if (choice == 2)
            {
                animator.Play("Base Layer.Walk", 0, 0);
            }
            else if (choice == 3)
            {
                if(preChoice == 3)
                    animator.Play("Base Layer.Walk", 0, 0);
                else
                    animator.Play("Base Layer.Attack", 0, 0);
            }
            preChoice = choice;
            Change = 3f;
        }
    }

    public void Attack()
    {
        EnemyAttack atk = GetComponent<EnemyAttack>();
        if (atk != null)
        {
            atk.Attack(); 
            AerigelOrbit a = satellite0.GetComponent<AerigelOrbit>(); a.ExpandOrbit();
            a = satellite1.GetComponent<AerigelOrbit>(); a.ExpandOrbit();
            a = satellite2.GetComponent<AerigelOrbit>(); a.ExpandOrbit();
        }
    }

    public void StopAttacking()
    {
        AerigelOrbit a;
        if (!(!satellite0)) { a = satellite0.GetComponent<AerigelOrbit>(); if (!(!a)) { a.BackToOrbit(); } }
        if (!(!satellite1)) { a = satellite1.GetComponent<AerigelOrbit>(); if (!(!a)) { a.BackToOrbit(); } }
        if (!(!satellite2)) { a = satellite2.GetComponent<AerigelOrbit>(); if (!(!a)) { a.BackToOrbit(); } }
    }

    public void Die()
    {
        animator.Play("Base Layer.Die", 0, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Aerigel>().enabled = false;

        Destroy(satellite0); Destroy(satellite1); Destroy(satellite2);
    }

    public void TakeDamage()
    {
        if (isInvulnerableFromDamage)
        {
            StopCoroutine(InvulnerableFromTakingDamage);
            isInvulnerableFromDamage = false;
        }

        InvulnerableFromTakingDamage = DamageTakenInvulnerableCountDown();
        StartCoroutine(InvulnerableFromTakingDamage);
    }

    private IEnumerator InvulnerableFromTakingDamage;
    bool isInvulnerableFromDamage = false;

    private IEnumerator DamageTakenInvulnerableCountDown()
    {
        isInvulnerableFromDamage = true;

        gameObject.GetComponent<EnemyHealth>().enabled = false;

        float time = 0.5f; float interval = 0.1f;

        Color[] temp = new Color[2];
        temp[0] = GetComponent<SpriteRenderer>().material.color;
        temp[1] = Color.red;

        int times = 5; GetComponent<SpriteRenderer>().material.color = temp[times % 2];

        while (time >= 0)
        {
            if (interval < 0.01f)
            {
                interval = 0.1f;
                times = times - 1;
                if (times < 0)
                    times = 0;
                GetComponent<SpriteRenderer>().material.color = temp[times % 2];
                continue;
            }

            interval -= Time.deltaTime;
            time -= Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().material.color = temp[0]; gameObject.GetComponent<EnemyHealth>().enabled = true;

        isInvulnerableFromDamage = false;
    }

    public void OnDeadDropLoot()
    {
        int rand = Random.Range(1, 10);
        if (rand > 5)
        {
            GameObject drop = Instantiate(temp, transform.position, Quaternion.identity);
        }
    }
}
