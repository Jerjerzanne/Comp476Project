using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : Instruction
{
    float timeToRepair;
    Interactable objectToRepair;
    #region Methods
    public Repair(float repairTime, Interactable objectToRepair, Entity entity) : base(entity)
    {
        timeToRepair = repairTime;
        this.objectToRepair = objectToRepair;
    }

    public override void Execute()
    {
        if (timeToRepair > 0)
        {
            timeToRepair -= Time.deltaTime;
        }
        else
        {
            objectToRepair.Repair();
            instructionRunner.instructionEvent.Invoke(null);
        }
    }
    #endregion
}
