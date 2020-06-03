using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour {

    [SerializeField] Vector3 topPosition;
    [SerializeField] Vector3 bottonPosition;
    [SerializeField] float speed;

	// Use this for initialization
	void Start () {
        StartCoroutine(Move(bottonPosition));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Move (Vector3 target)
    {
        while(Mathf.Abs((target- this.transform.localPosition).y) > 0.20f)
        {
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
            this.transform.localPosition += direction * Time.deltaTime * speed;

            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        Vector3 newTarget = target.y == topPosition.y ? bottonPosition : topPosition;

        StartCoroutine(Move(newTarget));
    }
}
