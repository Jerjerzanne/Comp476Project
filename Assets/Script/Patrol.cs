using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] routine;
    private int roomNb = 0;
    private float timer = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            if (transform.position.x != routine[roomNb].position.x && transform.position.z != routine[roomNb].transform.position.z)
            {
                agent.SetDestination(routine[roomNb].transform.position);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = 3;
            roomNb++;
        }
        if(roomNb > routine.Length - 1)
        {
            roomNb = 0;
        }
    }
}
