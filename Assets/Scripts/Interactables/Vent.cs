using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vent : Interactable
{
    #region Variables
    //Private variables

    //Editor variables
    public Instruction ventInstruction;
    public Vent exit;
    //Piece of the UI

    #endregion

    #region Methods

    //Overriding methods

    public override Instruction EntityInteract(Entity entity)
    {
        return ventInstruction;
    }

    public override void PlayerInteract(Player player)
    {
        base.PlayerInteract(player);
        InteractVent(player.gameObject);
    }

    //class methods

    /// <summary>
    /// Teleports entity to the exit vent
    /// </summary>
    private void InteractVent(GameObject entity)
    {
        if (exit != null)
        {
            entity.GetComponent<NavMeshAgent>().Warp(exit.transform.position);
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
                PlayerInteract(other.GetComponent<Player>());
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
