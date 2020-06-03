using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

	public GameObject[] items;
	int randomInt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drop () {
		randomInt = Random.Range (0, items.Length);
        if(items[randomInt] != null)
        {
            Instantiate(items[randomInt], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
	}
}
