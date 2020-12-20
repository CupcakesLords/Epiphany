using System;
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

    [HideInInspector]
    public float AutoTimer = 0;
    [HideInInspector]
    public float SkillTimer = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        InputManager.Instance.SetHero(this, transform, Data);
        Damage = (int)Data.AttackDamage;
    }

    public void Auto()
    {
        if (AutoTimer == 0)
        {
            InputManager.Instance.AttackClick();
            StartCoroutine(AutoTimerCountDown());
            animator.Play("Base Layer.Rogue_attack_02", 0, 0);

            Vector3 position = new Vector3(body.position.x, body.position.y + 0.5f, 0);
            GameObject projectileObject = Instantiate(Projectile, position, Quaternion.identity);

            DoDamage DMG = projectileObject.GetComponent<DoDamage>();

            if (DMG != null)
            {
                DMG.SetDamage(Damage);
            }

            RougeDagger projectile = projectileObject.GetComponent<RougeDagger>();

            if (InputManager.Instance.joystick.Horizontal == 0 || InputManager.Instance.joystick.Vertical == 0)
            {
                if (transform.localScale.x >= 0)
                {
                    Vector3 direction = new Vector3(1, 0, 0);

                    projectile.Launch(direction * 2f, 100, 3f);
                }
                else
                {
                    Vector3 direction = new Vector3(-1, 0, 0);

                    projectile.Launch(direction * 2f, 100, 3f);
                }
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

                Vector3 direction = new Vector3(InputManager.Instance.joystick.Horizontal + d, InputManager.Instance.joystick.Vertical + f, 0);

                projectile.Launch(direction * 2f, 100, 3f);
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

    public void Die()
    {
        animator.Play("Base Layer.Rogue_death_01", 0, 0);
        InputManager.Instance.PauseUI(true); InputManager.Instance.Ded.GetComponent<DeadMenu>().Initialize();
        gameObject.GetComponent<Rouge>().enabled = false;
        gameObject.GetComponent<HeroHealth>().enabled = false;
        gameObject.GetComponent<HeroMove>().enabled = false;
    }

    public void Resurrect()
    {
        animator.Play("Base Layer.Rogue_idle_01", 0, 0);
        gameObject.GetComponent<Rouge>().enabled = true;
        gameObject.GetComponent<HeroHealth>().enabled = true;
        gameObject.GetComponent<HeroMove>().enabled = true;

        gameObject.GetComponent<HeroHealth>().HealToFull();
    }

    public void Skill()
    {
        
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

    public GameObject Torso;
    private IEnumerator InvulnerableFromTakingDamage;
    bool isInvulnerableFromDamage = false;

    private IEnumerator DamageTakenInvulnerableCountDown()
    {
        isInvulnerableFromDamage = true;
        gameObject.GetComponent<HeroHealth>().enabled = false;

        float time = 0.5f; float interval = 0.1f;

        Color[] temp = new Color[2];
        temp[0] = Torso.GetComponent<SpriteRenderer>().material.color;
        temp[1] = Color.clear;

        int times = 5; Torso.GetComponent<SpriteRenderer>().material.color = temp[times % 2];

        while (time >= 0)
        {
            if (interval < 0.01f)
            {
                interval = 0.1f;
                times = times - 1;
                if (times < 0)
                    times = 0;
                Torso.GetComponent<SpriteRenderer>().material.color = temp[times % 2];
                continue;
            }

            interval -= Time.deltaTime;
            time -= Time.deltaTime;
            yield return null;
        }

        Torso.GetComponent<SpriteRenderer>().material.color = temp[0]; 
        
        gameObject.GetComponent<HeroHealth>().enabled = true;
        isInvulnerableFromDamage = false;
    }

    public void Ultimate()
    {
        
    }
}
