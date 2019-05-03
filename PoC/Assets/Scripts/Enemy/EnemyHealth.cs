using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 0.5f;

    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
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


    void Death ()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger ("Dead");
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Destroy(gameObject, 5f);
    }
}
