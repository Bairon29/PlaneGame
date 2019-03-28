using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracker : MonoBehaviour
{

    public float timeUntilExplode = 10f;
    public int attackDamage = 10;
    NavMeshAgent nav;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer;
    NavMeshPath path;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    } 
    
    // Update is called once per frame
    void FixedUpdate()
    {

        if (/*enemyHealth.currentHealth > 0 &&*/ playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.transform.position);
        }
        else
       {
            nav.enabled = false;
       }
        timer += Time.deltaTime;
        if (playerInRange)
        {
            explode();
        } else if(timer > timeUntilExplode)
        {
            explode();
        } 
    }

    void explode()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        if(playerInRange && playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
