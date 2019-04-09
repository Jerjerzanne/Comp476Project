using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Instruction
{

    #region Constants

    private const int entityLayer = 1 << 10;
    private const int interactalbeLayer = 1 << 11;
    private const int doorLayer = 1 << 12;
    private const int projectileLayer = 1 << 15;
    private const int finalMask = entityLayer | doorLayer | projectileLayer;

    #endregion

    private Destructible target;

    private Vector3 lastPosition;

    #region Methods

    public Attack(Destructible target, Entity entity) : base(entity)
    {
        this.target = target;
        lastPosition = target.transform.position;
    }

    public override void Execute()
    {
        RaycastHit hit;
        if (Physics.Raycast(instructionRunner.transform.position, target.transform.position - instructionRunner.transform.position, out hit, Mathf.Infinity, ~finalMask))
        {
            //Debug.DrawRay(instructionRunner.transform.position, (target.transform.position - instructionRunner.transform.position) * 4f);

            //Debug.Log("Ray connected with " + hit.collider.gameObject.name);
            Destructible raycastTarget = hit.collider.gameObject.GetComponent<Destructible>();
            if (raycastTarget != null)
            {
                if (raycastTarget == target)
                {
                    if (!target.IsDead())
                    {
                        //Debug.Log(instructionRunner.name + " is attacking target " + target.name);
                        lastPosition = target.transform.position;
                        instructionRunner.transform.LookAt(new Vector3(target.transform.position.x, instructionRunner.transform.position.y, target.transform.position.z)); //*Maybe add steering behavior in the future*
                        instructionRunner.UseWeapon();
                    }
                    else
                    {
                        //Debug.Log(target.name + " is dead. returning to previous behavior.");
                        instructionRunner.instructionEvent.Invoke(null);
                    }

                }
            }
            else
            {
                Debug.Log("Instruction runner tag:" + instructionRunner.tag);
                if(!instructionRunner.tag.Contains("Nest"))
                {
                    Debug.Log(target.name + " has been lost. Beginning Chase.");
                    instructionRunner.instructionEvent.Invoke(new Chase(lastPosition, instructionRunner));
                }
                else
                {
                    Debug.Log(target.name + " has been lost. Will not chase.");
                    instructionRunner.instructionEvent.Invoke(null);
                }
            }
        }
        else
        {
            //Debug.Log("Raycast did not connect.");
        }

    }
    #endregion
}
