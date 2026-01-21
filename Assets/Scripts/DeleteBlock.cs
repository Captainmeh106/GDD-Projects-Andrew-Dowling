using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlock : MonoBehaviour
{
    // Start is called before the first frame update

    public Light lighting;
    

    public void Press()
    {
        lighting.enabled = !lighting.enabled;
        Destroy(gameObject);
    }
}
