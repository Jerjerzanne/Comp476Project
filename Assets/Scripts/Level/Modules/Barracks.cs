using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{

    public struct SoldierStatus
    {
        public Soldier soldierRef;
        public bool deployed;
        public Vector3 podPosition;
    }

    #region Variables

    [SerializeField, Header("Barracks")]
    private int maxSoldiers;

    public GameObject soldierPrefab;
    public float spawnTimer;
    
    private List<SoldierStatus> soldiers;
    private bool spawning;
    #endregion

    #region Methods

    IEnumerator SpawnSoldier()
    {
        soldiers.Add(new SoldierStatus());
        yield return new WaitForSeconds(spawnTimer);
        spawning = false;
        yield return null;
    }

    public void RequestSoldier(Order order)
    {
        SoldierStatus soldier = soldiers.Find(status => status.deployed == false);
        if (soldier.soldierRef == null)
        {
            soldier.soldierRef = Instantiate(soldierPrefab, this.transform.position + soldier.podPosition, Quaternion.identity).GetComponent<Soldier>();
        }
        soldier.soldierRef.CurrentOrder = order;
        soldier.deployed = true;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        soldiers
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning && soldiers.Count < maxSoldiers)
        {
            spawning = true;
            StartCoroutine("SpawnSoldier");
        }
    }
}
