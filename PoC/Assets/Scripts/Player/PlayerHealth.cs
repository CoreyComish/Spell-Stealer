using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerAttack = GetComponentInChildren <PlayerAttack> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
            anim.SetBool("Damaged", true);
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            anim.SetBool("Damaged", false);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        playerAttack.DisableEffects ();
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
