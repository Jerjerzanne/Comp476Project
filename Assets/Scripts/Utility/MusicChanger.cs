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
    public AudioClip musicNeutral;
    public AudioClip musicSoldier;
    public AudioClip musicAlien;


    #endregion

    #region Functions

    void OnTriggerEnter(Collider other) {
        AudioSource currentSound = this.gameObject.GetComponent<AudioSource>();
        if (other.gameObject.layer == playerLayer && other.GetType() == typeof(SphereCollider))
        {

            currentSound.clip = musicSoldier;
            currentSound.Play();
        }
        else if (other.gameObject.layer == playerLayer && other.GetType() == typeof(BoxCollider)) {
            currentSound.clip = musicAlien;
            currentSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            AudioSource currentSound = this.gameObject.GetComponent<AudioSource>();
            currentSound.clip = musicNeutral;
            currentSound.Play();
        }
    }
    #endregion
}
