using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;
    private EnemyHealth enemyHealth;

    private AudioSource audioSource;

    public AudioClip attackAudio;
    public bool isRespawnStructure = false;


	// Use this for initialization
	void Start () {

        anim = this.GetComponent<Animator>();
        player = GameManager.instance.Player;
        weaponCollider = this.GetComponentInChildren<BoxCollider>();
        enemyHealth = this.GetComponent<EnemyHealth>();
        audioSource = this.GetComponent<AudioSource>();

        StartCoroutine(attack());
    }
	
	// Update is called once per frame
	void Update () {

        if(Vector3.Distance(this.transform.position, player.transform.position) < range)
        {
            playerInRange = true;
        } else
        {
            playerInRange = false;
        }
		
	}

    IEnumerator attack()
    {
        if (playerInRange && !GameManager.instance.GameOver && enemyHealth.IsAlive)
        {   
            anim.SetTrigger("isAttacking");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(attack());
    }

    void OnEnemyAttack()
    {
        if (attackAudio)
        {
            audioSource.PlayOneShot(attackAudio);
        }

        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
    }

    void OnEnemyAttackFinished()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    void OnRespawnEnemy()
    {
        if(isRespawnStructure)
        {
            RespawnEnemies rp = this.GetComponentInChildren<RespawnEnemies>();
            rp.OnRespawnEnemy();
        }
    }
}
