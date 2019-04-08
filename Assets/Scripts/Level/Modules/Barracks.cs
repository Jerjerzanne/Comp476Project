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
            Pods soldier = soldiers.Find(status => status.soldierRef != null);
            if (soldier != null)
            {
                if(order != null)
                {
                    soldier.soldierRef.CurrentOrder = order;
                }
                soldier.deployed = true;
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
            pod.soldierRef.Deployed = true;
            commandCenter.reportEvent.Invoke(pod.soldierRef.ReportPosition);
            return new Goto(pod.podPosition, 0, entity);
        }
        return null;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
