using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNest : Instruction
{
    public GameObject alienNestPrefab;

    #region Methods

    public CreateNest(Entity entity) : base(entity)
    {
        // TODO: search for a more ideal way of loading the prefab
        alienNestPrefab = Resources.Load("Prefabs/Entities/AlienNest", typeof(GameObject)) as GameObject;
    }

    override public void Execute()
    {
        Object.Instantiate(alienNestPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity);
        instructionRunner.instructionEvent.Invoke(null);
    }

    #endregion
}