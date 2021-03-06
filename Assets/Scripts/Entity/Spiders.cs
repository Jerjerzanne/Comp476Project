﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spiders : Entity
{

    #region Variables
    [Header("Wander Setting")]
    public Vector3 nestPosition;
    public float timer;
    public float minRange;
    public float visionRange;
    public float nestRange;

    #endregion

    #region Methods

    private void TargetAcquired(Destructible target)
    {
        if (CurrentInstruction != null)
        {
            Instructions.Push(CurrentInstruction);
        }

        _navMeshAgent.SetDestination(this.transform.position);
        Instructions.Push(new Attack(target, "Alien", this));
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
        Instructions.Push(new Wander(nestPosition,timer, minRange, visionRange, nestRange,this));
    }

    public override void TakeDamage(int damage, Vector3 origin = default(Vector3))
    {
        base.TakeDamage(damage);

        if (!IsDead() && origin != default && (CurrentInstruction.GetType() != typeof(Attack) &&
                                               CurrentInstruction.GetType() != typeof(Chase)))
        {
            Instructions.Push(CurrentInstruction);
            if (origin != default)
            {
                CurrentInstruction = new Goto(origin, 2, this);
            }
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
