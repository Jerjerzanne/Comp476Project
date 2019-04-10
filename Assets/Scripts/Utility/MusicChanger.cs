using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    #region Constants

    private const int playerLayer = 9;
    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Music")]
    public AudioClip musicSoldier;
    public AudioClip musicAlien;

    #endregion

    #region Functions

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.layer == playerLayer)
        {
            AudioSource currentSound = this.gameObject.GetComponent<AudioSource>();
            currentSound.clip = musicSoldier;
            currentSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            AudioSource currentSound = this.gameObject.GetComponent<AudioSource>();
            currentSound.clip = musicAlien;
            currentSound.Play();
        }
    }
    #endregion
}
