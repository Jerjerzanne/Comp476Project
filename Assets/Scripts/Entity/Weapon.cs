using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    [SerializeField, Header("Weapon")]
    public int damage;

    #endregion

    #region Methods
    public virtual void FireSingle() { }
    public virtual void FireBurst() { }

    #endregion
}
