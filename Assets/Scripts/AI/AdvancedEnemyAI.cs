using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedEnemyAI : MonoBehaviour {

    public int health = 100;
    public float viewRange = 25f;
    public float attackRange = 5f;

    public float thinkTimer = 5f;
    private float thinkTimerStart;
    public float randomUnityCircleRadius = 10f;
    private NavMeshAgent agent;
    public Transform playerTransform;
    public Transform playerTransformDist;
    private float distToPlayer;
    public float eyeHeight;
    public Vector3 newPos;
    public float distToAttack = 1.5f;




    private bool isChasing = false;
    private bool isAttacking = false;
    private float attackTimer = 1f;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        thinkTimerStart = thinkTimer;
        playerTransformDist = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        CheckHealth();
        distToPlayer = Vector3.Distance(transform.position, playerTransformDist.position);
        Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y + eyeHeight, transform.position.z);
        Ray ray = new Ray(eyePosition, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * viewRange);
        RaycastHit hitInfo;


        thinkTimer -= Time.deltaTime;

        if(distToPlayer <= distToAttack && isChasing == true)
        {
            isAttacking = true;
        }
        if(isAttacking == true)
        {
            attackTimer -= Time.deltaTime;
        }

        if (Physics.Raycast(ray, out hitInfo, attackRange))
        {
            if (attackTimer <= 0)
            {
                Attack();
            }
        }
            if (thinkTimer <= 0)
        {
            Think();
            thinkTimer = thinkTimerStart;
        }
       
        if(Physics.Raycast(ray, out hitInfo, viewRange))
        {
            if(hitInfo.collider.tag == "Player")
            {
                if(isChasing == false)
                {
                    isChasing = true;
                    if(playerTransform == null)
                    {
                        playerTransform = hitInfo.collider.GetComponent<Transform>();
                    }
                    
                }
            }


            
        }


        

        if (isChasing == true)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if(playerTransform != null)
        {
            isChasing = true;
        }
    }

    private void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Think()
    {
        if (!isChasing)
        {
             newPos = transform.position + new Vector3(Random.insideUnitCircle.x * randomUnityCircleRadius, 0, Random.insideUnitCircle.y * randomUnityCircleRadius);
            agent.SetDestination(newPos);
        }
    }
    private void Attack()
    {

    }
}
