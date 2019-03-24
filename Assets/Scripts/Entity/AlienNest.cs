using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienNest : MonoBehaviour
{
    #region Variables
    public GameObject alienSmall;
    public GameObject alienQueen;
    public float queenTimer = 6;
    public float spawnTimer = 3;
    bool playerSpotted = false;
    bool haveAllies = false;
    bool queenDetected = false;
    #endregion

    #region Methods
    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        queenTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
        Spawn();
        }
    }

    private void Spawn()
    {
        if (playerSpotted && haveAllies)
        {
            //switch to atk mode
        }
        if (!playerSpotted)
        {
            if (spawnTimer < 0)
            {
                GameObject alienSClone = (GameObject)Instantiate(alienSmall, transform.position, Quaternion.identity);
                spawnTimer = 3;
            }
        }
        if (!queenDetected)
        {
            if (queenTimer < 0)
            {
                GameObject alienQClone = (GameObject)Instantiate(alienQueen, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z + 1.0f), Quaternion.identity);
                queenTimer = 6;
            }
        }

    }
    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerSpotted = true;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerSpotted = true;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerSpotted = false;
        }
        if (other.gameObject.layer == 16)
        {
            haveAllies = false;
        }
    }
    #endregion
}
