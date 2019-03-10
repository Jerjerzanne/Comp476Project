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
       // detectionRange = GetComponent<ConeOfVision>().getDistanceToTarget();
       // Collider[] target = Physics.OverlapSphere(this.transform.position, detectionRange, 1 << 9);
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
        //DetectionReaction();
        base.Update();
    }


    #endregion


    //void OnGUI()
    //{
    //    // Make a multiline text area that modifies stringToEdit.

    //    string text = "";
    //    int count = 0;
    //    if (CurrentInstruction != null)
    //    {
    //        text = CurrentInstruction.ToString() + "\n";
    //        count++;
    //    }
    //    foreach (Instruction stackInt in Instructions)
    //    {
    //        text += stackInt + "\n";
    //        count++;
    //    }
    //    GUI.TextArea(new Rect(10, 10, 100, count* 10 + 15), text, 200);
    //}
}
