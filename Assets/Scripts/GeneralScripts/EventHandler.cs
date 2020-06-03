using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    private CharacterMovement characterMevement;

    private void Awake()
    {
        characterMevement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FireProjectile()
    {
        characterMevement.CallFireProjectile();
    }
}
