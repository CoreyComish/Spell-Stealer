using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int demonType;

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 0.5f;

    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;

    public PlayerAttack playerAttack;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount) //, Vector3 hitPoint)
    {
        if (isDead)
            return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
        else { anim.SetTrigger("Hit"); }
    }

    public void Heal (int amount)
    {
        // We can add heal animations and such here later
        // And maybe a slider?
        currentHealth = Mathf.Min(currentHealth + amount, startingHealth);
    }


    void Death ()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger ("Dead");
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Destroy(gameObject, 5f);

        // Change Player Attack
        switch (demonType)
        {
            case (int)Enemies.DemonProjectile:
                playerAttack.activeLeft = (int)Spells.BallProj;
                print("STOLE PROJECTILE");
                break;

            case (int)Enemies.DemonHeal:
                playerAttack.activeLeft = (int)Spells.BasicHeal;
                print("STOLE HEAL");
                break;

            case (int)Enemies.DemonRay:
                playerAttack.activeLeft = (int)Spells.LightRay;
                print("STOLE RAY");
                break;

            case (int)Enemies.DemonAOE:
                playerAttack.activeLeft = (int)Spells.LightRay;
                print("STOLE AOE");
                break;

            default:
                print("ERROR, not valid Demon Type Death");
                break;
        }

    }
}
