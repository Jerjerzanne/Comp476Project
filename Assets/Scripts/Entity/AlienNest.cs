using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNest : Entity
{
    #region Variables

    private float searchRadius = 5.0f;
    private float spawnCountTimer = 5.0f;

    public LayerMask entityMask;
    public LayerMask wallMask;

    #endregion

    #region Methods

    private void TargetAcquired(Destructible target)
    {
        if (CurrentInstruction != null)
        {
            Instructions.Push(CurrentInstruction);
        }

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

    public int CountSpawns()
    {
        int count = 0;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        foreach (Collider hit in hitColliders)
        {
            Debug.Log(hit);
            if (hit.gameObject.tag.Contains("Small"))
            {
                count++;
            }
        }

        //Debug.Log("Number of spawns: " + count);
        return count;
    }
    
    #endregion

    #region Functions

    void Update()
    {
        spawnCountTimer -= Time.deltaTime;

        if (spawnCountTimer < 0)
        {
            int count = CountSpawns();
            (_weapon as Gun).rateOfFire = count;
            spawnCountTimer = 5.0f;
        }

        base.Update();
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
    #endregion
}