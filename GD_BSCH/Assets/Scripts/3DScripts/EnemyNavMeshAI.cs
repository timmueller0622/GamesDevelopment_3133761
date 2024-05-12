using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAI : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public Transform patrolPoint;
    public float velocity;
    public NavMeshAgent agent;
    public Animator anim;
    public bool aggro;
    public bool chasing;
    public bool destinationReached;
    public float destinationReachedDistance;
    public float patrolSpeed;
    public float aggroSpeed;
    public float aggroTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        destinationReached = true;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = agent.velocity.magnitude;
        anim.SetFloat("velocity", velocity);

        if (aggro == false && destinationReached == true)
        {
            agent.speed = patrolSpeed;
            destinationReached = false;
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }

        if (aggro == true && chasing == true)
        {
            agent.speed = aggroSpeed;
            //move towards player using navmesh
            agent.destination = player.position;

        }

        //if distance between enemy and destination is less than destination distance, destinationReached is true
        if (Vector3.Distance(transform.position, agent.destination) < destinationReachedDistance)
        {
            destinationReached = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine("ChaseCooldown");
            aggro = true;
            chasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.destination = player.position;
            chasing = false;
            StopCoroutine("ChaseCooldown");
            StartCoroutine("ChaseCooldown");
        }
    }

    IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(aggroTimer);
        aggro = false;
        destinationReached = true;
    }
}
