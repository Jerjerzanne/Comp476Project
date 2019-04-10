using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player information
/// </summary>
public class Player : Destructible
{
    #region Constants

    private const int wallLayer = 13;
    private const int playerLayer = 9;
    private const int entityLayer = 10;
    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Player")]
    private int initialGrowth;
    public int growthMeter;
    public int maxGrowth;

    [SerializeField, Header("Gun")]
    public List<Gun> playerGuns;
    public Gun playerGun;
    public GameObject bulletPrefab;
    public float rateOfFire;
    public float offset = 0.5f;
    public int burstSize = 1;

    [SerializeField, Header("Weapon")]
    public int damage = 2;
    public float bulletSpeed = 2;

    [Header("Ammo")]
    public Text ammoText;
    public int maxAmmo = 30;
    public int ammoCount;
    public int refreshAmmoRate = 3;
    protected bool cooldown;

    #endregion

    #region Properties

    /// <summary>
    /// Current growth level of the player
    /// </summary>
    public int CurrentGrowth { get; set; }


    #endregion

    #region Functions

    protected void Awake()
    {
        base.Awake();
        CurrentGrowth = initialGrowth;
        ammoCount = maxAmmo;
        ammoText.text = "Ammo: " + ammoCount.ToString();
    }

    protected void Update()
    {
        ammoText.text = "Ammo: " + ammoCount.ToString();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Do you reach FireSingle()");
            if (ammoCount > 0)
            {
                playerGun.FireSingle();
                ammoCount --;
                
            }
            //FireSingle();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Debug.Log("Do you reach Burst()");
            if (ammoCount > 0 || ammoCount >= burstSize)
            {
                playerGun.FireBurst();
                ammoCount -= playerGun.burstSize;
            }
        }
        AutoReload();
    }

    public void AutoReload()
    {

        if (ammoCount < maxAmmo && cooldown == false)
        {
            cooldown = true;
            StartCoroutine(ReloadAmmo());

        }

    }

    public IEnumerator ReloadAmmo()
    {
        ammoCount ++;
        yield return new WaitForSeconds(refreshAmmoRate);
        cooldown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == entityLayer && other.gameObject.name == "Food(Clone)")
        {
            UpdateGrowth();
            Destroy(other.gameObject);
        }
    }
    protected void UpdateGrowth()
    {
        if (maxGrowth < 15)
        {
            //For testing, simply change the code below to maxGrowth += 1

            //growthMeter += 1;
            maxGrowth += 1;

            if (true) //(growthMeter >= 10)
            {
                //maxGrowth += 1;
                growthMeter = 0;


                if (maxGrowth % 5 == 0)
                {
                    // Grows in size after 5 growths
                    transform.localScale += new Vector3(0.5F, 0, 0.5F);

                    int gunIndex = (int)maxGrowth / 5;
                    if (gunIndex < 3)
                    {
                        playerGun = playerGuns[gunIndex];
                    }

                }
                else if (maxGrowth % 5 == 1)
                {
                    maxHealth += 10;

                }
                else if (maxGrowth % 5 == 2)
                {
                    TopdownController moveScript = gameObject.GetComponent<TopdownController>();
                    moveScript.speed += 2;
                }
                else if (maxGrowth % 5 == 3)
                {
                    int oldDamage = playerGun.damage;

                    playerGun.damage += 1;
                    Debug.Log("Increase health from " + oldDamage + " to " + playerGun.damage);

                }
                else if (maxGrowth % 5 == 4)
                {
                    playerGun.burstSize++;
                }
            }
        }
    }
    #endregion

}
