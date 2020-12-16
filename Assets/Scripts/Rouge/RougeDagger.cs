using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeDagger : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float duration = -1;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Launch(Vector3 Direction, float distance, float time)
    {
        duration = time;
        if (rigidbody2d == null)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        rigidbody2d.AddForce(Direction * distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Projectile" || collision.tag == "Hitbox")
            return;
        if (collision.tag == "Enemy")
        {
            EnemyHealth a = collision.GetComponent<EnemyHealth>();
            if (a != null)
                a.TakeDamage(gameObject.GetComponent<DoDamage>().damage);
            Destroy(gameObject);
        }
    }
}
