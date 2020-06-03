using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    private Rigidbody rigidBody;
    private Animator animator;

    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float knifeSpeed = 600.0f;
    public Transform knifeSpawn;
    public Rigidbody knifePrefab;

    public AudioClip projectileAudio;

    private AudioSource audioSource;
    private Rigidbody clone;

    private bool startCorrutine = false;

    private void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        knifeSpawn = GameObject.Find("KnifeSpawn").transform;
    }

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        moveDirection = Input.GetAxis("Horizontal");

        if(grounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("isJumping");
            rigidBody.AddForce(new Vector2(0, jumpSpeed));
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rigidBody.velocity = new Vector2(moveDirection * maxSpeed, rigidBody.velocity.y);
        //Quaternion targetRotation = this.transform.rotation;
        //targetRotation.y += 180.0f * moveDirection;

        if (moveDirection > 0.0f && !facingRight)
        {
            // targetRotation.y += 180.0f;
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            // targetRotation.y += 180.0f;
            Flip();
        }

        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 0.3f);

        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    void Flip()  
    {
        //float smooth = 2.0f;
        //facingRight = !facingRight;
        //this.transform.Rotate(Vector3.up, 180.0f * Time.deltaTime, Space.World);

        if(!startCorrutine)
        {
            
            StartCoroutine(SmoothPlayerFlip(0.0f));
        }
        //Quaternion targetRotation = Quaternion.FromToRotation(new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z), new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, this.transform.rotation.y + 180.0f,0), Time.deltaTime * smooth);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, 10.0f * Time.deltaTime);
    }

    void Attack()
    {
        animator.SetTrigger("isAttaking");
    }

    public void CallFireProjectile()
    {
        clone = Instantiate(knifePrefab, knifeSpawn.position, knifeSpawn.rotation) as Rigidbody;
        clone.AddForce(knifeSpawn.transform.right * knifeSpeed);
        audioSource.PlayOneShot(projectileAudio);
    }

    IEnumerator SmoothPlayerFlip(float turnedAngle)
    {
        startCorrutine = true;
        float objetiveAngle = 180.0f;
        float turnSpeed = 5.0f;

        if(turnedAngle < objetiveAngle)
        {
            float currentTurnAngle = objetiveAngle * Time.deltaTime * turnSpeed;
            if(turnedAngle + currentTurnAngle > objetiveAngle)
            {
                currentTurnAngle = objetiveAngle - turnedAngle;
            }

            this.transform.Rotate(Vector3.up, currentTurnAngle, Space.World);
            turnedAngle += currentTurnAngle;

            yield return new WaitForFixedUpdate();
            StartCoroutine(SmoothPlayerFlip(turnedAngle));
        }
        else
        {
            facingRight = !facingRight;
            startCorrutine = false;
        }
    }
}
