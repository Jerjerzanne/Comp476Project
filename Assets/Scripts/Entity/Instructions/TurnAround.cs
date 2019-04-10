using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnAround : Instruction
{
    float acceptanceRadius = 10f;
    
    //private float callForOrder;
    //private int roomNb = 0;
    private float targetAngle;
    private Quaternion targetQuaternion;
    private float angleStep;
    private Quaternion startAngle;

    #region Methods

    public TurnAround(float targetAngle, float angleStep, Entity entity) : base(entity)
    {
        this.startAngle = instructionRunner.transform.rotation;
        this.targetAngle = targetAngle;
        this.angleStep = angleStep;
        this.targetQuaternion = startAngle * Quaternion.Euler(0, targetAngle, 0);
    }

    public override void Execute()
    {

        Debug.Log("target angle: ");
        Debug.Log(instructionRunner.transform.rotation);
        Debug.Log(targetQuaternion);
        Debug.Log(Quaternion.Angle(instructionRunner.transform.rotation, targetQuaternion));
        if (Quaternion.Angle(instructionRunner.transform.rotation, targetQuaternion) < acceptanceRadius)
        {
            instructionRunner.instructionEvent.Invoke(null);
        }
        else
        {
            instructionRunner.transform.rotation = Quaternion.RotateTowards(instructionRunner.transform.rotation, targetQuaternion, angleStep * Time.deltaTime);
        }
    }
    #endregion
}

