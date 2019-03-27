using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    private new Rigidbody rigidbody;
    private Vector3 movement;
    private float speed = 6.5f;
    private float roationSpeed = 800f;
    bool playerInRange;
    public int attackDamage = 10;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (/*enemyHealth.currentHealth > 0 &&*/ playerHealth.currentHealth > 0)
        {
            //nav.SetDestination(player.position);
            Move();
            Turning();
        }
        else
        {
            nav.enabled = false;
        }

        if (playerInRange)
        {
            explode();
        }
    }

    void explode()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        print("player collider with enemy");
        if (playerInRange && playerHealth.currentHealth > 0)
        {
            print("yesssssssss, player collider with enemy");
            playerHealth.TakeDamage(attackDamage);
        }
        Destroy(gameObject);
    }

    void Move()
    {
        movement.Set(0f, 0f, 1f);
        movement = movement.normalized * speed * Time.deltaTime;
        //rigidbody.position += transform.forward * speed * Time.deltaTime;
        nav.Move(transform.forward * speed * Time.deltaTime);
    }

    void Turning()
    {
       // if (h != 0 || v != 0)
        //{
            //Vector3 lookVec = new Vector3(h, 0f, v);
            Quaternion newRotation = Quaternion.LookRotation(player.position - transform.position);
            //rigidbody.MoveRotation(newRotation);
            rigidbody.rotation = Quaternion.RotateTowards(this.transform.rotation, newRotation, roationSpeed * Time.deltaTime);
       //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInRange = false;
        }
    }
}
