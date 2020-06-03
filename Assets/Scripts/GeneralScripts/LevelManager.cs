using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float waitTime = 2.0f;

    public GameObject currentCheckPoint;
    private GameObject player;
    private PlayerHealth playerHealth;
    private CharacterMovement characterMovement;
    private Animator anim;
    private LifeManager lifeSystem;

	// Use this for initialization
	void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        characterMovement = player.GetComponent<CharacterMovement>();
        anim = player.GetComponent<Animator>();
        lifeSystem = FindObjectOfType<LifeManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(waitTime);
        player.transform.position = currentCheckPoint.transform.position;
        playerHealth.CurrentHealth = playerHealth.StartingHealth;
        playerHealth.HealthSlider.value = playerHealth.CurrentHealth;
        GameManager.instance.PlayerHit(playerHealth.CurrentHealth);
        anim.SetTrigger("isRespawn");
        lifeSystem.TakeLife();
        characterMovement.enabled = true;
    }
}
