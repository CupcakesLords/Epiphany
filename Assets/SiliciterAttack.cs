using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliciterAttack : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    float ShootingTimer = 0.5f;
    int Damage = 2;

    Rigidbody2D body;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        body = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ShootingTimer > 0)
        {
            ShootingTimer -= Time.deltaTime;
            return;
        }
        else
        {
            Launch();
            ShootingTimer = 0.5f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }

    public void Launch()
    {
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
    }

}
