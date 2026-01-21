using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class wallmove : MonoBehaviour
{
    [Header("Timing")]
    public float waittimer = 2f;         // Wait time before starting each cycle
    public float stopendtimer = 1f;      // Pause duration at each end

    [Header("Movement")]
    public float forwardSpeed = 2f;      // Speed when moving forward
    public float backwardSpeed = 3f;     // Speed when moving back
    public float movedistance = 5f;      // Total distance to move in one direction

    private float location = 0f;         // Tracks how far the object has moved in current direction
    private float movespeed = 0f;        // Current move speed applied to object

    private void Start()
    {
        StartCoroutine(MovementLoop());
    }

    private void Update()
    {
        // Move the object based on current speed
        Vector3 moveVector = new Vector3(0, 0, movespeed);
        transform.position += moveVector * Time.deltaTime;

        // Track how far we've moved in the current direction
        location += Mathf.Abs(movespeed) * Time.deltaTime;
    }

    private IEnumerator MovementLoop()
    {
        while (true)
        {
            // Wait before starting forward move
            yield return new WaitForSeconds(waittimer);

            // Move forward
            movespeed = forwardSpeed;
            location = 0f;
            yield return new WaitUntil(() => location >= movedistance);

            // Stop
            movespeed = 0f;
            yield return new WaitForSeconds(stopendtimer);

            // Move backward
            movespeed = -backwardSpeed;
            location = 0f;
            yield return new WaitUntil(() => location >= movedistance);

            // Stop
            movespeed = 0f;
            yield return new WaitForSeconds(stopendtimer);
        }
    }
}