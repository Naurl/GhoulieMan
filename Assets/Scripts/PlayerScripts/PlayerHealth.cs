using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {


    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeSinceLastHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] Slider healthSlider;

    private CharacterMovement characterMovement;
    private AudioSource audioSource;
    private float timer = 0f;
    private Animator anim;

    public LevelManager levelManager;

    public AudioClip hitAudio;
    public AudioClip deadAudio;

    public LifeManager lifeManager;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            if(value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public int StartingHealth
    {
        get
        {
            return startingHealth;
        }
    }

    public Slider HealthSlider
    {
        get
        {
            return healthSlider;
        }
    }

    void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }


    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement>();
        audioSource = this.GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        lifeManager = FindObjectOfType<LifeManager>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                takeHit();
                timer = 0;
            }
        }
    }

    void takeHit()
    {
        currentHealth -= 10;

        if (currentHealth > 0)
        {
            healthSlider.value = currentHealth;
            GameManager.instance.PlayerHit(currentHealth);
            anim.SetTrigger("isGetHitFront");

            if (hitAudio != null)
            {
                audioSource.PlayOneShot(hitAudio);
            }
        }
        else
        {
            KillPlayer();
        }
    }

    public void PowerUpHealth(int powerfull)
    {
        if(currentHealth <= startingHealth- powerfull)
        {
            currentHealth += powerfull;
        }
        else if(currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }

        healthSlider.value = currentHealth;
    }

    public void KillPlayer()
    {
        currentHealth = 0;
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("isDead");
        if (deadAudio != null)
        {
            audioSource.PlayOneShot(deadAudio);
        }
        characterMovement.enabled = false;
        healthSlider.value = currentHealth;

        levelManager.RespawnPlayer();
    }

    public void ExtraLife(int powerfull)
    {
        lifeManager.Givelife();
    }
}
