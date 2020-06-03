using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour {

    public LevelManager levelManager;
    public ParticleSystem particle_System;
    public BoxCollider boxCollider;
    private AudioSource audioSource;
    public AudioClip audioClip;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        particle_System = GetComponentInChildren<ParticleSystem>();
        boxCollider = this.GetComponent<BoxCollider>();
        audioSource = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if(levelManager.currentCheckPoint != null)
            {
                Destroy(levelManager.currentCheckPoint);
            }

            if(audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }

            var em = particle_System.emission;
            em.enabled = true;
            boxCollider.enabled = false;
            levelManager.currentCheckPoint = gameObject;
        }
    }
}
