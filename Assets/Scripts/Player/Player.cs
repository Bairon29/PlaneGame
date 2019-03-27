using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public Joybutton joybutton;

    private bool jump;
    private new Rigidbody rigidbody;
    private Vector3 movement;
    private float speed = 10f;
    private float roationSpeed = 400f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        /*rigidbody.velocity = new Vector3(joystick.Horizontal * 100f, rigidbody.velocity.y, joystick.Vertical * 100f);

        if(!jump && joybutton.pressed)
        {
            jump = true;
            rigidbody.velocity += Vector3.up * 100f;
        }

        if (jump && !joybutton.pressed)
        {
            jump = false;
        }*/
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        Move();
        Turning(h, v);
    }

    void Move()
    {
        movement.Set(0f, 0f, 1f);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.position += transform.forward * speed * Time.deltaTime;
    }

    void Turning(float h, float v)
    {
        if(h != 0 || v != 0)
        {
            Vector3 lookVec = new Vector3(h, 0f, v);
            Quaternion newRotation = Quaternion.LookRotation(lookVec);
            //rigidbody.MoveRotation(newRotation);
            rigidbody.rotation = Quaternion.RotateTowards(this.transform.rotation, newRotation, roationSpeed * Time.deltaTime);
        }
    }
}
