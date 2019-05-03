using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public int damagePerSpell = 20;
    public float timeBetweenSpells = 0.15f;
    public float range = 100f;

    Animator anim;
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer spellLine;
    Vector3 pos;
    Light spellLight;
    float effectsDisplayTime = 0.15f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        anim = GetComponent<Animator>();
        spellLine = GetComponent<LineRenderer>();
        spellLight = GetComponent<Light>();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenSpells && Time.timeScale != 0)
        {
            anim.SetBool("AttackL", true);
            Shoot();
        }
        else if (Input.GetButton("Fire2") && timer >= timeBetweenSpells && Time.timeScale != 0) 
        {
            anim.SetBool("AttackR", true);
            Shoot();
        }
        else { anim.SetBool("AttackL", false); anim.SetBool("AttackR", false); }

        if(timer >= timeBetweenSpells * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        spellLine.enabled = false;
        spellLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;
        pos = transform.position;
        pos.y += 1;

        spellLight.enabled = true;

        spellLine.enabled = true;
        spellLine.SetPosition(0, pos);

        shootRay.origin = pos;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerSpell, shootHit.point);
            }
            spellLine.SetPosition (1, shootHit.point);
        }
        else
        {
            spellLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
