using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement components
    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 4f;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    // Interaction coomponents
    PlayerInteraction playerInteraction;
        
    
    void Start()
    {
        // Get movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

   
    void Update()
    {
        // Runs the function that handles all movement
        Move();

        // Runs the function that handles all interaction
        Interact();
    }
        
    public void Interact()
    {
        // Tool interaction
        if (Input.GetButtonDown("Fire1"))
        {
            // Interact
            playerInteraction.Interact();
        }
    }

    public void Move()
    {
        // Get the horizontal and vertical inputs as a number
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Direction in a normalised vector
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        // Check if sprint key is pressed
        if(Input.GetButton("Sprint"))
        {
            // Set the animation to run and increase movespeed
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            // Set the animation to walk and decrease our movespeed
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        // Check if there is movement
        if (dir.magnitude >= 0.1f)
        {
            // Look in that direction
            transform.rotation = Quaternion.LookRotation(dir);

            // Move
            controller.Move(velocity);
        }

        // Animation speed parameter
        animator.SetFloat("Speed", velocity.magnitude);
    }
}
