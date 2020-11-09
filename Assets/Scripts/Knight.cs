using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour, Hero
{
    private float moveSpeed;
    private float AutoTimer = 0;
    private float SkillTimer = 0;

    public KnightObject Data;
    public GameObject AutoHitbox;

    Animator animator;

    private void Start()
    {
        moveSpeed = Data.MoveSpeed;
        animator = GetComponent<Animator>();
        InputManager.Instance.SetHero(this, transform, Data);
    }

    public void Auto()
    {
        if (AutoTimer == 0)
        {
            InputManager.Instance.AttackClick();
            StartCoroutine(AutoTimerCountDown());
            animator.Play("Base Layer.Attack", 0, 0);

            Collider2D myCollider = AutoHitbox.GetComponent<Collider2D>();
            int numColliders = 10;
            Collider2D[] colliders = new Collider2D[numColliders];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.NoFilter();
            int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);
            if (colliderCount > 0)
            {
                foreach(Collider2D i in colliders)
                {
                    Siliciter a = i.GetComponent<Siliciter>();
                    if(a != null)
                        a.Die();
                }
            }
        }
    }

    private IEnumerator AutoTimerCountDown()
    {
        AutoTimer = Data.AttackCDR;
        while (AutoTimer > 0)
        {
            AutoTimer -= Time.deltaTime;
            yield return null;
        }
        AutoTimer = 0;
    }

    public void Skill()
    {
        InputManager.Instance.SkillClick();
        StartCoroutine(SkillTimerCountDown());
    }

    public void Die()
    {
        animator.Play("Base Layer.Die", 0, 0);
    }

    private IEnumerator SkillTimerCountDown()
    {
        moveSpeed += 75;
        SkillTimer = Data.SkillTimer;
        while (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
            yield return null;
        }
        SkillTimer = 0;
        moveSpeed -= 75;
    }

    private void Update()
    {
        if (InputManager.Instance.joystick.Horizontal == 0 || InputManager.Instance.joystick.Vertical == 0)
        {
            animator.SetFloat("MoveX", AnimationConstant.IDLE);
            animator.SetFloat("Basic", AnimationConstant.IDLE); 
            return;
        }
        else
        {
            float Vertical = InputManager.Instance.joystick.Vertical; if (Vertical < 0) Vertical = Vertical * -1;
            float Horizontal = InputManager.Instance.joystick.Horizontal; if (Horizontal < 0) Horizontal = Horizontal * -1;

            float c = (float)Math.Sqrt(Vertical * Vertical + Horizontal * Horizontal);
            float e = 1 - c;
            float d = Horizontal * e / c;              // this adds to horizontal
            float f = (float)Math.Sqrt(e * e - d * d); // this adds to vertical

            if (InputManager.Instance.joystick.Horizontal < 0) d = d * -1;
            if (InputManager.Instance.joystick.Vertical < 0) f = f * -1;
       
            Vector3 movement = new Vector3((InputManager.Instance.joystick.Horizontal + d) * 0.1f, (InputManager.Instance.joystick.Vertical + f) * 0.1f, 0);
            transform.position += movement * moveSpeed * Time.deltaTime;

            if (InputManager.Instance.joystick.Horizontal > 0)
            {
                if (transform.localScale.x < 0)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (InputManager.Instance.joystick.Horizontal < 0)
            {
                if (transform.localScale.x > 0)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            if (moveSpeed < 100)
            {
                animator.SetFloat("MoveX", AnimationConstant.WALK);
                animator.SetFloat("Basic", AnimationConstant.WALK);
            }
            else
            {
                animator.SetFloat("MoveX", AnimationConstant.RUN);
                animator.SetFloat("Basic", AnimationConstant.RUN);
            }
        }
    }
}
