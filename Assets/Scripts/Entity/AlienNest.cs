using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNest : Entity
{
    #region Variables

    public LayerMask entityMask;

    #endregion

    #region Methods



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

    private IEnumerator CountSpawns()
    {
        yield return new WaitForSeconds(5.0f);

        float searchRadius = 5.0f;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        (_weapon as Gun).rateOfFire = hitColliders.Length;
    }

    #endregion

    #region Functions

    protected void Start()
    {
        StartCoroutine("CountSpawns");
    }

    protected void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    #endregion
}
