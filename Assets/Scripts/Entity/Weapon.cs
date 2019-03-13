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

    virtual public void Fire() { }

    #endregion
}
