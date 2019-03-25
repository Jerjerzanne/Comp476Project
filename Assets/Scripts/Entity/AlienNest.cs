using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNest : Entity
{
    #region Variables
    public float spawnCountTimer = 5.0f;
    public int spawnCount = 0;

    public LayerMask entityMask;
    public LayerMask wallMask;


    #endregion

    #region Methods

    // Update is called once per frame
    void Update()
    {
        spawnCountTimer -= Time.deltaTime;

        if (spawnCountTimer < 0)
        {
            int count = CountSpawns();
            Debug.Log("New Rate of Fire: " + count);
            (_weapon as Gun).rateOfFire = count;
            spawnCountTimer = 5.0f;
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
        /*if (playerSpotted && haveAllies)
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
        */
    }

    public int CountSpawns()
    {
        float searchRadius = 5.0f;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        return hitColliders.Length;
    }

    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.layer == 9)
        {
            playerSpotted = true;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = true;
        }*/
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
        /*if (other.gameObject.layer == 9)
        {
            playerSpotted = false;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = false;
        }*/
    }
    #endregion
}