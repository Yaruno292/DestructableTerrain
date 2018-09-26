using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 20f;
    private float moveVert;
    private float moveHor;

    private bool grounded = false;

    private Vector3 movement;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        moveVert = Input.GetAxis("Vertical");
        moveHor = Input.GetAxis("Horizontal");


        movement = new Vector3(moveHor * speed, 0.0f, moveVert * speed);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        rb.MovePosition(transform.position + movement);

        if(transform.position.y <= -50)
        {
            rb.MovePosition(transform.position + new Vector3(0,200,0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if(grounded == true)
        {
            rb.AddForce(transform.up * 275f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Ground" && grounded == false)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground" && grounded == true)
        {
            grounded = false;
        }
    }
}
