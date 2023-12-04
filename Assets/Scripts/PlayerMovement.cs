using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isWalking;
    bool isRunning;
    public bool isAbleToKill = false;
    [SerializeField] AudioSource WalkingSound;
    [SerializeField] AudioSource JumpSound;
    [SerializeField] AudioSource StabSound;
    

    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [SerializeField] float groundDrag;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump;
   
    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    
    [Header("Ground Check")]
    [SerializeField] float playerHight;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Animator myAnim;

    Rigidbody rb;

    private void Start()
    {
        //myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (targetVelocity.x != 0 || targetVelocity.z != 0 && grounded)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;   
        }
        
        if (isWalking == false)
        {
            myAnim.SetBool("isWalking",false);
            WalkingSound.Play();
        }
    }

   

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            myAnim.SetBool("Jumped",true);
            myAnim.SetBool("isOnGround", false);

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

            JumpSound.Play();
        }

        if (Input.GetKey(runKey))
        {
            moveSpeed = 7f;
            myAnim.SetBool("isRunning", true);
            myAnim.SetBool("isWalking", false);
        }
        else
        {
            moveSpeed = 3f;
            isRunning = false;
        }

        if (isRunning == true)
        {
            myAnim.SetBool("isRunning", true);
            myAnim.SetBool("isWalking", false);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isWalking", true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Stab();
        }
        else
        {
            isAbleToKill = false;
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            myAnim.SetBool("Jumped", false);
            myAnim.SetBool("isOnGround", true);
        }
            

        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);     
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Stab()
    {
        myAnim.SetTrigger("Stab");
        StabSound.Play();
        isAbleToKill = true;
        //Debug.Log("is able to kill");
    }
}   
