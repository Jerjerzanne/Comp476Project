using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    #region Variables

    [Header("Barracks")]
    public GameObject soldierPrefab;
    public float spawnTimer;
    
    public List<Pods> soldiers;
    private bool spawning;
    #endregion

    #region Methods

    IEnumerator SpawnSoldier(Pods newSoldier)
    {
        newSoldier.soldierRef = Instantiate(soldierPrefab, this.transform.position + newSoldier.podPosition, Quaternion.identity, this.transform).GetComponent<Soldier>();
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
