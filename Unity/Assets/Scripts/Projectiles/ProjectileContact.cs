using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : MonoBehaviour
{
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;
    public string source;

    // THis probably needs to be split into two different classes for Player/Enemy projectiles
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && source == "Player")
        {
            print("Enemy HIT!");
            enemyHealth.TakeDamage(30);
            Destroy(gameObject);
           
        }

        // Need to set damage based on projectile damage
        if (other.tag == "Player" && source == "Enemy")
        {
            playerHealth.TakeDamage(10);
            Destroy(gameObject);
        }

    }
}