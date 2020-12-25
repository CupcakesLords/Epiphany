using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siliciter : MonoBehaviour, Enemy
{
    public EnemyObject SiliciterData;
 
    float Change = 3f;
    Animator animator;

    [HideInInspector]
    public float AttackSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AttackSpeed = SiliciterData.AttackCDR;
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
                animator.Play("Base Layer.Attack", 0, 0);
            }

            Change = 3f;
        }
    }

    public void Attack()
    {
        EnemyAttack atk = GetComponent<EnemyAttack>();
        if(atk != null)
        {
            atk.Attack();
        }
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

    public void Die()
    {
        animator.Play("Base Layer.Die", 0, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Siliciter>().enabled = false;
    }
}
