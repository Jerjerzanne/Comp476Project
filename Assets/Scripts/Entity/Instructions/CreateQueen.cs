using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQueen : Instruction
{
    public GameObject alienQueenPrefab;
    private float timerMax;
    private float timer;

    #region Methods

    public CreateQueen(float timerMax, Entity entity) : base(entity)
    {
        this.timerMax = timerMax;
        this.timer = 0;
        // TODO: search for a more ideal way of loading the prefab
        alienQueenPrefab = Resources.Load("Prefabs/Entities/AlienQueen", typeof(GameObject)) as GameObject;
    }

    private void SpawnAlienQueen()
    {
        Object.Instantiate(alienQueenPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity);
    }

    override public void Execute()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            // TODO: possibly limit number of queen spawns per nest to 1
            SpawnAlienQueen();
            timer = timerMax;
        }
    }

    #endregion
}