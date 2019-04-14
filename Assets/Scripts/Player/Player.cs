using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the player information
/// </summary>
public class Player : Destructible
{
    #region Constants

    private const int wallLayer = 13;
    private const int playerLayer = 9;
    private const int entityLayer = 10;
    private const int thresholdInterval = 5;
    private const int maxGrowMeter = 5;
    private const float defaultColliderRadius = 0.375f;

    #endregion

    #region Variables

    //Editor variables
    [SerializeField, Header("Player")]
    private int initialGrowth;
    public int growthMeter;
    public int maxGrowth;
    public List<Sprite> playerSprites;

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
    private float refreshAmmoRate = 1.5f;
    protected bool cooldown;

    [Header("UI")]
    public Image damageImage;
    public Text growthText;
    public Image growthBar;
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     //The colour the damageImage is set to, to flash.

    #endregion

    #region Method

    protected override void Die()
    {
        base.Die();
        Debug.Log("You died! game over...");
        SceneManager.LoadScene("GameOver");
    }

    #endregion

    #region Functions

    void UpdateMaxAmmo()
    {
        playerGun.ammoCount = playerGun.maxAmmo;

        ammoText.text = "Ammo: " + playerGun.ammoCount.ToString();
    }

    protected void Awake()
    {
        base.Awake();
        CurrentGrowth = initialGrowth;
        UpdateMaxAmmo();
    }

    protected void Update()
    {
        //if(CurrentHealth <= 0)
        //{
        //    SceneManager.LoadScene("GameOver");
        //}
        ammoText.text = "Ammo: " + playerGun.ammoCount.ToString();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (playerGun.ammoCount > 0 && playerGun.ammoCount >= playerGun.bulletCount)
            {
                playerGun.FireSingle();                
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (playerGun.ammoCount > 0 && playerGun.ammoCount >= playerGun.burstSize)
            {
                //Debug.Log("Burst size is" + playerGun.burstSize);
                playerGun.FireBurst();
            }
        }
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            playerGun = playerGuns[0];
        }
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            if (maxGrowth >= thresholdInterval * 1)
            {
                playerGun = playerGuns[1];
            }
        }
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            if (maxGrowth >= thresholdInterval * 2)
            {
                playerGun = playerGuns[2];
            }
        }

        // Testing demo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateGrowth();
        }

        damagedUI();
        AutoReload();
    }
    public void damagedUI()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void AutoReload()
    {
        if (playerGun.ammoCount < playerGun.maxAmmo && cooldown == false)
        {
            cooldown = true;
            StartCoroutine(ReloadAmmo());
        }

    }

    public IEnumerator ReloadAmmo()
    {
        playerGun.ammoCount ++;
        yield return new WaitForSeconds(refreshAmmoRate);
        cooldown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Food(Clone)")
        {
            CurrentHealth += 10;
            CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
            updateHealthUI();
            UpdateGrowth();
            Destroy(other.gameObject);
        }
    }
    protected void UpdateGrowth()
    {
        if (maxGrowth <= 15)
        {
            growthMeter += 1;

            if (growthMeter >= maxGrowMeter)
            {
                maxGrowth += 1;
                growthMeter = 0;

                if (maxGrowth % thresholdInterval == 0)
                {
                    int growthIndex = (int)maxGrowth / thresholdInterval;
                    if (growthIndex < 3)
                    {
                        CurrentGrowth = growthIndex;
                        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
                        gameObject.GetComponent<SphereCollider>().radius = defaultColliderRadius + growthIndex * 0.25f;

                        foreach (SpriteRenderer r in renderers)
                        {
                            r.sprite = playerSprites[growthIndex];
                        }
                        playerGun = playerGuns[growthIndex];
                    }
                }
                else if (maxGrowth % thresholdInterval == 1)
                {
                    maxHealth += 10;

                }
                else if (maxGrowth % thresholdInterval == 2)
                {
                    TopdownController moveScript = gameObject.GetComponent<TopdownController>();
                    moveScript.speed += 2;
                }
                else if (maxGrowth % thresholdInterval == 3)
                {
                    playerGun.damage += 1;

                }
                else if (maxGrowth % thresholdInterval == 4)
                {
                    playerGun.burstSize++;
                }
            }
        }

        if (growthBar != null)
        {
            Debug.Log(this.name + " has fill amount" + growthMeter + " " + maxGrowMeter);
            growthBar.fillAmount = (float)growthMeter / maxGrowMeter;
            Debug.Log(this.name + " has fill amount" + growthBar.fillAmount);
        }
        growthText.text = "Growth Level: " + maxGrowth.ToString();
    }

    #endregion
}
