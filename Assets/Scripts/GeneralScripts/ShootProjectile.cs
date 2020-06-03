using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour {

    public float projectileSpeed = 600.0f;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawn;
    public string projectileTag;

    public AudioClip projectileAudio;

    private AudioSource audioSource;
    private Rigidbody clone;



    // Use this for initialization
    void Start () {
        audioSource = this.GetComponent<AudioSource>();
        projectileSpawn = this.transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallFireProjectile()
    {
        clone = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation) as Rigidbody;
        clone.AddForce(-projectileSpawn.transform.right * projectileSpeed);

        if(projectileTag != null && projectileTag != "")
        {
            clone.gameObject.tag = projectileTag;
        }

        if (projectileAudio != null)
        {
            audioSource.PlayOneShot(projectileAudio);
        }
    }
}
