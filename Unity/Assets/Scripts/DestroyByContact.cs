using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        // Need to set damage based on projectile damage
        if (other.tag == "Player")
        {
            // Deplete player health
            playerHealth.TakeDamage(10);
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            enemyHealth.TakeDamage(10);
            Destroy(gameObject);
        }

    }
}