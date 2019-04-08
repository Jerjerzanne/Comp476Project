using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : Instruction
{
    NavMeshAgent agent;
    float speed = 1.0f;
    float rotationspeed = 100.0f;

    bool isWandering = false;
    bool rotLeft = false;
    bool rotRight = false;
    bool isWalking = false;
    Vector3 nestPosition;
    float timer;
    float visionRange;
    float nestRange;
    Vector3 randomPoint;
    
    public Wander(Vector3 nest,float t,float visionRange, float nestRange,Entity entity) : base(entity)
    {
        nestPosition = nest;
        timer = t;
        this.visionRange = visionRange;
        this.nestRange = nestRange;
    }
    Vector3 RandomPointInCircle(Vector3 center, float radius, float angle)
    {    //Draws circle of radius, with center center, and locates a point on that circle within angle angle   
        float newAngle = Random.Range(0,180);
        Vector3 position;
        position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = center.y;
        position.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        instructionRunner.transform.eulerAngles = new Vector3(0,newAngle,0);
        return position;
    }
    override public void Execute()
    {
        //Find a point infront
        Vector3 point = RandomPointInCircle(instructionRunner.transform.position, 1 ,instructionRunner.transform.eulerAngles.y);
        RaycastHit hit;
        if (Physics.Raycast(instructionRunner.transform.position, point, out hit, 1, 13))
        {
            instructionRunner.transform.Rotate(0, 0, 180);
        }
        
        else if ((point - nestPosition).magnitude < nestRange)
        {
            randomPoint = point;
        }
        else
        {
            float newAngle = Random.Range(180, 360);
            while(instructionRunner.transform.eulerAngles.y < newAngle)
            {
                instructionRunner.transform.Rotate(instructionRunner.transform.up, Time.deltaTime);
            }
        }
        //randomPoint = point;
        SetWander();
        Debug.Log((point - nestPosition).magnitude);
        
    }
    private void SetWander()
    {
        instructionRunner.Instructions.Push(instructionRunner.CurrentInstruction);
        instructionRunner.instructionEvent.Invoke(new Goto(randomPoint, timer, instructionRunner));
    }
    
}
