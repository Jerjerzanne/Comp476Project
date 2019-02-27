using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable game entity
/// </summary>
public class Interactable : MonoBehaviour
{
    #region Variables

    //editor variables

    #endregion

    #region Methods
    
    /// <summary>
    /// Manages interactions between the interactable and the entity
    /// </summary>
    public virtual Instruction EntityInteract(Entity entity)
    {
        return null;
    }

    /// <summary>
    /// Manages interactions between the interactable and the player
    /// </summary>
    public virtual void PlayerInteract(Player player) { }

    #endregion

}
