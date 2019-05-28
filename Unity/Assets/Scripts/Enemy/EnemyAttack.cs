using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public string attackAnim;
    public GameObject spellProj;
    public float timeBetweenAttacks;
    public int damage;
    public int range;
    public Vector3 spellSpawn;
    Animator anim;
    GameObject player;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        spellProj.GetComponent<ProjectileContact>().source = "Enemy";
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) < range)
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

    }

    void Attack ()
    {
        spellProj.GetComponent<ProjectileContact>().source = "Enemy";
        anim.SetTrigger(attackAnim);
        timer = 0f;
        Instantiate(spellProj, transform.position + spellSpawn, transform.rotation);
    }
}

