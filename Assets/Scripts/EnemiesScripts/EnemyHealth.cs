using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 30;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    private AudioSource audioSource;

    private DropItem dropItem;

    public AudioClip hitAudio;
    public AudioClip deadAudio;

    public GameObject impactEffect;
    public float durationTimeImpactEffect = 1f;

    public bool IsAlive
    {
        get { return isAlive; }
    }

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        weaponCollider = this.GetComponentInChildren<BoxCollider>();
        audioSource = this.GetComponent<AudioSource>();
        dropItem = this.GetComponent<DropItem>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if(dissapearEnemy)
        {
            this.transform.Translate(Vector3.down * dissapearSpeed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                takeHit();
                timer = 0f;
            }
        }
    }

    void takeHit()
    {
        currentHealth -= 10;
        PlayImpactEffect();

        if (currentHealth > 0)
        {
            audioSource.PlayOneShot(hitAudio);
            anim.SetTrigger("isGetHitFront");
        }
        else
        {
            isAlive = false;
            KillEnemy();
        }

    }

    void KillEnemy()
    {
        capsuleCollider.enabled = false;

        if(nav != null)
        {
            nav.enabled = false;
        }
        anim.SetTrigger("isEnemyDead");
        audioSource.PlayOneShot(deadAudio);
        rigidBody.isKinematic = true;
        if(weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }

        if(dropItem != null)
        {
            dropItem.Drop();
        }

        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(2f);

        dissapearEnemy = true;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    void PlayImpactEffect()
    {
        if(impactEffect != null)
        {
            GameObject newImpactEffect = (GameObject)Instantiate(impactEffect, this.transform.position, this.transform.rotation);
            Destroy(newImpactEffect, durationTimeImpactEffect);
        }
    }
}
