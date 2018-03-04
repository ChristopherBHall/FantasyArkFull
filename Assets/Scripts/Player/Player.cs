using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Player : NetworkBehaviour {

    public float health = 100f;
    public float hunger = 100f;
    public float thirst = 100f;
    public float healthMax = 100f;
    public float hungerMax = 100f;
    public float thirstMax = 100f;

    public bool isDying = false;

    private Image healthBar;
    private Image hungerBar;
    private Image thirstBar;

    public float hungerSpeedMultiplier = 0.10f;
    public float thirstSpeedMultiplier = 0.25f;
    public float healthSpeedMultiplier = 0.25f;

    private GameObject deathCanvas;
    public Vector3 spawnPoint = Vector3.zero;
    // Use this for initialization
    public void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        hungerBar = GameObject.FindGameObjectWithTag("HungerBar").GetComponent<Image>();
        thirstBar = GameObject.FindGameObjectWithTag("ThirstBar").GetComponent<Image>();
        deathCanvas = GameObject.FindGameObjectWithTag("DeathCanvas");
        //gameObject.name = gameObject.GetComponent<NetworkIdentity>().assetId.ToString();

        
    }
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            CheckDeath();


            if (hunger > 0)
            {
                hunger -= Time.deltaTime * hungerSpeedMultiplier;
            }
            if (thirst > 0)
            {
                thirst -= Time.deltaTime * thirstSpeedMultiplier;
            }
            if (hunger <= 0 || thirst <= 0)
            {
                isDying = true;

            }
            else { isDying = false; }
            if (thirst > thirstMax)
            {
                thirst = thirstMax;
            }
            if (hunger > hungerMax)
            {
                hunger = hungerMax;
            }
            if (health > healthMax)
            {
                health = healthMax;
            }

            if (isDying == true)
            {
                health -= Time.deltaTime * healthSpeedMultiplier;
            }

            hungerBar.fillAmount = hunger / 100;
            thirstBar.fillAmount = thirst / 100;
            healthBar.fillAmount = health / 100;
        }
	}

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        deathCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void Respawn()
    {
        deathCanvas.SetActive(false);
        transform.position = spawnPoint;
        hunger = 100;
        thirst = 100;
    }

    public void AddHealth(float amount)
    {
        health += amount;
    }
    public void AddHunger(float amount)
    {
        hunger += amount;
    }
    public void AddThirst(float amount)
    {
        thirst += amount;
    }
}
