using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour {

    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    public int detentionPlayerDistance = 6;
    public bool isRange = false;
    private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
        player = GameManager.instance.Player.transform;
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        enemyHealth = this.GetComponent<EnemyHealth>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(player.position, this.transform.position) < detentionPlayerDistance)
        {
            if(!GameManager.instance.GameOver && enemyHealth.IsAlive)
            {
                if(Vector3.Distance(player.position, this.transform.position) > nav.stoppingDistance || !isRange)
                {

                    if (nav.isOnNavMesh)
                    {
                        nav.SetDestination(player.position);
                    }

                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                }
                else
                {
                    RotateTowards(player);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", true);
                }

            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }

        if(GameManager.instance.GameOver || !enemyHealth.IsAlive)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            nav.enabled = false;
        }
	}

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //Only update only Y axis rotation.
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * nav.angularSpeed);
    }

}
