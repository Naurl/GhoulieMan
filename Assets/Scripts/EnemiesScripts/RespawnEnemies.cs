using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnEnemies : MonoBehaviour {

    public GameObject[] EnemiesToRespawn;
    public Transform pointToWalk;
    int randomInt;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRespawnEnemy()
    {
        randomInt = Random.Range(0, EnemiesToRespawn.Length);
        if (EnemiesToRespawn[randomInt] != null)
        {
            GameObject enemy = Instantiate(EnemiesToRespawn[randomInt], new Vector3(transform.position.x, transform.position.y, transform.position.z), this.transform.rotation);
            NavMeshAgent nav = enemy.GetComponent<NavMeshAgent>();
            if(nav != null)
            {
                nav.SetDestination(pointToWalk.position);
            }
        }
    }
}
