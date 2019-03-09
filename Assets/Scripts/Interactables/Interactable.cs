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
    public virtual void PlayerInteract(Player player) { }

    #endregion

}
