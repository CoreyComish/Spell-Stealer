using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 50;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public Image healImage;

    public float flashSpeed = 5f;
    public Color damageflashColor = new Color(1f, 0f, 0f, 0.1f);
    public Color healflashColor = new Color(0f, 1f, 0f, 0.1f);

    Animator anim;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    bool isDead;
    bool damaged;
    bool healed;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerAttack = GetComponentInChildren <PlayerAttack> ();
        currentHealth = startingHealth;
    }

    void Update ()
    {
        // Damaged animation
        if(damaged)
        {
            damageImage.color = damageflashColor;
            anim.SetTrigger("Damaged");
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        // Healed animation
        if (healed)
        {
            healImage.color = healflashColor;
            // [JL] We could pick a different animation here
            anim.SetTrigger("Damaged");
        }
        else
        {
            healImage.color = Color.Lerp(healImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        healed = false;



        if (currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    public void Heal (int amount)
    {
        healed = true;
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startingHealth);
        healthSlider.value = currentHealth;
    }

    public void TakeDamage (int amount)
    {

        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;
        playerAttack.DisableAllEffects();
        anim.SetTrigger ("Die");
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        RestartLevel();
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
