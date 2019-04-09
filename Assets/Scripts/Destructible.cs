using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Destructible game entity
/// </summary>
public class Destructible : MonoBehaviour
{
    #region Constants

    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Destructible")]
    public GameObject foodPrefab;
    public int maxHealth;

    [SerializeField, Header("HealthBar")]
    public Image healthBar;

    #endregion

    #region Properties

    /// <summary>
    /// Current health of the destructible
    /// </summary>
    public int CurrentHealth { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// reduce the damage from the destructible
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
       
        CurrentHealth -= damage;
        Debug.Log(this.name + " took " + damage);
        Debug.Log(this.name + " has " + CurrentHealth);
        Debug.Log(this.name + " has max " + maxHealth);
        healthBar.fillAmount = CurrentHealth / maxHealth;
        Debug.Log(this.name + " has " + healthBar.fillAmount);
        if (CurrentHealth <= 0)
        {
            Die();
            if (gameObject.layer == 9)
            {

            }
            else
            {
                Destroy(gameObject);
                Destructible food = Instantiate(foodPrefab, new Vector3(transform.position.x, transform.position.y + 0.50f, transform.position.z), Quaternion.Euler(new Vector3(180, 90, 90))).GetComponent<Destructible>();
            }

        }
    }

    /// <summary>
    /// the destructible changes layer to death layer
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log(this.name + " died.");
    }

    public bool IsDead()
    {
        if (CurrentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Functions

    protected void Awake()
    {
        CurrentHealth = maxHealth;
        Debug.Log(this.name + " has " + CurrentHealth);
    }
    #endregion

}
