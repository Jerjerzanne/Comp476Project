using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destructible game entity
/// </summary>
public class Destructible : MonoBehaviour
{
    #region Constants

    /// <summary>
    /// Layer of the dead destructible
    /// </summary>
    protected const int deathLayer = 12;

    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Destructible")]
    public int maxHealth;

    #endregion

    #region Properties

    /// <summary>
    /// Current health of the destructible
    /// </summary>
    public int CurrentHealth { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// reduce the damage from the destructible
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// the destructible changes layer to death layer
    /// </summary>
    protected virtual void Die()
    {
        this.gameObject.layer = deathLayer;
    }
    #endregion

}
