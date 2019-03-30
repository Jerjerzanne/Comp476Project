using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNest : Instruction
{
    private GameObject alienNestPrefab;

    #region Methods

    public CreateNest(GameObject alienNestPrefab, Entity entity) : base(entity)
    {
        this.alienNestPrefab = alienNestPrefab;
    }

    override public void Execute()
    {
        GameObject nest = Object.Instantiate(alienNestPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity) as GameObject;
        nest.transform.parent = instructionRunner.transform.parent;
        instructionRunner.instructionEvent.Invoke(null);
    }

    #endregion
}