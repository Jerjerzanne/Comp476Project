using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    #region Variables

    [SerializeField, Header("Barracks")]
    private int maxSoldiers;

    public GameObject soldierPrefab;
    public float spawnTimer;
    
    public List<Pods> soldiers;
    private bool spawning;
    #endregion

    #region Methods

    IEnumerator SpawnSoldier()
    {
        Pods newSoldier = soldiers.Find(status => status.deployed == false && status.soldierRef == null);
        newSoldier.soldierRef = Instantiate(soldierPrefab, this.transform.position + newSoldier.podPosition, Quaternion.identity).GetComponent<Soldier>();
        yield return new WaitForSeconds(spawnTimer);
        spawning = false;
        yield return null;
    }

    private void CheckForSpawn()
    {
        int soldierCount = soldiers.FindAll(soldier => soldier.soldierRef != null).Count;
        if (!spawning && soldierCount < maxSoldiers && soldierCount < soldiers.Count && soldiers != null)
        {
            spawning = true;
            StartCoroutine("SpawnSoldier");
        }
    }

    public void RequestSoldier(Order order) //TODO: rework order
    {
        if (soldiers != null)
        {
            Pods soldier = soldiers.Find(status => status.deployed == false && status.soldierRef != null);

            if (soldier.soldierRef != null && soldier.deployed == false)
            {
                soldier.soldierRef.CurrentOrder = order;
                soldier.deployed = true;
            }
        }
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    CheckForSpawn();
    }
}
