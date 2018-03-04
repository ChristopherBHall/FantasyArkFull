using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour {

    private NavMeshAgent agent;
    public int health = 100;

    private Transform playerTransform;
    private bool isChasing = false;

	// Use this for initialization
	private void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if (isChasing == true)
        {
            agent.SetDestination(playerTransform.position);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
         if(target.tag == "Player")
        {
            playerTransform = target.transform;
            isChasing = true;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }


}
