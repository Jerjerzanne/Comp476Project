using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Current high-level state of an entity
/// </summary>
public class Order : MonoBehaviour
{
    #region Variables
    
    //Editor fields
    public GameObject[] routine;
    public List<Instruction> instructions;

    #endregion

    #region Methods


    virtual public void ExtractInstructions(Entity entity) { }


    #endregion
}
