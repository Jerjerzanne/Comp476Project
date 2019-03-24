using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable game entity
/// </summary>
public class Interactable : MonoBehaviour
{
    #region Variables

    public Instruction nextInstruction;


    protected float cooldownTime = 0.3f;
    // When checking for cooldown, we want to do it as such:
    // currentTime - lastInteractionTime > cooldownTime
    protected static float lastInteractionTime;
    //editor variables

    #endregion

    #region Methods

    /// <summary>
    /// Manages interactions between the interactable and the entity
    /// </summary>
    public virtual Instruction EntityInteract(Entity entity)
    {
        return nextInstruction;
    }

    /// <summary>
    /// Manages interactions between the interactable and the player
    /// </summary>
    public virtual void PlayerInteract(Player player)
    {
        // Check cooldown of the inputs:
    }

    #endregion

}
