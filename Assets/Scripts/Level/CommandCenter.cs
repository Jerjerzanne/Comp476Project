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

    [Header("Patrols")]
    public List<Patrol> patrolRoutes;
    public float checkTimer;

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
        RaycastHit ray;
      if(Physics.Raycast(position + Vector3.up * 50, Vector3.down, out ray, 100, 1 << 18))
        {
            Room room = ray.collider.gameObject.GetComponent<Room>();
            Debug.Log("Command center received a report to " + room.name);

            //Requesting a new soldier from the barracks
            Soldier soldier = barracks.RequestSoldier(null);

            if (soldier != null)
            {
                ray.collider.gameObject.GetComponent<Room>();
                soldier.Instructions.Push(new SearchRoom(ray.collider.gameObject.GetComponent<Room>(), searchTimer, navigationTimer,
                    soldier));
                soldier.CurrentInstruction = new Goto(position, 0, soldier);
            }
            else
            {
                Debug.Log("No soldier was available for a ReportReaction");
            }
            
            
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="breaker"></param>
    private void PowerStatusReaction(Breaker breaker)
    {

        //Requesting a new soldier from the barracks
        Soldier soldier = barracks.RequestSoldier(null);

        if (soldier != null)
        {
            // Get the position of the collider:
            Vector3 colliderPos = breaker.GetComponent<BoxCollider>().transform.position + -breaker.transform.forward * 1;
            Debug.Log(colliderPos);
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

    IEnumerator CheckRoutes()
    {
        while(true)
        {
            Debug.Log(Time.time);
            foreach (var route in patrolRoutes)
            {
                Debug.Log(route.agent);
                if (route.agent == null)
                {
                    route.agent = barracks.RequestSoldier(route);
                }
            }
            yield return new WaitForSeconds(checkTimer);
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

    void Start()
    {
        StartCoroutine("CheckRoutes");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
