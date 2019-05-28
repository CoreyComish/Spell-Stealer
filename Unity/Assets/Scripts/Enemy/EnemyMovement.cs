using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    public Vector3 enemyDirection;
    public int aggroRange;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
        anim.SetBool("Moving", false);
    }

    void Update ()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= aggroRange || enemyHealth.currentHealth < enemyHealth.startingHealth)
            {
                anim.SetBool("Moving", true);
                nav.SetDestination(player.position);
                enemyDirection = transform.forward;
            }
        }
        else
        {
            anim.SetBool("Moving", false);
            nav.enabled = false;
        }
    }
}
