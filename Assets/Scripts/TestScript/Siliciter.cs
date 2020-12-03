using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siliciter : MonoBehaviour
{
    public GameObject projectilePrefab;

    float Change = 3f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Change -= Time.deltaTime;
        if(Change <= 0)
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

    public void Die()
    {
        Destroy(gameObject);
    }
}
