﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

enum Spells
{
    LightRay,
    BallProj,
    BasicHeal
}

// Spell superclass
[System.Serializable]
public class Spell
{
    public int range;
    public int spellType;
}

// Ray Spell superclass
[System.Serializable]
public class RaySpell : Spell
{
    public RaycastHit shootHit;
    public Ray shootRay;
    public LineRenderer spellLine;
    public Vector3 pos;
    public Light spellLight;
    public float effectsDisplayTime;

    public int damage;

    public void ShootRay(Transform player_trans, int shootableMask)
    {
        pos = player_trans.position;
        pos.y += 1;

        spellLight.enabled = true;

        spellLine.enabled = true;
        spellLine.SetPosition(0, pos);

        shootRay.origin = pos;
        shootRay.direction = player_trans.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); //, shootHit.point);
            }
            spellLine.SetPosition(1, shootHit.point);
        }
        else
        {
            spellLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void DisableEffects()
    {
        spellLine.enabled = false;
        spellLight.enabled = false;
    }
}

// Projectile Spell superclass
[System.Serializable]
public class ProjectileSpell : Spell
{
    public Transform spellSpawn;
    public GameObject projObject;
}

// 1. Ball 
[System.Serializable]
public class BallSpell : ProjectileSpell
{
    public BallSpell(GameObject player)
    {
        spellSpawn = player.transform;
        spellSpawn.position += new Vector3(0f, 0.5f, 0f);
    }
}

// 2. Light Ray
[System.Serializable]
public class LightRay : RaySpell
{
    public LightRay(GameObject player)
    {
        damage = 20;
        range = 100;
        effectsDisplayTime = 0.15f;
        spellLine = player.GetComponent<LineRenderer>();
        spellLight = player.GetComponent<Light>();
        shootRay = new Ray();
    }

}

// 3. Basic Heal
[System.Serializable]
public class BasicHeal : Spell
{
    public int amountHealed;
    public GameObject target;

    public BasicHeal(GameObject target)
    {
        range = 0;
        spellType = 3;
        amountHealed = 10;
        this.target = target;
    }

    public void CastHeal()
    {
        if (target.tag == "Player")
        {
            PlayerHealth ph = target.GetComponent<PlayerHealth>();
            ph.Heal(amountHealed);
        }

        else if (target.tag == "Enemy")
        {
            EnemyHealth eh = target.gameObject.GetComponent<EnemyHealth>();
            eh.Heal(amountHealed);
        }

    }
}

public class PlayerAttack : MonoBehaviour
{
    // General Properties
    Animator anim;
    float timer;
    int shootableMask;
    public float timeBetweenSpells = 0.15f;

    // Spells
    public int activeLeft;
    public LightRay lightRay;
    public BallSpell ballSpell;
    public BasicHeal basicHeal;

    // Seems like we need to define this here. I can't figure out how to 
    // properly set the class object from Unity.
    public GameObject ballSpellPrefab;

    void Awake()
    {
        // General Properties
        shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
        activeLeft = (int)Spells.LightRay;
        //activeLeft = (int)Spells.BallProj;
        //activeLeft = (int)Spells.BasicHeal;

        // Initalize Spell Class Instances
        lightRay = new LightRay(this.gameObject);
        ballSpell = new BallSpell(this.gameObject);
        ballSpell.projObject = ballSpellPrefab;
        basicHeal = new BasicHeal(this.gameObject);
    }

    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenSpells && Time.timeScale != 0)
        {
            anim.SetBool("AttackL", true);

            switch (activeLeft)
            {
                case (int)Spells.LightRay:
                    lightRay.ShootRay(transform, shootableMask);
                    break;

                case (int)Spells.BallProj:
                    // Instantiate(ballSpell.projObject, 
                    Instantiate(ballSpell.projObject,
                                ballSpell.spellSpawn.position,
                                ballSpell.spellSpawn.rotation);
                    break;

                case (int)Spells.BasicHeal:
                    basicHeal.CastHeal();
                    break;
            }

            timer = 0;
        }
        else { 
            anim.SetBool("AttackL", false);
        }

        // Need to figure out where to do this
        if (timer >= timeBetweenSpells * lightRay.effectsDisplayTime)
        {
            lightRay.DisableEffects ();
        }

    }

    public void DisableAllEffects()
    {
        lightRay.DisableEffects();
    }
}