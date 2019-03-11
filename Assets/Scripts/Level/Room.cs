using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Variables

    [HideInInspector]
    public List<Vector3> waypoints;

    #endregion

    #region Functions

    private void Awake()
    {
        
        foreach (Transform childTransform in this.GetComponentsInChildren<Transform>())
        {
           waypoints.Add(childTransform.position);
        }
    }

    #endregion
}
