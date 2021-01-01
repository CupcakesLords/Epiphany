using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    bool isMoving = false;

    private IEnumerator mover;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void SetMoving(bool move)
    {
        isMoving = move;
    }

    public void FollowPlayer()
    {
        float step = Time.deltaTime; 
        Transform player;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            if(Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x, 2) + Mathf.Pow(player.position.y - transform.position.y, 2)) > 2)
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            if (player.position.x - transform.position.x >= 0) // go to the left side
            {
                if (transform.localScale.x > 0) // facing right
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else //go to the right side
            {
                if (transform.localScale.x < 0) // facing left
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void IntervalWalk()
    {
        if(isMoving)
        {
            StopCoroutine(mover);
            isMoving = false;
        }

        float extra_x = Random.Range(-1f, 1f);

        if(extra_x < 0) // go to the left side
        {
            if(transform.localScale.x > 0) // facing right
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else //go to the right side
        {
            if (transform.localScale.x < 0) // facing left
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        mover = MoveToCoroutine(transform, new Vector3(transform.position.x + extra_x, transform.position.y + Random.Range(-1f, 1f), 0), 3f);
        StartCoroutine(mover);
    }

    private IEnumerator MoveToCoroutine(Transform targ, Vector3 pos, float dur)
    {
        isMoving = true;

        float t = 0f;
        Vector3 start = targ.position;
        Vector3 v = pos - start;
        while (t < dur)
        {
            t += Time.deltaTime;
            rigidbody2d.MovePosition(start + v * t / dur);
            yield return null;
        }

        targ.position = pos;

        isMoving = false;
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(isMoving)
        {
            GetComponent<Animator>().Play("Base Layer.Idle", 0, 0);
            StopCoroutine(mover);
            isMoving = false;
        }
    }
}
