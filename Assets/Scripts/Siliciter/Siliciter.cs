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

    }

    public void Die()
    {
        animator.Play("Base Layer.Die", 0, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Siliciter>().enabled = false;
    }
}
