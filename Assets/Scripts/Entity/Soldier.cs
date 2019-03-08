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

    #endregion

    #region Functions

    protected void Update()
    {
        Collider[] targetHit = Physics.OverlapSphere(this.transform.position, detectionRange, 1 << 9);
        if (targetHit.Length > 0 && targetHit[0].GetComponent<Destructible>() != null)
        {
            if (CurrentInstruction == null)
            {
                Debug.Log(targetHit[0].name + " has a tag " + targetHit[0].gameObject.layer);
                //Instructions.Push(new Goto(this.transform.position, 0,  this));
                TargetAcquired(targetHit[0].gameObject.GetComponent<Destructible>());
            }
            else if (CurrentInstruction.GetType() == typeof(Chase))
            {
                Instructions.Pop();
                Debug.Log(targetHit[0].name + " has a tag " + targetHit[0].gameObject.layer);
                TargetAcquired(targetHit[0].gameObject.GetComponent<Destructible>());
            }
            else if(CurrentInstruction.GetType() != typeof(Attack))
            {
                Debug.Log(targetHit[0].name + " has a tag " + targetHit[0].gameObject.layer);
                TargetAcquired(targetHit[0].gameObject.GetComponent<Destructible>());
            }
           
        }
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
