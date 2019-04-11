using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Entity
{
    #region Variables
    public enum ReportState { Patrolling, Investigating, Attacking };

    private Pods myPod;
    private bool deployed;
    private Vector3 reportPosition;
    private bool isWalking = true;
    private bool canAttack = true;

    [HideInInspector]
    public Barracks barracks;

    [Header("UI")]
    public AudioClip walking;
    [Range(0, 1)] public float walkingVol = 0.25f;

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

    public ReportState SpookLevel
    {
        get; set;
    }

    public int NumberOfSoldiersToSend
    {
        get; set;
    }

    #endregion

    #region Methods

    private void TargetAcquired(Destructible target)
    {
        if (canAttack)
        {
            if (SpookLevel == ReportState.Attacking || target.CurrentGrowth == (int)Sizes.Small)
            {
                if (CurrentInstruction != null)
                {
                    Instructions.Push(CurrentInstruction);
                }

                _navMeshAgent.SetDestination(this.transform.position);
                Instructions.Push(new Attack(target, "Soldier", this));
                CurrentInstruction = Instructions.Pop();
            }
            else if (target.CurrentGrowth == (int)Sizes.Medium)
            {
                canAttack = false;
                NumberOfSoldiersToSend = 2;
                Instructions.Clear();
                Instructions.Push(new Interact(barracks, this));
                CurrentInstruction = new Goto(barracks.transform.position, 0, this);
                reportPosition = target.transform.position;
            }
            else if (target.CurrentGrowth == (int)Sizes.Large)
            {
                canAttack = false;
                NumberOfSoldiersToSend = 4;
                Instructions.Clear();
                Instructions.Push(new Interact(barracks, this));
                CurrentInstruction = new Goto(barracks.transform.position, 0, this);
                reportPosition = target.transform.position;
            }

        }
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
                       // Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        //Instructions.Push(new Goto(this.transform.position, 0,  this));
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                    else if (CurrentInstruction.GetType() == typeof(Chase))
                    {
                        Instructions.Pop();
                       // Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                    else if (CurrentInstruction.GetType() != typeof(Attack))
                    {
                        //Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
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
        else
        {
            base.RanOutOfInstructions();
        }
    }

    public override void TakeDamage(int damage, Vector3 origin = default(Vector3))
    {
        base.TakeDamage(damage);

        if (!IsDead() && origin != default && (CurrentInstruction.GetType() != typeof(Attack) && CurrentInstruction.GetType() != typeof(Chase)))
        {
            Instructions.Push(CurrentInstruction);
            if (origin != default)
            {
                CurrentInstruction = new Goto(origin, 2, this);
            }
        }

        if (CurrentHealth < 10)
        {
            canAttack = false;
            Instructions.Clear();
            Instructions.Push(new Interact(barracks, this));
            CurrentInstruction = new Goto(barracks.transform.position, 0, this);
            reportPosition = origin;
        }
    }

    public void setCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    #endregion

    #region Functions

    protected void Update()
    {
        if((CurrentInstruction == null || CurrentInstruction.GetType() != typeof(Attack) && CurrentInstruction.GetType() != typeof(FixBreaker)) && isWalking == true )
        {
            isWalking = false;
            StartCoroutine(walkingLoop());
        }
        base.Update();
        
    }

    public IEnumerator walkingLoop() {

        AudioSource.PlayClipAtPoint(walking, Camera.main.transform.position, walkingVol);
        yield return new WaitForSeconds(1);
        isWalking = true;
    }
    #endregion

}
