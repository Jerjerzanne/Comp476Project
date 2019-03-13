using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

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
        if (other.gameObject.layer == 1 >> LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("Projectile hit a wall");
            Destroy(this.gameObject);
        }
        else if(other.gameObject.layer == 1 >> LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<Destructible>().TakeDamage(this._damage);
            Destroy(this.gameObject);
            //Debug.Log("Projectile hit " + other.gameObject.name);
        }

       
    }
    
    #endregion
}
