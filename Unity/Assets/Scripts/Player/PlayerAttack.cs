using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    float timer;
    int shootableMask;
    public GameObject l_spellProj;
    public float l_timeBetweenAttacks;
    public int l_damage;
    public int l_range;
    public GameObject r_spellProj;
    public float r_timeBetweenAttacks;
    public int r_damage;
    public int r_range;
    Vector3 spellSpawn = new Vector3(0f, 2f, 0f);
    public bool l;
    public bool r;
    public Sprite lSpellImage;
    public Sprite rSpellImage;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        GameObject lSpellUI = GameObject.Find("Left Spell");
        lSpellUI.GetComponent<Image>().sprite = l_spellProj.GetComponent<Image>().sprite;
        GameObject rSpellUI = GameObject.Find("Right Spell");
        rSpellUI.GetComponent<Image>().sprite = r_spellProj.GetComponent<Image>().sprite;


        if (Input.GetButton("Fire1") && timer >= l_timeBetweenAttacks && Time.timeScale != 0f)
        {
            anim.SetTrigger("AttackL");
            timer = 0f;
            l_spellProj.GetComponent<ProjectileContact>().source = "Player";
            Instantiate(l_spellProj, transform.position + spellSpawn, transform.rotation);
            l = true;
            r = false;
        }
        else if (Input.GetButton("Fire2") && timer >= r_timeBetweenAttacks && Time.timeScale != 0f)
        {
            anim.SetTrigger("AttackR");
            timer = 0f;
            r_spellProj.GetComponent<ProjectileContact>().source = "Player";
            Instantiate(r_spellProj, transform.position + spellSpawn, transform.rotation);
            r = true;
            l = false;
        }
    }
}