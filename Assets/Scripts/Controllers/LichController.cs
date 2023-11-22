using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LichController : Controller
{
    [SerializeField]
    private float castDelay;
    private float lastCastTime;

    public List<BossAbility> abilities;
    public List<BossAbility> defensiveAbilities;
    public GameObject shield;

    [SerializeField]
    int activeCrystals;
    bool hasShield;

    public GameObject teleportationPoints;
    List<Vector2> tppoints = new List<Vector2>();

    int currentPoint = 0;

    bool first = false;

    void Start()
    {
        base.Start();
        hasShield = true;
        activeCrystals = 3;

        tppoints.Add(this.transform.position);
        foreach (Transform child in teleportationPoints.transform)
        {
            tppoints.Add(child.position);
        }

        lastCastTime = Time.time;
        first = false;
    }

    private void Update()
    {
       if (!first)
       {
           BossAbility choice = Instantiate(abilities[2]);
           choice.AbilityBehavior(this.gameObject);
           first = true;
       } else if (Time.time - lastCastTime >= castDelay)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            float distance = (player.transform.position - this.transform.position).magnitude;
            
            if (distance < 5 && !hasShield) {
                ChooseDefensive();
            } else {
                ChooseAttack();
            }
            lastCastTime= Time.time;
        }
    }

    public void RemoveCrystal()
    {
        activeCrystals--;
        if (activeCrystals == 0)
        {
            this.RemoveShield();
        }
    }

    private void RemoveShield()
    {
        hasShield = false;
        Enemy enemyComp = GetComponent<Enemy>();
        enemyComp.enabled = true;
        castDelay = castDelay / 1.5f;
        Destroy(shield);
    }

    public bool HasShield(){
        return hasShield;
    }
    public void ChooseAttack()
    {
        if (activeCrystals > 0)
        {
            // Phase 1 stuff
            int i = Random.Range(0, 100);
            if (i <= 15)
            {
                i = 2;
             } else if (i <= 75)
            {
                i = 0;
            } else
            {
                i = 1;
            }
            BossAbility choice = Instantiate(abilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
        else
        {
            // Phase 2 stuff
            int i = Random.Range(0, 100);
            if (i <= 60)
            {
                i = 0;
            } else
            {
                i = 1;
            }
            BossAbility choice = Instantiate(abilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
    }

    public void ChooseDefensive()
    {
        int i = Random.Range(0, 100);
        if (i <= 35)
        {
            i = 0;
        }
        else if (i <= 75)
        {
            i = 1;
        }
        else
        {
            i = 2;
        }

        if (i < defensiveAbilities.Count)
        {
            BossAbility choice = Instantiate(defensiveAbilities[i]);
            choice.AbilityBehavior(this.gameObject);
        }
        else
        {
            TeleportFromPlayer();
        }
    }

    //honestly can be changed into an ability that tps to a random point later when theres time
    private void TeleportFromPlayer()
    {
        currentPoint = (currentPoint + 1) % tppoints.Count;
        this.transform.position = tppoints[currentPoint];
    }
}
