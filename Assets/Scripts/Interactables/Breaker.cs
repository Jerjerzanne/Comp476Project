using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : Interactable
{
    #region Variables
    //Private variables

    //Editor variables
    public Instruction breakerInstruction;
    public List<Light> lights;
    public bool enabled;
    protected CommandCenter commandCenter;

    //Piece of the UI

    #endregion

    #region Methods

    //Overriding methods
    public override void PlayerInteract(Player player)
    {
        base.PlayerInteract(player);
        InteractBreaker();
    }

    public override Instruction EntityInteract(Entity entity)
    {
        return new Repair(5.0f, this, entity);
    }

    public override void Repair()
    {
        CurrentHealth = maxHealth;
        enabled = true;
        InteractBreaker();
    }

    //class methods

    /// <summary>
    /// Toggles the lights:
    /// </summary>
    private void InteractBreaker()
    {
        if (enabled)
        {
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            enabled = false;
        }
        else
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
            enabled = true;
        }
    }

    private void SetLights()
    {
        if (enabled)
        {
            foreach (Light light in lights)
                light.enabled = true;
        }
        else
        {
            foreach (Light light in lights)
                light.enabled = false;
        }
    }

    protected override void Die()
    {
        base.Die();
        enabled = false;
        InteractBreaker();
        commandCenter.powerStatusEvent.Invoke(this); //Activates the CC response
    }

    #endregion

    #region Functions

    void Start()
    {
        commandCenter = FindObjectOfType<CommandCenter>();
        //enabled = true;
        // Set the lights to the proper enabled value:
        SetLights();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name + " entered the breaker range");
            //Start showing the UI
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (Input.GetButtonDown("Interact") && Time.time - lastInteractionTime > cooldownTime)
            {
                Debug.Log("Player interacted with the breaker");
                PlayerInteract(other.GetComponent<Player>());
                // Interact cooldown set:
                lastInteractionTime = Time.time;
            }
        }
        if (other.gameObject.layer == 10)
        {
            // Debug.Log("Entity interacted with the breaker");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log(other.gameObject.name + " left the breaker range");
            //Stop showing the UI
        }
    }

    #endregion
}
