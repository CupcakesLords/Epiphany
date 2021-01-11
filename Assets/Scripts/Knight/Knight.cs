using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : MonoBehaviour, Hero
{
    [HideInInspector]
    public float AutoTimer = 0;
    [HideInInspector]
    public float SkillTimer = 0;

    public KnightObject Data;
    public GameObject AutoHitbox;

    Animator animator;

    Color KnightColor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        KnightColor = GetComponent<SpriteRenderer>().material.color;
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
                    if (i == null)
                        continue;
                    EnemyHealth a = i.GetComponent<EnemyHealth>();
                    if (a != null)
                        a.TakeDamage((int)Data.AttackDamage);
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
        InputManager.Instance.PauseUI(true); InputManager.Instance.Ded.GetComponent<DeadMenu>().Initialize();
        gameObject.GetComponent<Knight>().enabled = false;
        gameObject.GetComponent<HeroHealth>().enabled = false;
        gameObject.GetComponent<HeroMove>().enabled = false;
    }

    private IEnumerator SkillTimerCountDown()
    {
        gameObject.GetComponent<HeroMove>().moveSpeed += 75;
        float temp = AutoTimer; AutoTimer = 0f;
        SkillTimer = Data.SkillTimer;
        while (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
            yield return null;
        }
        SkillTimer = 0;
        AutoTimer = temp;
        gameObject.GetComponent<HeroMove>().moveSpeed -= 75;
    }

    public void Ultimate() 
    {
        if (isInvulnerableFromDamage)
        {
            StopCoroutine(InvulnerableFromTakingDamage);
            isInvulnerableFromDamage = false;
            GetComponent<SpriteRenderer>().material.color = KnightColor;
        }
        InputManager.Instance.UltimateClick();
        StartCoroutine(Ult());
    }

    private IEnumerator Ult()
    {
        animator.Play("Base Layer.Block", 0, 0);
        InputManager.Instance.EnableUI(false);
     
        gameObject.GetComponent<Knight>().enabled = false;
        gameObject.GetComponent<HeroHealth>().enabled = false;
        gameObject.GetComponent<HeroMove>().enabled = false;

        Color temp = GetComponent<SpriteRenderer>().material.color;
        GetComponent<SpriteRenderer>().material.color = Color.yellow;

        Vector3 scaletemp = transform.localScale;

        float sign = 1f; if (scaletemp.x < 0) sign = -1f;

        float Timer = 1.5f;
        while (Timer > 0)
        {
            Timer -= Time.deltaTime;
            transform.localScale += new Vector3(sign, 1f, 0) * Time.deltaTime * 0.3f;
            yield return null;
        }

        transform.localScale = scaletemp;

        gameObject.GetComponent<Knight>().enabled = true;
        gameObject.GetComponent<HeroHealth>().enabled = true;
        gameObject.GetComponent<HeroMove>().enabled = true;

        GetComponent<SpriteRenderer>().material.color = temp;

        animator.Play("Base Layer.Idle", 0, 0);
        InputManager.Instance.EnableUI(true);
    }

    public void TakeDamage() 
    {
        if(isInvulnerableFromDamage)
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

        gameObject.GetComponent<HeroHealth>().enabled = false;

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

        GetComponent<SpriteRenderer>().material.color = temp[0]; gameObject.GetComponent<HeroHealth>().enabled = true;

        isInvulnerableFromDamage = false;
    }

    public void Resurrect()
    {
        animator.Play("Base Layer.Idle", 0, 0);
        gameObject.GetComponent<Knight>().enabled = true;
        gameObject.GetComponent<HeroHealth>().enabled = true;
        gameObject.GetComponent<HeroMove>().enabled = true;

        gameObject.GetComponent<HeroHealth>().HealToFull();
    }

    public string ReturnName()
    {
        return "Knight";
    }
}
