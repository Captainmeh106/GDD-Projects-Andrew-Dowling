using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Pendulum Settings")]
    public float swingSpeed = 1.5f;
    public float swingAngle = 75f;

    [Tooltip("Set the phase offset in degrees (0-360), where 0 = center going right")]
    [Range(0f, 360f)]
    public float startAngleDegrees = 0f;

    private float timeOffset;

    void Awake()
    {
        // Convert degrees to radians for sine function
        timeOffset = Mathf.Deg2Rad * startAngleDegrees;
    }

    void Update()
    {
        float angle = swingAngle * Mathf.Sin(Time.time * swingSpeed + timeOffset);
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}