using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class SnowballSpell
{
    public GameObject snowball;
    public Transform spellSpawn;
}

[System.Serializable]
public class RaySpell
{
    public int damagePerSpell = 20;
    public float range = 100f;

    public RaycastHit shootHit;
    public Ray shootRay;

    public LineRenderer spellLine;
    public Vector3 pos;
    public Light spellLight;
    public float effectsDisplayTime = 0.15f;

    public void ShootSpell(Transform player_trans, int shootableMask)
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
                enemyHealth.TakeDamage(damagePerSpell, shootHit.point);
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

public class PlayerAttack : MonoBehaviour
{
    // General Properties
    Animator anim;
    float timer;
    int shootableMask;
    public float timeBetweenSpells = 0.15f;

    // Spells
    Dictionary<string, bool> spellMap = new Dictionary<string, bool>();
    RaySpell raySpell;
    SnowballSpell snowballSpell;

    // Need to move
    public GameObject snowball;

    void Awake ()
    {
        // Spell Map
        spellMap.Add("ray", false);
        spellMap.Add("snowball", true);

        // General Properties
        shootableMask = LayerMask.GetMask ("Shootable");
        anim = GetComponent<Animator>();

        // Ray Spell Properties
        raySpell = new RaySpell();
        raySpell.spellLine = GetComponent<LineRenderer>();
        raySpell.spellLight = GetComponent<Light>();
        raySpell.shootRay = new Ray();

        snowballSpell = new SnowballSpell();
        snowballSpell.spellSpawn = transform;
    // other properties set in unity GUI
}


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenSpells && Time.timeScale != 0)
        {
            anim.SetBool("AttackL", true);

            if (spellMap["ray"])
            {
                raySpell.ShootSpell(transform, shootableMask);
            }

            else if (spellMap["snowball"])
            {
                Instantiate(snowball, 
                            snowballSpell.spellSpawn.position + new Vector3(0f, 0.5f,0f), 
                            snowballSpell.spellSpawn.rotation);
            }

            timer = 0;
        }
        else { 
            anim.SetBool("AttackL", false);
        }

        if (timer >= timeBetweenSpells * raySpell.effectsDisplayTime)
        {
            raySpell.DisableEffects ();
        }

    }

    public void DisableAllEffects()
    {
        raySpell.DisableEffects();
    }
}
