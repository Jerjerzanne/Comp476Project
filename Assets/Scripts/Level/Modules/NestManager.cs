using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NestManager : MonoBehaviour
{ 
    #region  Variables

    private List<NestInstance> nests;

    #endregion

    #region Methods

    public NestInstance RandomNest()
    {
        List<NestInstance> availableNests = nests.FindAll(nest => nest.occupied != true);
        if (availableNests.Count > 0)
        {
            NestInstance randomNest = availableNests[Random.Range(0, availableNests.Count - 1)];
            return randomNest;
        }
        return null;
    }

    public NestInstance OccupiedNests()
    {
        List<NestInstance> availableNests = nests.FindAll(nest => nest.occupied);
        if (availableNests.Count > 0)
        {
            NestInstance randomNest = availableNests[Random.Range(0, availableNests.Count - 1)];
            return randomNest;
        }
        return null;
    }

    #endregion

    #region Functions

    // Use this for initialization
    void Awake()
    {
        nests = new List<NestInstance>();
        //Transform listOfPointObjects = transform.Find("PointNest");
        foreach (Transform childTransform in transform)
        {
            NestInstance nest = new NestInstance();
            nest.occupied = false;
            nest.nestPosition = childTransform.transform.position;
            nests.Add(nest);
        }
    }

    #endregion

}
