using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTestScript : MonoBehaviour
{
    // ------ GENERIC PLAYER MOVEMENT SCRIPT ------
    // - This Script will be a 2D Player movment script using Layer Collision Detection

    // ---- To Do ----
    // - More advanced movement features 
    // --- Run, Momentum jump


    // - To Fix animation delay
    // Set To each transition to the following settings: 
    // ---- Has Exit Time = off
    // ---- Exit Time = 0
    // ---- Fixed Duration = Off
    // ---- transition Turation = 0
    // ---- Can Transition To = Off

    //-- Cache Objects --
    // - Cached Objects eg: RigidBody2D, Animator... ect
    //
    public Rigidbody2D PlayerRB;           //Store the players Rigid Body (Set in the Inspector)
    public Collider2D PlayerCol;           //Store the players BoxCollider2D (Set in the Inspector) ---------- NOT SURE IF NEEDED ----------
    public static Animator PlayerAnimator; //Store the Players Animator for transitions (Set in the Inspector)
    public GameObject BoxColliderObject;

    //-- Player Variables --
    // - Stored Player Specific variables EG: Health, Speed, 
    //

    public float moveSpeed;             //Players move speed (Set in Inspector)
    public float moveSpeedStore;
    public float JumpHeight;            //Players JumpHeight (Set in Inspector)
    public float maxJumpHeight;
    public float GravityScale;

     public float velocity = 0;
    //-- Player Bools --
    // - Store Player Specific bools EG: Facing status, has jumped, current state, is living
    //

    public bool FaceRight = true;       //Check if Player is facing right (Change True/ False dependant on circumstance)
    public static bool isGrounded;             //Check if the Player is grounded to prevent infinate jumping 
    public bool falling;

    //Crouch Code Variables ---- IN TEST ----
    public static bool Crouching;
    private Vector2 FullBoxSize;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();     //Get the Players RigidBody Component
        PlayerAnimator = GetComponent<Animator>();  //Get the Players Animator Component

        // Initialised variables
        //
        moveSpeedStore = moveSpeed;
        isGrounded = false;                          //Initialised to false because the player in this build starts in the air 
    }

    // Update is called once per frame
    void Update()
    {
        // ---------- Check if the Player is Grounded ----------
        if (isGrounded == false)
        {
            ResetAnimationBools();                             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Jumping", true);           //Transition to Correct animation

            if (PlayerRB.velocity.y < maxJumpHeight)
            {
                falling = true;
            }
        }

        if (falling == true)
        {
            Vector2 newVel = PlayerRB.velocity;
            newVel.y += GravityScale * Time.deltaTime;
            PlayerRB.velocity = newVel;
        }
    }

    private void FixedUpdate()
    {
        //Jump Code
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse); //Add force to the PlayerRB
                                                                                                  //isGrounded = false;                             //Set grounded to false to dissable infinate jump
            ResetAnimationBools();                         //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Idle", true);          //Transition to Correct animation
            falling = false;
        }
    }

    //Inneficient and messy way of making sure the correct bool is active at any time
    public static void ResetAnimationBools()
    {
        PlayerAnimator.SetBool("Idle", false);
        PlayerAnimator.SetBool("Jumping", false);
        PlayerAnimator.SetBool("Hurt", false);
        PlayerAnimator.SetBool("Crouch", false);
        PlayerAnimator.SetBool("Climb", false);
        PlayerAnimator.SetBool("Moving", false);
    }
}
