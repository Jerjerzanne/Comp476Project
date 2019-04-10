using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : Order
{
    private float timer = 15.0f;
    public GameObject alienSmallPrefab;
    public GameObject alienQueenPrefab;

    void Start()
    {

    }

    private void Awake()
    {
    }

    override public void ExtractInstructions(Entity entity)
    {
        instructions = new List<Instruction>();
        foreach (GameObject obj in routine)
        {
            // Spawn
            instructions.Add(new Spawn(alienSmallPrefab, alienQueenPrefab, timer, 5, entity));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
