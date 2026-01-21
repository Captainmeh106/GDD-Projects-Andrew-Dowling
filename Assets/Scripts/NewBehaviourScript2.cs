using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f; 
    private Rigidbody rb;
    public Vector3 launchDirection = new Vector3(0, 1, 0);
    public float launchForce = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.right * moveSpeed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.left * moveSpeed, ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
               
                Vector3 normalizedDirection = launchDirection.normalized;

                
                rb.AddForce(normalizedDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
}
