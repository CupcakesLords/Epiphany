using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerigelOrbit : MonoBehaviour
{
    public GameObject Center;

    [HideInInspector]
    public float distance;

    Rigidbody2D rigidbody2d;

    

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Center.transform.position.x, 2) + Mathf.Pow(transform.position.y - Center.transform.position.y, 2)); 
    }

    public void ExpandOrbit()
    {
        float x = transform.position.x - Center.transform.position.x;
        float y = transform.position.y - Center.transform.position.y;
        if (x >= 0)
            x = transform.position.x + 2f;
        else
            x = transform.position.x - 2f;
        if (y >= 0)
            y = transform.position.y + 2f;
        else
            y = transform.position.y - 2f;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, y, 0), Time.deltaTime);
    }

    public void BackToOrbit()
    {
        if (IsReturning)
        {
            StopCoroutine(ReturnProtocol);
            IsReturning = false;
        }

        ReturnProtocol = Return();
        StartCoroutine(ReturnProtocol);
    }

    bool IsReturning = false;
    private IEnumerator ReturnProtocol;

    private IEnumerator Return()
    {
        IsReturning = true;
        while (CalculateIfBackToOrbit())
        {
            transform.position = Vector3.MoveTowards(transform.position, Center.transform.position, Time.deltaTime * 1.5f);
            yield return null;
        }
        IsReturning = false;
    }

    bool CalculateIfBackToOrbit()
    {
        float new_distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Center.transform.position.x, 2) + Mathf.Pow(transform.position.y - Center.transform.position.y, 2));
        return !Mathf.Approximately(distance, new_distance) && new_distance > distance;
    }

    void Update()
    {
        if (Center == null)
            return;
   
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(Center.transform.position, axis, 100 * Time.deltaTime);
        
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z += 5;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Projectile" || collision.tag == "Hitbox")
            return;
        if (collision.tag == "Player")
        {
            HeroHealth temp = collision.GetComponent<HeroHealth>();
            if (!temp.isActiveAndEnabled)
            {
                return;
            }
            if (!temp.IsDead())
            {
                temp.TakeDamage(gameObject.GetComponent<DoDamage>().damage);
            }
        }
    }
}
