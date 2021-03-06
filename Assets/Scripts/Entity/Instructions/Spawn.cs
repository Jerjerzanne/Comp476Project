﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Instruction
{
    private GameObject alienSmallPrefab;
    private GameObject alienQueenPrefab;
    private float timerMax;
    private float timer;
    private int spawnMaxCount = 5;

    #region Methods

    public Spawn(GameObject alienSmallPrefab, GameObject alienQueenPrefab, float timerMax, int spawnMaxCount, Entity entity) : base(entity)
    {
        this.timerMax = timerMax;
        this.timer = 0;
        this.alienSmallPrefab = alienSmallPrefab;
        this.alienQueenPrefab = alienQueenPrefab;
        this.spawnMaxCount = spawnMaxCount;
    }

    private void SpawnAlienSmall()
    {
        GameObject small = Object.Instantiate(alienSmallPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity) as GameObject;
        small.GetComponent<Spiders>().nestPosition = instructionRunner.transform.position;
        
        small.transform.parent = instructionRunner.transform.parent;
    }

    override public void Execute()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if ((instructionRunner as AlienNest).CountSpawns() >= spawnMaxCount && !(instructionRunner as AlienNest).spawnedQueen)
            {
                instructionRunner.Instructions.Push(instructionRunner.CurrentInstruction);
                instructionRunner.instructionEvent.Invoke(new CreateQueen(alienQueenPrefab, timerMax * 2, instructionRunner));
            }

            if ((instructionRunner as AlienNest).CountSpawns() < spawnMaxCount)
            {
            SpawnAlienSmall();
            }
            timer = timerMax;
        }
    }

    #endregion
}