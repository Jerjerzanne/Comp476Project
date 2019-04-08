using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Entity
{
    #region Variables

    private Pods myPod;
    private bool deployed;
    private Vector3 reportPosition;

    [HideInInspector]
    public Barracks barracks;

    #endregion

    #region Properties

    public bool Deployed
    {
        set { deployed = value; }
        get { return deployed; }
    }
    public Vector3 ReportPosition
    {
        set { reportPosition = value; }
        get { return reportPosition; }
    }

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
                        //Instructions.Push(new Goto(this.transform.position, 0,  this));
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                    else if (CurrentInstruction.GetType() == typeof(Chase))
                    {
                        Instructions.Pop();
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

    protected override void RanOutOfInstructions()
    {
        if (CurrentOrder is Patrol)
        {
            CurrentOrder = CurrentOrder;
        }
        else if(Deployed)
        {
            Instructions.Push(new Interact(barracks, this));
            CurrentInstruction = new Goto(barracks.transform.position, 0, this);
            Debug.Log("The soldier should return to the barracks");
        }
    }

    #endregion

    #region Functions

    protected void Update()
    {
        base.Update();
    }

    #endregion

}
