using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 0.5f;
    public PlayerAttack playerAttack;
    public EnemyAttack enemyAttack;
    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount, string action)
    {
        if (isDead)
            return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death(action);
        }
        else { anim.SetTrigger("Hit"); }
    }

    void Death (string action)
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger ("Dead");
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Destroy(gameObject, 5f);

        // Change Player Attack
        if (action == "left")
        {
            enemyAttack.spellProj.GetComponent<ProjectileContact>().source = "Player";
            playerAttack.l_spellProj = enemyAttack.spellProj;
            playerAttack.l_damage = enemyAttack.damage;
            playerAttack.l_timeBetweenAttacks = enemyAttack.timeBetweenAttacks;
            playerAttack.l_range = enemyAttack.range;
        }
        else
        {
            enemyAttack.spellProj.GetComponent<ProjectileContact>().source = "Player";
            playerAttack.r_spellProj = enemyAttack.spellProj;
            playerAttack.r_damage = enemyAttack.damage;
            playerAttack.r_timeBetweenAttacks = enemyAttack.timeBetweenAttacks;
            playerAttack.r_range = enemyAttack.range;
        }
    }
}
