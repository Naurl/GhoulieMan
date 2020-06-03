using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMevement : MonoBehaviour {

    [SerializeField] Vector3 rightPosition;
    [SerializeField] Vector3 leftPosition;
    [SerializeField] float speed;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Move(leftPosition));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move(Vector3 target)
    {
        while (Mathf.Abs((target - this.transform.localPosition).x) > 0.20f)
        {
            Vector3 direction = target.x == rightPosition.x ? Vector3.right : Vector3.left;
            this.transform.localPosition += direction * Time.deltaTime * speed;

            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        Vector3 newTarget = target.x == rightPosition.x ? leftPosition : rightPosition;

        StartCoroutine(Move(newTarget));
    }
}
