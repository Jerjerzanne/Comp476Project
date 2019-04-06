using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandCenter : Destructible
{

    [System.Serializable]
    public class ReportEvent : UnityEvent<Vector3>
    {
    }

    [System.Serializable]
    public class PowerStatusEvent : UnityEvent<Breaker>
    {
    }

    #region Variables

    [Header("Command Center")]
    public ReportEvent reportEvent;
    public PowerStatusEvent powerStatusEvent;
    public Barracks barracks;

    [Header("Investigate Attributes")]

    [Tooltip("Full time for the search")]
    public float searchTimer;
    [Tooltip("Time in between travel points")]
    public float navigationTimer;

    #endregion

    #region Methods

    /// <summary>
    /// invoked function for report events
    /// </summary>
    /// <param name="position"></param>
    private void ReportReaction(Vector3 position)
    {
        //TODO: Box on top of room, room as a collider, position index
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="breaker"></param>
    private void PowerStatusReaction(Breaker breaker)
    {
        Soldier soldier = barracks.RequestSoldier(null);

        if (soldier != null)
        {
            // Get the position of the collider:
            Vector3 colliderPos = breaker.GetComponent<Collider>().transform.position;

            soldier.Instructions.Push(new SearchRoom(breaker.GetComponentInParent<Room>(), searchTimer, navigationTimer,
                soldier));
            soldier.Instructions.Push(new Interact(breaker, soldier));
            soldier.CurrentInstruction = new Goto(colliderPos, 0, soldier);
        }
        else
        {
            Debug.Log("No soldier was available for a PowerStatusReaction");
        }
    }

    #endregion

    #region Functions

    void Awake()
    {

        if (reportEvent == null)
            reportEvent = new ReportEvent();

        if (powerStatusEvent == null)
            powerStatusEvent = new PowerStatusEvent();

        reportEvent.AddListener(ReportReaction);
        powerStatusEvent.AddListener(PowerStatusReaction);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
