using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNest : Instruction
{
    private GameObject alienNestPrefab;
    private NestInstance nestInstance;
    private float timer;

    #region Methods
    public CreateNest(GameObject alienNestPrefab, float protectionTimer, Entity entity) : base(entity)
    {
        this.alienNestPrefab = alienNestPrefab;
        this.timer = protectionTimer;
    }

    public CreateNest(GameObject alienNestPrefab, NestInstance potentialNest, float protectionTimer, Entity entity) : base(entity)
    {
        this.alienNestPrefab = alienNestPrefab;
        this.timer = protectionTimer;
        this.nestInstance = potentialNest;
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (nestInstance != null)
            {
                if (!nestInstance.occupied)
                {
                    AlienNest nest = Object.Instantiate(alienNestPrefab,
                            instructionRunner.transform.position + instructionRunner.transform.forward,
                            Quaternion.identity)
                        .GetComponent<AlienNest>();
                    nest.transform.parent = instructionRunner.transform.parent;
                    nestInstance.occupied = true;
                    nest.nestInstance = nestInstance;
                    instructionRunner.instructionEvent.Invoke(null);
                }
                else
                {
                    instructionRunner.instructionEvent.Invoke(null);
                }
            }
            else
            {
                GameObject nest = Object.Instantiate(alienNestPrefab, new Vector3(instructionRunner.transform.position.x, 0.5f, instructionRunner.transform.position.z) + instructionRunner.transform.forward, Quaternion.identity) as GameObject;
                nest.transform.parent = instructionRunner.transform.parent;
                instructionRunner.instructionEvent.Invoke(null);    
            }
        }
    }

    #endregion
}