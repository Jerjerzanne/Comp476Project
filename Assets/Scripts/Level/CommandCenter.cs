using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandCenter : Destructible
{

    [System.Serializable]
    public class ReportEvent : UnityEvent<Vector3, int>
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

    private List<Breaker> listOfBrokenBreaker = new List<Breaker>();

    #endregion

    #region Methods

    /// <summary>
    /// invoked function for report events
    /// </summary>
    /// <param name="position"></param>
    private void ReportReaction(Vector3 position, int numberOfSoldiers)
    {
        RaycastHit ray;
        if (Physics.Raycast(position + Vector3.up * 50, Vector3.down, out ray, 100, 1 << 18))
        {
            Room room = ray.collider.gameObject.GetComponent<Room>();
            Debug.Log("Command center received a report to " + room.name);

            if (numberOfSoldiers == null)
                numberOfSoldiers = 1;

            //Requesting a new soldier from the barracks
            for (int i = 0; i < numberOfSoldiers; i++)
            {
                Soldier soldier = barracks.RequestSoldier(null);


                if (soldier != null)
                {
                    ray.collider.gameObject.GetComponent<Room>();
                    soldier.Instructions.Push(new SearchRoom(ray.collider.gameObject.GetComponent<Room>(), searchTimer, navigationTimer,
                        soldier));
                    soldier.CurrentInstruction = new Goto(position, 0, soldier);
                    soldier.SpookLevel = Soldier.ReportState.Attacking;
                }
                else
                {
                    Debug.Log("No soldier was available for a ReportReaction");
                    break;
                }
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
            if (listOfBrokenBreaker.Count == 0)
            {
                listOfBrokenBreaker.Add(breaker);
            }
            else
            {
                foreach (Breaker breakers in listOfBrokenBreaker)
                {
                    if (breakers != breaker)
                    {
                        listOfBrokenBreaker.Add(breaker);
                    }
                }
            }
            // Get the position of the collider:
            Vector3 colliderPos = breaker.GetComponent<BoxCollider>().transform.position + -breaker.transform.forward * 1;
            Debug.Log(colliderPos);
            soldier.Instructions.Push(new SearchRoom(breaker.GetComponentInParent<Room>(), searchTimer, navigationTimer,
                soldier));
            soldier.Instructions.Push(new Interact(breaker, soldier));
            soldier.CurrentInstruction = new Goto(colliderPos, 0, soldier);
            soldier.SpookLevel = Soldier.ReportState.Investigating;
        }
        else
        {
            Debug.Log("No soldier was available for a PowerStatusReaction");
        }
    }
    IEnumerator CheckBrokenBreaker()
    {
        foreach (Breaker breakers in listOfBrokenBreaker)
        {
            PowerStatusReaction(breakers);
        }
        yield return new WaitForSeconds(checkTimer);
    }
    IEnumerator CheckRoutes()
    {
        while(true)
        {
            //Debug.Log(Time.time);
            foreach (var route in patrolRoutes)
            {
                //Debug.Log(route.agent);
                if (route.agent == null)
                {
                    route.agent = barracks.RequestSoldier(route);
                    if (route.agent != null)
                    {
                        (route.agent as Soldier).SpookLevel = Soldier.ReportState.Patrolling;
                    }
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
        StartCoroutine("CheckBrokenBreaker");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
