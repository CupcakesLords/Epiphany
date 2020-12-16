using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public HeroObject Data;

    Animator animator;
    Rigidbody2D rigidbody2d;

    [HideInInspector]
    public float moveSpeed;

    private Vector3 movement;

    private void Start()
    {
        moveSpeed = Data.MoveSpeed;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
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

            movement = new Vector3((InputManager.Instance.joystick.Horizontal + d) * 0.1f, (InputManager.Instance.joystick.Vertical + f) * 0.1f, 0);

            rigidbody2d.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

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
