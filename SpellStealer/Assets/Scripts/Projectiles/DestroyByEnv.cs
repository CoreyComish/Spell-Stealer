using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByEnv : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Env")
        {
            Destroy(gameObject);
        }
    }
}
