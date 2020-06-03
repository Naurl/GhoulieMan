using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestruction : MonoBehaviour {

    public float lifeSpan = 2.0f;
    //private Rigidbody rigidBody;
    //private BoxCollider boxCollider;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, lifeSpan);
        //rigidBody = this.GetComponent<Rigidbody>();
        //boxCollider = this.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "Weapon")
        {
            if (other.gameObject && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Weapon" && other.gameObject.tag != "Item")
            {
                if(other.gameObject.tag == "PlayerWeapon")
                {
                    Destroy(this.gameObject);
                    // Poner anumacion de choque de projectiles del enemigo
                    //rigidBody.mass = 100000;
                    //rigidBody.AddForce(-this.transform.up * 10000.0f);
                    //rigidBody.useGravity = true;
                    //rigidBody.rotation = Quaternion.Lerp(rigidBody.rotation, new Quaternion(rigidBody.rotation.x, rigidBody.rotation.y, rigidBody.rotation.z - 90.0f, rigidBody.rotation.w), 0.2f * Time.deltaTime);
                    //boxCollider.isTrigger = false;
                    //StartCoroutine(WaitBeforeDestroy());
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else if(this.gameObject.tag == "PlayerWeapon")
        {
            if (other.gameObject)
            {
                if (other.gameObject.tag == "Weapon")
                {
                    Destroy(this.gameObject);
                    // Poner anumacion de choque de projectiles del enemigo
                    //rigidBody.mass = 100000;
                    //rigidBody.AddForce(-this.transform.up * 10000.0f);
                    //rigidBody.useGravity = true;
                    //rigidBody.rotation = Quaternion.Lerp(rigidBody.rotation, new Quaternion(rigidBody.rotation.x, rigidBody.rotation.y, rigidBody.rotation.z - 90.0f, rigidBody.rotation.w), 0.2f * Time.deltaTime);
                    //boxCollider.isTrigger = false;
                    //StartCoroutine(WaitBeforeDestroy());
                }
                else if(other.gameObject.tag != "Item")
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject)
            {
                
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(this.gameObject);
    }

}
