using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNest : Entity
{
    #region Variables
    public GameObject alienSmall;
    public GameObject alienQueen;
    public float queenTimer = 60;
    public float spawnTimer = 10;
    public float spawnCountTimer = 5;
    bool playerSpotted = false;
    bool haveAllies = false;
    bool queenDetected = false;
    public LayerMask entityMask;
    public LayerMask wallMask;


    #endregion

    #region Methods

    protected void Start()
    {
        //StartCoroutine("CountSpawns");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        /*queenTimer -= Time.deltaTime;
        spawnCountTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            Spawn();
        }*/
        if (spawnCountTimer < 0)
        {
            CountSpawns();
            spawnCountTimer = 5;
        }

        base.Update();
    }

    private void TargetAcquired(Destructible target)
    {
        if (CurrentInstruction != null)
        {
            Instructions.Push(CurrentInstruction);
        }

        _navMeshAgent.SetDestination(this.transform.position);
        Instructions.Push(new Attack(target, this));
        CurrentInstruction = Instructions.Pop();
    }

    protected override void DetectionReaction(GameObject[] target)
    {
        foreach (GameObject potentialEnemy in target)
        {
            Destructible enemy = potentialEnemy.GetComponent<Destructible>();
            if (enemy != null)
            {
                if (!enemy.IsDead())
                {
                    if (CurrentInstruction == null)
                    {
                        Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                    else if (CurrentInstruction.GetType() != typeof(Attack))
                    {
                        Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                }
            }
        }
    }

    private void Spawn()
    {
        if (playerSpotted && haveAllies)
        {
            //switch to atk mode
        }
        if (!playerSpotted)
        {
            if (spawnTimer < 0)
            {
                GameObject alienSClone = (GameObject)Instantiate(alienSmall, transform.position, Quaternion.identity);
                spawnTimer = 3;
            }
        }
        if (!queenDetected)
        {
            if (queenTimer < 0)
            {
                GameObject alienQClone = (GameObject)Instantiate(alienQueen, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z + 1.0f), Quaternion.identity);
                queenTimer = 6;
            }
        }

    }

    private void CountSpawns()
    {
        float searchRadius = 5.0f;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        Debug.Log("New Rate of Fire: " + hitColliders.Length);
        (_weapon as Gun).rateOfFire = hitColliders.Length;
    }
    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerSpotted = true;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)    //or soldier
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, other.gameObject.transform.position - this.transform.position, out hit, Mathf.Infinity, wallMask))
            {
                DetectionReaction(new GameObject[] { other.gameObject });
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerSpotted = false;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = false;
        }
    }
    #endregion
}