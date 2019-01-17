using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayerMove : MonoBehaviour
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

    //-- Player Bools --
    // - Store Player Specific bools EG: Facing status, has jumped, current state, is living
    //

    public bool FaceRight = true;       //Check if Player is facing right (Change True/ False dependant on circumstance)
    public static bool isGrounded;             //Check if the Player is grounded to prevent infinate jumping 
    public bool falling;

    //Crouch Code Variables ---- IN TEST ----
    public static bool Crouching;
    private Vector2 FullBoxSize;





    //- TEST JUMP VALUES!!
    public float TestJumpForce;
    public float TestJumpTime;
    public float TestJumpCounter;
    public bool stoppedJumping;

    // Start is called before the first frame update
    void Start()
    {
        //- TEST JUMP VALUES!!
        TestJumpCounter = TestJumpTime;
    //Cache Components 
    //

    PlayerRB = GetComponent<Rigidbody2D>();     //Get the Players RigidBody Component
        PlayerAnimator = GetComponent<Animator>();  //Get the Players Animator Component


        // Initialised variables
        //
        moveSpeedStore = moveSpeed;
        isGrounded = false;                          //Initialised to false because the player in this build starts in the air 
        Crouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Set Bools and animations
        if (Input.GetKey(KeyCode.A))
        {
            FaceRight = false;                         //Set RaceRight to false to flip the sprite 
            ResetAnimationBools();                    //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Moving", true);   //Transition t0 Correct animation
        }
        if (Input.GetKey(KeyCode.D))
        {
            FaceRight = true;                          //Set RaceRight to True to flip the sprite
            ResetAnimationBools();                     //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Moving", true);   //Transition to Correct animation
        }
        // -- Crouch 
        if (Input.GetKey(KeyCode.S))
        {
            ResetAnimationBools();                   //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Crouch", true);        //Transition to Correct animation
        }
        if (Input.GetKeyUp(KeyCode.S))         // check for then the "S" key is released to re enable movement
        {
            ResetAnimationBools();                  //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Idle", true);         //Transition to Correct animation
        }

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
        if(isGrounded == true)
        { //- TEST JUMP VALUES!!
            TestJumpCounter = TestJumpTime;
        }

        if (falling == true)
        {
            Vector2 newVel = PlayerRB.velocity;
            newVel.y += GravityScale * Time.deltaTime;
            PlayerRB.velocity = newVel;
        }

        if (Crouching == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            BoxColliderObject.GetComponent<BoxCollider2D>().enabled = true;

            ResetAnimationBools();
            PlayerAnimator.SetBool("Crouch", true);
        }
        else if (Crouching == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            BoxColliderObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        //To Check if the sprite needs to be flipped
        FlipSprite();
    }

    private void FixedUpdate()
    {
        //==== Movement Controls ====
        //---------------------------

        //Left
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
                                                       //FaceRight = false;                         //Set RaceRight to false to flip the sprite 
            PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
        }

        //Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x += moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
                                                       //FaceRight = true;                          //Set RaceRight to True to flip the sprite 
            PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
        }

        //Crouch Code
        if (Input.GetKeyDown(KeyCode.S))         // check for then the "S" key is released to re enable movement
        {
            moveSpeed = moveSpeed / 2;
            Crouching = true;
        }
        if (Input.GetKeyUp(KeyCode.S))         // check for then the "S" key is released to re enable movement
        {
            moveSpeed = moveSpeedStore;
            Crouching = false;
        }

        //Jump Code
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse); //Add force to the PlayerRB
                                                                                                  //isGrounded = false;                             //Set grounded to false to dissable infinate jump
            ResetAnimationBools();                         //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Idle", true);          //Transition to Correct animation
            
        }

        //Test Jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded == true)
            {
                PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse);
                stoppedJumping = false;
                falling = false;
            }
        }

        if ((Input.GetKey(KeyCode.W)) && !stoppedJumping)
        {
            if (TestJumpCounter > 0f)
            {
                PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse);
                TestJumpCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            TestJumpCounter = 0;
            stoppedJumping = true;
        }
    }

    //Function to flip sprite around 
    void FlipSprite()
    {
        if (FaceRight == true)
        {
            transform.localScale = new Vector2(1, 1); //If Right: flip to the Right = (x1, y1)
        }
        if (FaceRight == false)
        {
            transform.localScale = new Vector2(-1, 1); //If Left: flip to the left = (x-1, y1)
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


    
    public void TestJump()
    {
        TestJumpCounter -= Time.deltaTime;
        PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse); //Add force to the PlayerRB
                                                                                              //isGrounded = false;                             //Set grounded to false to dissable infinate jump
        ResetAnimationBools();                         //Reset the animation bools for the next transition
        PlayerAnimator.SetBool("Idle", true);          //Transition to Correct animation
        falling = false;
    }
}