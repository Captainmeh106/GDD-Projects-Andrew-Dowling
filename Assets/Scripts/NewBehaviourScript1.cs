using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerLauncher : MonoBehaviour
{


    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalVelocity = 0f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private float launchVelocity = 0f;
    private bool isLaunched = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            if (isLaunched)
            {
                // Stop the launch effect once grounded
                launchVelocity = 0f;
                isLaunched = false;
            }

            // Example jump input
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // Apply gravity and combine with launch if active
        verticalVelocity += gravity * Time.deltaTime;

        float finalVertical = verticalVelocity + launchVelocity;

        moveDirection.y = finalVertical;

        controller.Move(moveDirection * Time.deltaTime);
    }

    // Called by LaunchPad
    public void Launch(Vector3 force)
    {
        // Only take vertical part of the force for now
        launchVelocity = force.y;
        isLaunched = true;
    }
}
