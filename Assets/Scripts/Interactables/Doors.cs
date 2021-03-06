﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Doors : MonoBehaviour
{
    public enum doorState
    {
        CLOSED = 0,
        OPEN = 1
    }

    #region Variables

    //Editor variables
    public AudioClip doorSound;

    //Private variables
    private int _count;
    private Animator _animator;
    private bool lockedStatus = false;

    #endregion

    #region Properties

    /// <summary>
    /// Returns the current state of the door
    /// </summary>
    public doorState CurrentState { set; get; }

    #endregion

    #region Methods
    
    /// <summary>
    /// Toggle the lock status of the controlled door:
    /// </summary>
    public void SetLockedState()
    {
        if (lockedStatus)
            lockedStatus = false;
        else
            lockedStatus = true;
    }

    #endregion

    #region Functions

    void Awake()
    {
        this._animator = GetComponent<Animator>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==  9 || other.gameObject.layer == 10 || other.gameObject.layer == 16)
        {
            //Debug.Log(other.gameObject.name + " entered the door range");
            _count++;
            if(CurrentState == doorState.CLOSED && !lockedStatus)
            {
                //Debug.Log("Opening the doors");
                this.CurrentState = doorState.OPEN;
                this._animator.SetBool("Open", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10 || other.gameObject.layer == 16)
        {
            //Debug.Log( other.gameObject.name+ " left the door range");
            _count--;
            if (_count == 0)
            {
                //Debug.Log("Closing the doors");
                this.CurrentState = doorState.CLOSED;
                this._animator.SetBool("Open", false);
            }
        }
    }

    #endregion

}
