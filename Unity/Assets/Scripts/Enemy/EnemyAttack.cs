using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public string attackAnim = "Attack01";

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    public GameObject snowball;
    public Transform spellSpawn;
    public int spellDistance;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    /*
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    */

    void Update ()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) < spellDistance)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            anim.SetTrigger(attackAnim);

            Instantiate(snowball, spellSpawn.position, spellSpawn.rotation);

            //playerHealth.TakeDamage (attackDamage);
        }
    }
}