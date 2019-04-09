using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Constants

    private const int playerLayer = 9;
    private const int soldierLayer = 10;
    private const int interactableLayer = 11;
    private const int wallLayer = 13;
    private const int alienLayer = 16;
    #endregion

    #region Variables

    private float _speed;
    private int _damage;

    #endregion

    #region Methods

    public void SetSpeed(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;
    }

    #endregion

    #region Functions

    void Update()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        int otherLayer = other.gameObject.layer;

        if (otherLayer == wallLayer)
        {
            Debug.Log("Projectile hit a wall");
            Destroy(this.gameObject);
        }
        else if ((otherLayer == playerLayer || otherLayer == soldierLayer || otherLayer == interactableLayer) && gameObject.name == "AlienBullet(Clone)")
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(this._damage);
            Destroy(this.gameObject);
            //Debug.Log("Projectile hit " + other.gameObject.name);
        }
        else if ((otherLayer == playerLayer || otherLayer == alienLayer) && gameObject.name == "Bullet(Clone)")
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(this._damage);
            Destroy(this.gameObject);
            //Debug.Log("Projectile hit " + other.gameObject.name);
        }
        else if ((otherLayer == soldierLayer || otherLayer == alienLayer || otherLayer == interactableLayer) && gameObject.name == "PlayerBullet(Clone)")
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(this._damage);
            Destroy(this.gameObject);
        }

    }

    #endregion
}
