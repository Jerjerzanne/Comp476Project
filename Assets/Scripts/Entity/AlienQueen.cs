using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienQueen : Entity
{
    #region Variables

    [HideInInspector]
    public AlienNest homeNest;
    [HideInInspector]
    public NestManager nestManager;

    [Header("Create room attributes")]
    public float protectionTimer;
    public GameObject nestPrefab;

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
                    // TODO: properly detect if enemy is dead?
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
        NestInstance potentialNest = nestManager.RandomNest();
        if (potentialNest != null)
        {
            Instructions.Push(new CreateNest(nestPrefab, potentialNest, protectionTimer, this));
            CurrentInstruction = new Goto(potentialNest.nestPosition + Vector3.forward, 0, this);
            
        }
        else
        {
            CurrentInstruction = new Goto(homeNest.transform.position + Vector3.forward, 0, this);
            Instructions.Push(new Goto(nestManager.OccupiedNests().nestPosition, protectionTimer, this));
        }
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

        protected void Start()
    {
        nestManager = GameObject.FindObjectOfType<NestManager>();
        if (CurrentOrder == null)
        {
            NestInstance potentialNest = nestManager.RandomNest();
            if (potentialNest != null)
            {
                CurrentInstruction = new Goto(potentialNest.nestPosition + Vector3.forward, 0, this);
                Instructions.Push(new CreateNest(nestPrefab, potentialNest, protectionTimer, this));
            }
        }
    }

    protected void Update()
    {
        base.Update();
    }

    #endregion

}
