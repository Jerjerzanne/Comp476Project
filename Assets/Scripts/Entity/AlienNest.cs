using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNest : Entity
{
    #region Variables

    public float searchRadius = 5.0f;
    public float spawnCountTimer = 5.0f;

    public LayerMask entityMask;
    public LayerMask wallMask;

    [HideInInspector]
    public bool spawnedQueen;

    [Header("Spawn parameters")]
    public GameObject spiderPrefab;
    public GameObject queenPrefab;
    public float spawnTimer;
    public int spawnMaxCount;

    #endregion

    #region Methods

    private void callSmallAliens(Vector3 targetPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag.Contains("Small"))
            {
                Entity small = hit.GetComponent<Entity>();

                if (small.CurrentInstruction.GetType() != typeof(Attack) &&
                    small.CurrentInstruction.GetType() != typeof(Chase))
                {
                    small.Instructions.Push(small.CurrentInstruction);
                    small.Instructions.Push(new Goto(small.transform.position, 0, small));

                    small.CurrentInstruction = new Chase(targetPosition, small);
                }
            }
        }
    }
    private void TargetAcquired(Destructible target)
    {
        if (CurrentInstruction != null)
        {
            Instructions.Push(CurrentInstruction);
        }

        Instructions.Push(new Attack(target, "Alien", this));
        CurrentInstruction = Instructions.Pop();

        callSmallAliens(target.transform.position);

    }

    protected override void DetectionReaction(GameObject[] target)
    {
        foreach (GameObject potentialEnemy in target)
        {
            Destructible enemy = potentialEnemy.GetComponent<Destructible>();

            if (enemy != null)
            {
                if (!enemy.IsDead())
                {
                    if (CurrentInstruction == null)
                    {
                        Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                    else if (CurrentInstruction.GetType() != typeof(Attack))
                    {
                        Debug.Log(enemy + " has a tag " + target[0].gameObject.layer);
                        TargetAcquired(target[0].gameObject.GetComponent<Destructible>());
                        break;
                    }
                }
            }
        }
    }

    public int CountSpawns()
    {
        int count = 0;
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRadius, entityMask);
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.tag.Contains("Small"))
            {
                count++;
            }
        }
        //Debug.Log("Number of spawns: " + count);
        return count;
    }

    IEnumerator CheckFireRate()
    {
        while (true)
        {
            int count = CountSpawns();
            (_weapon as Gun).rateOfFire = count;
            yield return new WaitForSeconds(spawnCountTimer);
        }
    }
    public override void TakeDamage(int damage, Vector3 origin = default(Vector3))
    {
        base.TakeDamage(damage);

        if (!IsDead() && origin != default && (CurrentInstruction.GetType() != typeof(Attack) && CurrentInstruction.GetType() != typeof(Chase)))
        {
            callSmallAliens(origin);
        }
    }
    #endregion

    #region Functions

    void Start()
    {
        if (CurrentOrder == null)
        {
            CurrentInstruction = new Spawn(spiderPrefab, queenPrefab, spawnTimer, spawnMaxCount, this);
        }
        StartCoroutine("CheckFireRate");
    }

    void Update()
    {
        base.Update();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, other.gameObject.transform.position - this.transform.position, out hit, Mathf.Infinity, wallMask))
            {
                DetectionReaction(new GameObject[] { other.gameObject });
            }
        }
    }
    #endregion
}