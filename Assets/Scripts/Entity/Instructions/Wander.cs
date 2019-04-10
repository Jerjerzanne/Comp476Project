using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Wander : Instruction
{

    #region Variables

    NavMeshAgent agent;
    float speed = 1.0f;
    float rotationspeed = 100.0f;

    bool isWandering = false;
    bool rotLeft = false;
    bool rotRight = false;
    bool isWalking = false;


    private Vector3 nestPosition;
    private float timer;
    private float visionRange;
    private float nestRange;
    private Vector3 randomPoint;
    private float minRange;

    private float radius;
    private float angleOffsetMax = 20;
    private float angleOffsetSteps = 2;
    private float angleOffset;

    #endregion


    #region Methods

    /// <summary>
    /// Small alien constructor
    /// </summary>
    /// <param name="nest"></param>
    /// <param name="t"></param>
    /// <param name="visionRange"></param>
    /// <param name="nestRange"></param>
    /// <param name="entity"></param>
    public Wander(Vector3 nest, float t, float minRange, float visionRange, float nestRange, Entity entity) : base(entity)
    {
        nestPosition = nest;
        timer = t;
        this.visionRange = visionRange;
        this.nestRange = nestRange;
        this.minRange = minRange;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3 RandomPointInCircle()
    {    //Draws circle of radius, with center center, and locates a point on that circle within angle angle   
        float radius = (visionRange - minRange) / 2;
        RaycastHit hit;

        Vector3 center = instructionRunner.transform.position + instructionRunner.transform.forward * (minRange +
                                                                                                       radius);
        Vector3 position;
        Vector2 pointCircle = Random.insideUnitCircle * radius;
        position.x = center.x + radius + pointCircle.x;
        position.y = center.y;
        position.z = center.z + radius * pointCircle.y;
        if (!Physics.SphereCast(instructionRunner.transform.position, 1, position - instructionRunner.transform.position, out hit, minRange + radius, 1 << 13))
        {
            if ((position - nestPosition).magnitude < nestRange)
            {
                return position;
            }
        }
        return default;
    }

    override public void Execute()
    {
            //Find a point infront
            Vector3 point = RandomPointInCircle();

            if (point != default)
            {
                randomPoint = point;
                angleOffset = 0;
                SetWander();
            }
            else
            {
                //float randomAngle = instructionRunner.transform.eulerAngles.y + Random.Range(45, 315);
                SetRotate(Random.Range(45, 315), 40);
            }

            Debug.Log((point - nestPosition).magnitude);

    }
    private void SetWander()
    {
        instructionRunner.Instructions.Push(instructionRunner.CurrentInstruction);
        instructionRunner.instructionEvent.Invoke(new Goto(randomPoint, timer, instructionRunner));
    }
    private void SetRotate(float targetAngle, float step)
    {
        instructionRunner.Instructions.Push(instructionRunner.CurrentInstruction);
        instructionRunner.instructionEvent.Invoke(new TurnAround(targetAngle, step, instructionRunner));
    }


    #endregion

}
