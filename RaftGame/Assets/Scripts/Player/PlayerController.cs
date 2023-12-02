using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Vars
    Rigidbody2D rb;
    PlayerInput playerInput;
    InputActions playerInputActions;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundCheckDistance;
    [SerializeField] Vector2 input;
    [Header("Stats")]
    [SerializeField] float runSpeed;
    [SerializeField] float runAccel, runDeccel, speedMuti, runForce, jumpHeight, jumpCancelForce, maximumFallVel, reach;
    bool grounded, isRight = true, canMove = true, hasLanded; 
    
    [Header("MiscTimers")]
    [SerializeField] float cyoteTime, cyoteLeft;
    #endregion
    
    void Awake(){
        rb = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new InputActions();
        playerInputActions.Player.Enable();
    }

    void Start(){
    }

    void Update(){
        input = GetInput();
    }

    void FixedUpdate(){

        grounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckDistance, groundLayer);

        if(!grounded && hasLanded){
            hasLanded = false;
        }
        
        if(!hasLanded && grounded){
            hasLanded=!hasLanded;
        }

        cyoteLeft = grounded ? cyoteTime : cyoteLeft > 0 ? cyoteLeft -= Time.deltaTime : cyoteLeft = 0;
        Movement();
    }
    void Movement(){
        float targetSpeed = input.x * runSpeed * speedMuti;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? runAccel : runDeccel;
        float final = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, runForce) * Mathf.Sign(speedDiff);
        if(canMove){rb.AddForce(final * Vector2.right);}
        
        if(isRight && input.x < 0){Flip();}
        else if(!isRight && input.x > 0){Flip();}
    }
    void Jump(bool jump = true){
        if(grounded && jump || jump && cyoteLeft > 0){rb.velocity = new Vector2(rb.velocity.x,0); rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);}
        else if(!grounded && !jump && rb.velocity.y > 0){rb.AddForce(Vector2.down * rb.velocity.y * (1 + jumpCancelForce));}
    }

    void Flip(){transform.Rotate(new Vector3(0,180,0));isRight = !isRight;} //Flips character 

    #region Input
    public Vector2 GetInput(){ //Returns player input as vector 2
        return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }
    public void JumpInput(InputAction.CallbackContext ctx){
        if(ctx.performed){Jump();}
        if(ctx.canceled){Jump(false);}
    }

    #endregion

    public Vector2 ReturnMousePos(){
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
        return worldPos;
    }
    public Vector2 ReturnPos() => transform.position;
    public float ReturnReach() => reach;
}




    

