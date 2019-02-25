using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    enum state
    {
        CLOSED = 0,
        OPEN = 1

    }

    #region Variables
    private int _count;
    private state _currentState;
    private Animator _animator;
    #endregion
    #region Functions

    // Start is called before the first frame update
    void Start()
    {
        this._animator = GetComponent<Animator>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer ==  9 || other.gameObject.layer == 10)
        {
            Debug.Log(other.gameObject.name + " entered the door range");
            _count++;
            if(_currentState == state.CLOSED)
            {
                Debug.Log("Opening the doors");
                this._currentState = state.OPEN;
                this._animator.SetBool("Open", true);
                
                //Start coroutine
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 || other.gameObject.layer == 10)
        {
            Debug.Log( other.gameObject.name+ " left the door range");
            _count--;
                if (_count == 0)
                {
                    Debug.Log("Closing the doors");
                this._currentState = state.CLOSED;
                this._animator.SetBool("Open", false);
                //Start coroutine
            }
        }
    }

    #endregion

}
