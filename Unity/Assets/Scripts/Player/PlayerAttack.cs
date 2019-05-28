using UnityEngine;
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

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
        l_spellProj.GetComponent<ProjectileContact>().source = "Player";
        r_spellProj.GetComponent<ProjectileContact>().source = "Player";
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= l_timeBetweenAttacks && Time.timeScale != 0f)
        {
            anim.SetTrigger("AttackL");
            timer = 0f;
            Instantiate(l_spellProj, transform.position + spellSpawn, transform.rotation);
            l = true;
            r = false;
        }
        else if (Input.GetButton("Fire2") && timer >= r_timeBetweenAttacks && Time.timeScale != 0f)
        {
            anim.SetTrigger("AttackR");
            timer = 0f;
            Instantiate(r_spellProj, transform.position + spellSpawn, transform.rotation);
            r = true;
            l = false;
        }
    }
}