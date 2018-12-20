using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntitie
{
    public enum State { Idle, Chasing, Attacking }
    State currentState;


    NavMeshAgent pathfinder;
    Transform target;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttack = 1f;
    float nextAttackTime;

    float collisionRadius;
    float targetCollisionRadius;
    
    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        collisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        // Se puede poner en un Update pero para mejor performance se hace una corrutina.
        StartCoroutine(UpdatePath());
    }

   	void Update () 
    {
		if (Time.time > nextAttackTime) 
        {
			float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
			if (sqrDstToTarget < Mathf.Pow (attackDistanceThreshold + collisionRadius + targetCollisionRadius, 2))
            {
				nextAttackTime = Time.time + timeBetweenAttack;
				StartCoroutine(Attack());
			}

		}

	}

    IEnumerator Attack() {

		currentState = State.Attacking;
		pathfinder.enabled = false;

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (collisionRadius);

		float attackSpeed = 3;
		float percent = 0;

		while (percent <= 1) {

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

			yield return null;
		}

		currentState = State.Chasing;
		pathfinder.enabled = true;
	}

	IEnumerator UpdatePath() {
		float refreshRate = .25f;

		while (target != null) {
			if (currentState == State.Chasing) {
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (collisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
				if (!dead) {
					pathfinder.SetDestination (targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
