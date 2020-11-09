using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siliciter : MonoBehaviour
{
    float ShootingTimer = 0.5f;
    public GameObject projectilePrefab;

    private void Update()
    {
        if(ShootingTimer > 0)
        {
            ShootingTimer -= Time.deltaTime;
            return;
        }
        else {
            Launch();
            ShootingTimer = 1;
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        SiliciterBullet projectile = projectileObject.GetComponent<SiliciterBullet>();
        projectile.Launch(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), 500, 5f);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
