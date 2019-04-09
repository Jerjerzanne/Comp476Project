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
        Transform listOfPointObjects = transform.Find("Waypoints");
        foreach (Transform childTransform in listOfPointObjects)
        {
            if (childTransform.gameObject.name.Contains("Point")) // TODO: Find a more stable way to detect Point objects only in the list of transforms.
                waypoints.Add(childTransform.position);
        }
    }

    #endregion
}
