using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    [SerializeField] AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if(Input.GetButtonDown("Jump")&& IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
       rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.y);
       jumpSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Enemy Head"))
       {
         Destroy(collision.transform.parent.gameObject);
         Jump();
       } 
    }

    bool IsGrounded()
    {
       return Physics.CheckSphere(groundCheck.position,0.1f,ground);
    }
   
}
