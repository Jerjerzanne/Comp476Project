using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Instruction
{
    public GameObject alienSmallPrefab;
    private float timerMax;
    private float timer;
    private int spawnMaxCount = 5;

    #region Methods

    public Spawn(float timerMax, Entity entity) : base(entity)
    {
        this.timerMax = timerMax;
        this.timer = 0;
        // TODO: search for a more ideal way of loading the prefab
        alienSmallPrefab = Resources.Load("Prefabs/Entities/AlienSmall", typeof(GameObject)) as GameObject;
    }

    private void SpawnAlienSmall()
    {
        Object.Instantiate(alienSmallPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity);
    }

    override public void Execute()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if ((instructionRunner as AlienNest).CountSpawns() >= spawnMaxCount)
            {
                instructionRunner.instructionEvent.Invoke(new CreateQueen(60.0f, instructionRunner));
            }

            // TODO: limit number of spawns per nest
            SpawnAlienSmall();
            timer = timerMax;
        }
    }

    #endregion
}