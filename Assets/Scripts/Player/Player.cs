using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player information
/// </summary>
public class Player : Destructible
{
    #region Variables

    //Editor variables
    [SerializeField, Header("Player")]
    private int initialGrowth;
    public int maxGrowth;

    #endregion

    #region Properties
    
    /// <summary>
    /// Current growth level of the player
    /// </summary>
    public int CurrentGrowth { get; set; }

    #endregion

    #region Methods
    #endregion

    #region Functions

    protected void Awake()
    {
        base.Awake();
        CurrentGrowth = initialGrowth;
    }

    #endregion

}
