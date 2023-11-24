using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isWalking = false;
    bool isRunning = false;
    [SerializeField] AudioSource WalkingSound;
    [SerializeField] AudioSource JumpSound;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
   
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    
    [Header("Ground Check")]
    public float playerHight;
    public LayerMask whatIsGround;
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

        if (grounded)
        {
            rb.drag = groundDrag;
           // myAnim.SetBool("isOnGround", true);
        }
        else
        {
            rb.drag = 0;
           // myAnim.SetBool("isOnGround", false);
        }

        if (Input.GetButton("Horizontal") && isWalking == true)
        {
            isWalking = true;
            myAnim.SetBool("isWalking", true);
            WalkingSound.Play();
        }
        

        if (Input.GetButton("Vertical") && isWalking == true)
        {
            isWalking = true;
            myAnim.SetBool("isWalking",true);
            WalkingSound.Play();
        }

        if (isWalking == false)
        {
            myAnim.SetBool("isWalking",false);
        }
    }

   

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void MyInput()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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
            moveSpeed = 10f;
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            moveSpeed = 4f;
            isRunning = false;
        }

        if (isRunning == true)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
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
}   
