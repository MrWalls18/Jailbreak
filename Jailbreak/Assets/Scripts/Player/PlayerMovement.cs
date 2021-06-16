using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SingletonPattern<PlayerMovement>
{
    [SerializeField] private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public float walkingSpeed;
    public float gravity;
    public float groundDistance;
    public float jumpHeight;
    public Transform groundCheck;    
    public LayerMask groundMask;

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput;
        controller.Move(move * walkingSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}