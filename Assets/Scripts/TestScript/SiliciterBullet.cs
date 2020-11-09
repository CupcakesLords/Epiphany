using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliciterBullet : MonoBehaviour
{
    //private IEnumerator MoveToCoroutine(Vector3 direction, float distance)
    //{
    //    float temp = (direction.x * direction.x + direction.y * direction.y) * 75f * Time.deltaTime;
    //    float step = 0;

    //    while (step < distance)
    //    {
    //        step += temp;
    //        transform.position += direction * 75f * Time.deltaTime;
    //        yield return null;
    //    }

    //    Destroy(gameObject);
    //}
    
    Rigidbody2D rigidbody2d;
    float duration = -1;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
            if(duration <= 0)
            {
                Destroy(gameObject);
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
        if (collision.tag == "Enemy" || collision.tag == "Projectile" || collision.tag == "Hitbox")
            return;
        if(collision.tag == "Player")
        {
            InputManager.Instance.CurrentHero.Die();
            Destroy(gameObject);
        }
    }
}
