using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vents : MonoBehaviour
{
    #region Variables
    //Private variables

    //Editor variables
    public Vents exit;
    //Piece of the UI

    #endregion

    #region Methods

    /// <summary>
    /// Teleports entity to the exit vent
    /// </summary>
    private void InteractVent(GameObject entity)
    {
        if (exit != null)
        {
            entity.GetComponent<NavMeshAgent>().Warp(exit.transform.position);
            //entity.transform.position = exit.transform.position;
        }
    }
    #endregion

    #region Functions

    void Awake()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name + " entered the vent range");
            //Start showing the UI
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Player interacted with the vent");
                InteractVent(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name + " left the vent range");
            //Stop showing the UI
        }
    }
    #endregion
}
