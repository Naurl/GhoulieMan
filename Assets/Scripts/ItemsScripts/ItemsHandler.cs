using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHandler : MonoBehaviour {

    public int itemEffectForce = 20;
    private GameObject player;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private SphereCollider sphereCollider;
    private MeshRenderer meshRenderer;
    private ParticleSystem thisParticleSystem;
    private Rigidbody rigidBody;

    private AudioSource audioSource;
    public AudioClip itemClip;

    public GameObject pickUpEffect;

    // Use this for initialization
    void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        audioSource = this.GetComponent<AudioSource>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        sphereCollider = this.GetComponent<SphereCollider>();
        meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        thisParticleSystem = this.GetComponent<ParticleSystem>();
        rigidBody = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if(this.gameObject.name.Contains("HealthItem") && playerHealth.CurrentHealth < playerHealth.StartingHealth)
            {
                playerHealth.PowerUpHealth(itemEffectForce);

                if (itemClip != null)
                {
                    audioSource.PlayOneShot(itemClip);
                }

                spriteRenderer.enabled = false;
                sphereCollider.isTrigger = false;
                rigidBody.detectCollisions = false;

                Destroy(this.gameObject, 1.0f);
            }
            else if(this.gameObject.name.Contains("PowerItem") && playerHealth.enabled)
            {
                PickUp();
                meshRenderer.enabled = false;
                sphereCollider.isTrigger = false;
                rigidBody.detectCollisions = false;

                var em = thisParticleSystem.emission;
                em.enabled = false;
                StartCoroutine(Invencibility(itemEffectForce));
            }
            else if(this.gameObject.name.Contains("LifeItem"))
            {
                
                if (itemClip != null)
                {
                    audioSource.PlayOneShot(itemClip);
                }

                playerHealth.ExtraLife(itemEffectForce);
                spriteRenderer.enabled = false;
                sphereCollider.isTrigger = false;
                rigidBody.detectCollisions = false;
                Destroy(this.gameObject, 1.0f);
            }
        }
    }

    IEnumerator Invencibility(float time)
    {
        playerHealth.enabled = false;
        ParticleSystem particleSystem = player.GetComponent<ParticleSystem>();
        var em = particleSystem.emission;
        em.enabled = true;

        if (itemClip != null)
        {
            audioSource.clip = itemClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        yield return new WaitForSeconds(time);
        audioSource.Stop();
        playerHealth.enabled = true;
        em.enabled = false;
        Destroy(this.gameObject);

    }

    void PickUp ()
    {
        GameObject newPickUpEffect = (GameObject)Instantiate(pickUpEffect, this.transform.position, this.transform.rotation);
        Destroy(newPickUpEffect, 1);
    }
}
