using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;
    bool shooted;
    public LayerMask layerMask;
    Collider m_Collider;
    RaycastHit m_Hit;
    public float scaleFactor = 10f;
    public float timer = 5f;
    public float timeUntilNextShot = 8f;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 30.0f;
        m_Speed = 20.0f;
        m_Collider = GetComponent<Collider>();
        shooted = false;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale * scaleFactor, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, layerMask.value);
        //m_HitDetect = Physics.BoxCast(m_Collider.bounds.center,);
        if (m_HitDetect && timer > timeUntilNextShot)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
            //if (!shooted)
            //{
               // shooted = true;
                //Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //}
            timer = 0f;
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale * scaleFactor);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale * scaleFactor);
        }
    }
}
