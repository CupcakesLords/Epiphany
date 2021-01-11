using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliciterBullet : MonoBehaviour, Enemy
{ 
    Rigidbody2D rigidbody2d;
    float duration = -1;

    Animator animator;

    public GameObject Explosion;

    public void OnDeadDropLoot()
    {

    }

    public void OnDestruction()
    {
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

    public void TakeDamage()
    {

    }

    public void Attack()
    {

    }

    public void Die()
    {
        OnDestruction();
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
            if(duration <= 0)
            {
                OnDestruction();
            }
        }
    }

    public void Launch(Vector3 Direction, float distance, float time)
    {
        duration = time;
        if(rigidbody2d == null)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();   
        }
        rigidbody2d.AddForce(Direction * distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Projectile" || collision.tag == "Hitbox" || collision.tag == "Drop")
            return;
        if (collision.tag == "Player")
        {
            HeroHealth temp = collision.GetComponent<HeroHealth>();
            if (!temp.isActiveAndEnabled)
            {
                OnDestruction();
                return;
            }
            if (!temp.IsDead())
            {
                temp.TakeDamage(gameObject.GetComponent<DoDamage>().damage);
            }
            OnDestruction();
        }
        else
        {
            OnDestruction();
        }
    }
}
