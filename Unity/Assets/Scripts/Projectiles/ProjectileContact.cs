using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContact : MonoBehaviour
{
    EnemyHealth enemyHealth;
    PlayerHealth playerHealth;
    EnemyAttack enemyAttack;
    PlayerAttack playerAttack;
    public string source;

    // THis probably needs to be split into two different classes for Player/Enemy projectiles
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemyAttack = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAttack>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && source == "Player")
        {
            print("Enemy HIT!");
            enemyHealth = other.GetComponent<EnemyHealth>();
            if (playerAttack.l == true)
            {
                enemyHealth.TakeDamage(playerAttack.l_damage, "left");
                Destroy(gameObject);
            }
            else
            {
                enemyHealth.TakeDamage(playerAttack.r_damage, "right");
                Destroy(gameObject);
            }
        }

        if (other.tag == "Player" && source == "Enemy")
        {
            Debug.Log(other.material.name);
            playerHealth.TakeDamage(enemyAttack.damage);
            Destroy(gameObject);
        }

    }
}