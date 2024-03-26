using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Detect whether the player is left or right of the enemy gameobject, then move toward the player.
 * If no player detected, stay idle or patrol left and right (You can set it up so that the enemy moves between patrol points 
 * OR uses collision and groundcheck to move left/right until it hits wall or detects cliff
 * For bonus points, set up correct animations
 */

public class EnemyAIScript : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public Transform playerLocation;
    public float initialSpeed;
    public float aggroSpeed;
    public AIBehavior behavior;
    public float detectTime;
    public float aggroTime;
    public bool detecting;
    public bool aggro;

    public enum AIBehavior
    {
        Idle,
        Patrol,
        DetectPlayer,
        ChasePlayer,
        AggroIdle
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (behavior) {
            case AIBehavior.Idle:
                //Do Nothing
                speed = 0;
                aggro = false;
                break;
            case AIBehavior.Patrol:
                speed = initialSpeed; 
                break;
            case AIBehavior.DetectPlayer:
                speed = 0;
                aggro = true;
                break;
            case AIBehavior.ChasePlayer:
                speed = aggroSpeed;
                aggro = true;
                break;
            case AIBehavior.AggroIdle:
                speed = 0;
                aggro = true;
                break;
                  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !aggro)
        {
            StopCoroutine("AggroTimer");
            StartCoroutine("DetectTime");
        }
        if (other.CompareTag("Player") && aggro)
        {
            StopCoroutine("AggroTimer");
            behavior = AIBehavior.ChasePlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (detecting)
            {
                StopCoroutine("DetectTime");
                behavior = AIBehavior.Idle;
                detecting = false;
            }
            
            else if (behavior == AIBehavior.ChasePlayer)
            {
                StartCoroutine("AggroTimer");
            }
        }
    }
    IEnumerator DetectTime()
    {
        detecting = true;
        behavior = AIBehavior.DetectPlayer;
        yield return new WaitForSeconds(detectTime);
        if (behavior != AIBehavior.Idle)
            behavior = AIBehavior.ChasePlayer;
        detecting = false; 
    }

    IEnumerator AggroTimer()
    {
        behavior = AIBehavior.AggroIdle;
        yield return new WaitForSeconds(aggroTime);
        if (behavior != AIBehavior.ChasePlayer) 
        {
            behavior = AIBehavior.Idle;
        }
    }

}
