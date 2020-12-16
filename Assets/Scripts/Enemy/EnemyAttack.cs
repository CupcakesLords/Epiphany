using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyObject Data;

    public bool isMelee;

    public GameObject projectilePrefab;

    int Damage = 0;

    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Damage = (int)Data.Damage;
    }

    public void Attack()
    {
        if (!isMelee)
        {
            Transform player;
            player = GameObject.FindGameObjectWithTag("Player").transform;

            GameObject projectileObject = Instantiate(projectilePrefab, body.position, Quaternion.identity);

            DoDamage DMG = projectileObject.GetComponent<DoDamage>();

            if (DMG != null)
            {
                DMG.SetDamage(Damage);
            }

            SiliciterBullet projectile = projectileObject.GetComponent<SiliciterBullet>();

            float x = -(body.transform.position.x - player.position.x);
            float y = -(body.transform.position.y - player.position.y);

            Vector3 direction = new Vector3(x, y, 0);

            projectile.Launch(direction, 25, 3f);

            if (x < 0) //attack to the left side
            {
                if (transform.localScale.x > 0) // facing right
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else //attack to the right side
            {
                if (transform.localScale.x < 0) // facing left
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {

        }
    }
}
