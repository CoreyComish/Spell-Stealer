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

    public BallSpell ballSpell;
    public int spellDistance;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        snowballHitbox = ballSpell.projObject.GetComponent<SphereCollider>();
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

            if (attackAnim == "Attack01")
            {
                Instantiate(ballSpell.projObject, 
                            ballSpell.spellSpawn.position, 
                            ballSpell.spellSpawn.rotation);
            }

            if (attackAnim != "Attack01")
            {
                playerHealth.TakeDamage(attackDamage);
            }

        }
    }
}