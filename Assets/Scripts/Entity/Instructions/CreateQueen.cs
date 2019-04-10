using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQueen : Instruction
{
    private GameObject alienQueenPrefab;
    private float timerMax;
    private float timer;

    #region Methods

    public CreateQueen(GameObject alienQueenPrefab, float timerMax, Entity entity) : base(entity)
    {
        this.timerMax = timerMax;
        this.timer = timerMax;
        this.alienQueenPrefab = alienQueenPrefab;
    }

    private void SpawnAlienQueen()
    {
        AlienQueen queen = Object.Instantiate(alienQueenPrefab, instructionRunner.transform.position + instructionRunner.transform.forward, Quaternion.identity).GetComponent<AlienQueen>();
        queen.homeNest = instructionRunner.GetComponent<AlienNest>();
        instructionRunner.GetComponent<AlienNest>().spawnedQueen = true;
        if (instructionRunner.GetComponent<Roam>() != null)
        {
            queen.CurrentOrder = instructionRunner.GetComponent<Roam>();
        }
        queen.transform.parent = instructionRunner.transform.parent;
    }

    override public void Execute()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            // TODO: possibly limit number of queen spawns per nest to 1
            SpawnAlienQueen();
            instructionRunner.instructionEvent.Invoke(null);
            //timer = timerMax;
        }
    }

    #endregion
}