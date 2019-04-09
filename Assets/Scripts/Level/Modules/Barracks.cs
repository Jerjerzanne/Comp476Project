using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Barracks : Interactable
{
    #region Variables

    [Header("Barracks")]
    public GameObject soldierPrefab;
    public float spawnTimer;
    public CommandCenter commandCenter;

    public List<Pods> soldiers;
    private bool spawning;

    //TODO: remove once done with testing
    [Header("Testing Parameters")]
    public bool testing;
    public Vector3 testingPosition;

    #endregion

    #region Methods

    IEnumerator SpawnSoldier(Pods newSoldier)
    {
        newSoldier.soldierRef = Instantiate(soldierPrefab, this.transform.position + newSoldier.podPosition, Quaternion.identity, this.transform).GetComponent<Soldier>();
        newSoldier.soldierRef.barracks = this;
        yield return new WaitForSeconds(spawnTimer);
        spawning = false;
        yield return null;
    }


    public Soldier RequestSoldier(Order order = null)
    {
        if (soldiers != null)
        {
            Pods soldier = soldiers.Find(status => status.soldierRef != null && status.deployed == false);
            if (soldier != null)
            {
                if(order != null)
                {
                    soldier.soldierRef.CurrentOrder = order;
                }
                soldier.deployed = true;
                soldier.soldierRef.Deployed = true;
                return soldier.soldierRef;
            }

        }

        return null;
    }

    public override Instruction EntityInteract(Entity entity)
    {
        Pods pod = soldiers.Find(soldierPod => soldierPod.soldierRef == entity);
        if (pod != null)
        {
            pod.deployed = false;
            pod.soldierRef.Deployed = false;
            if (pod.soldierRef.ReportPosition != Vector3.zero)
            {
            commandCenter.reportEvent.Invoke(pod.soldierRef.ReportPosition);
            pod.soldierRef.ReportPosition = Vector3.zero;
            }
            Debug.Log(this.transform.position + pod.podPosition);
            return new Goto(this.transform.position + pod.podPosition, 0, entity);
        }
        return null;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (Pods soldier in soldiers.FindAll(soldier => soldier.soldierRef != null))
        {
            if (soldier.soldierRef.barracks == null)
            {
                soldier.soldierRef.barracks = this;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pods newSoldier = soldiers.Find(soldier => soldier.soldierRef == null);
        if (!spawning && newSoldier != null)
        {
            spawning = true;
            StartCoroutine("SpawnSoldier", newSoldier);
        }

        //TODO: remove once done with testing
        if (testing == true && testingPosition != Vector3.zero && soldiers.Find(soldier => soldier.soldierRef != null) != null)
        {
            commandCenter.reportEvent.Invoke(testingPosition);
            testingPosition = Vector3.zero;
        }
    }
}
