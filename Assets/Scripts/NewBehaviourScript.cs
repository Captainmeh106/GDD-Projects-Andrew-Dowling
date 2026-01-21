using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    
    public float launchForce = 20f;
    public Vector3 launchDirection = Vector3.up;
    public string targetTag = "Player"; // Only launch objects with this tag

    
}
