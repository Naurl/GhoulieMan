using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public float xMargin = 1.0f;
    public float yMargin = 1.0f;

    public float xSmooth = 5.0f;
    public float ySmooth = 5.0f;

    public Vector2 maxXandY;
    public Vector2 minXandY;

    public Transform cameraTarget;

    private void Awake()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").transform;
        maxXandY = new Vector2(500.0f,100.0f);
        minXandY = new Vector2(11.0f, -15.0f);
    }

    private bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - cameraTarget.position.x) > xMargin;
    }

    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - cameraTarget.position.y) > yMargin;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, cameraTarget.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, cameraTarget.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
