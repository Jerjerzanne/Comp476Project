using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    public float detectionRange; //TODO: delete after conic detection

    #region Methods

    private void TargetAcquired(Destructible target)
    {
        Instructions.Push(CurrentInstruction);
        Instructions.Push(new Attack(target, this));
        CurrentInstruction = Instructions.Pop();
    }

    
    override protected void DetectionReaction(GameObject[] target)
    {
        if (target.Length > 0 && target[0].GetComponent<Destructible>() != null)
        {
            if (CurrentInstruction == null)
            {
                Debug.Log(target[0].name + " has a tag " + target[0].gameObject.layer);
                //Instructions.Push(new Goto(this.transform.position, 0,  this));
                TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
            }
            else if (CurrentInstruction.GetType() == typeof(Chase))
            {
                Instructions.Pop();
                Debug.Log(target[0].name + " has a tag " + target[0].gameObject.layer);
                TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
            }
            else if (CurrentInstruction.GetType() != typeof(Attack))
            {
                Debug.Log(target[0].name + " has a tag " + target[0].gameObject.layer);
                TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
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
