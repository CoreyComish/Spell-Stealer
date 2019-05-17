using UnityEngine;
using System.Collections;

enum Enemies
{
    DemonProjectile,
    DemonHeal,
    DemonAOE,
    DemonRay
}

public class EnemyAttack : MonoBehaviour
{

    public int demonType;

    public float timeBetweenAttacks;
    public string attackAnim = "Attack01";

    Animator anim;
    GameObject player;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    public BallSpell ballSpell;
    public BasicHeal basicHeal;

    public int castRange;



    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();

        //ballSpell = new BallSpell(this.gameObject);
        basicHeal = new BasicHeal(this.gameObject);
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) < castRange)
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
        timer = 0f;

        anim.SetTrigger(attackAnim);

        if (demonType == (int)Enemies.DemonProjectile)
        {
            ballSpell.spellSpawn.position += new Vector3(0, 1, 0);
            Instantiate(ballSpell.projObject,
                ballSpell.spellSpawn.position,
                ballSpell.spellSpawn.rotation);
        }

        else if (demonType == (int)Enemies.DemonHeal)
        {
            basicHeal.CastHeal();
        }

        else print("ERROR, not valid demonType, Attack");

    }
}

