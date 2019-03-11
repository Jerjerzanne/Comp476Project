using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class SearchRoom : Instruction
{

    #region Variables

    private Room room;
    private List<Vector3> points;
    private Vector3 currentPoint;
    private float timer;
    private float startTimer;
    //private float lost ;
    private float navigateTimer;

    #endregion

    public SearchRoom(Room room, float timer, float navigateTimer, Entity entity) : base(entity)
    {
        this.room = room;
        this.timer = timer;
        this.navigateTimer = navigateTimer;

        points = new List<Vector3>(room.waypoints);
        if (points == null)
        {
            Debug.Log("Cannot search room without waypoints.");
            //instructionRunner.instructionEvent.Invoke(null);
        }
        else
        {
            int index = UnityEngine.Random.Range(0, points.Count - 1);
            currentPoint = points[index];
            points.Remove(currentPoint);
            SetWaypoint();
        }
    }

    #region Methods

    private void GetWaypoint()
    {
        Debug.Log(points.Count);
        if (points.Count == 0)
        {
            Debug.Log("You should reach here");
            points = new List<Vector3>(room.waypoints);
        }
        int index = UnityEngine.Random.Range(0, points.Count -1);
        Debug.Log(index + " and the count: " + points.Count);
        currentPoint = points[index];
        points.Remove(currentPoint);
        SetWaypoint();
    }

    private void SetWaypoint()
    {
        instructionRunner.Instructions.Push(instructionRunner.CurrentInstruction);
        instructionRunner.instructionEvent.Invoke(new Goto(currentPoint, navigateTimer, instructionRunner));
    }

    override public void Execute()
    {
        Debug.Log("there...");
        if (startTimer == 0.0f)
        {
            Debug.Log("Arrived at the room.");
            startTimer = Time.time;
            GetWaypoint();
        }
        else
        {
            timer -= Mathf.Abs(startTimer - Time.time);

            if (timer <= 0.0f)
            {
                Debug.Log("Search has ended. Returning to previous behavior");
                instructionRunner.instructionEvent.Invoke(null);
            }
            else
            {
                startTimer = Time.time;
                GetWaypoint();
            }
        }
    }

    #endregion
}
