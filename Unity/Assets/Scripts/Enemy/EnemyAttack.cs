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
    SphereCollider snowballHitbox;
    CapsuleCollider playerHitbox;
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
        snowballHitbox = snowball.GetComponent<SphereCollider>();
        playerHitbox = GetComponent<CapsuleCollider>();
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

        if (playerHealth.currentHealth <= 0)
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

            if (snowballHitbox.bounds.Intersects(playerHitbox.bounds))
            {
                playerHealth.TakeDamage(10);
            }

            /*
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                playerHealth.TakeDamage (attackDamage);
            }
            */
        }
    }
}