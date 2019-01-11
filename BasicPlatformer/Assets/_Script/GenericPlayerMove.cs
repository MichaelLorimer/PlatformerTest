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

    public Rigidbody2D PlayerRB;        //Store the players Rigid Body (Set in the Inspector)
    public Collider2D PlayerCol;    //Store the players BoxCollider2D (Set in the Inspector) ---------- NOT SURE IF NEEDED ----------
    public static Animator PlayerAnimator; //Store the Players Animator for transitions (Set in the Inspector)

    //-- Player Variables --
    // - Stored Player Specific variables EG: Health, Speed, 
    //

    public float moveSpeed;             //Players move speed (Set in Inspector)
    public float moveSpeedStore;
    public float JumpHeight;            //Players JumpHeight (Set in Inspector)
    public float maxJumpHeight;
    public float GravityScale;

    public bool falling;
    public float velocity = 0;
    //-- Player Bools --
    // - Store Player Specific bools EG: Facing status, has jumped, current state, is living
    //

    public bool FaceRight = true;       //Check if Player is facing right (Change True/ False dependant on circumstance)
    public static bool isGrounded;             //Check if the Player is grounded to prevent infinate jumping 

    
    //Initiasise variables on startup of application
    void Start()
    {
        //Cache Components 
        //

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
        //-- Keyboard Controlls --
        // -- Get the users Keyboard input and decide what to do with it 
        //

        // -- Move Left
        if (Input.GetKey(KeyCode.A))
        {
            //Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            //Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            FaceRight = false;                         //Set RaceRight to false to flip the sprite 
            //PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added to

            ResetAnimationBools();                    //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Moving", true);   //Transition t0 Correct animation
        }

        // -- Move Right
        if (Input.GetKey(KeyCode.D))
        {
            //Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            //Pos.x += moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            FaceRight = true;                          //Set RaceRight to True to flip the sprite 
            //PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added to

            ResetAnimationBools();                     //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Moving", true);   //Transition to Correct animation
        }

        // -- Crouch 
        if (Input.GetKey(KeyCode.S))
        {
            //float xVel = 0;
            //PlayerRB.velocity = new Vector2(xVel, PlayerRB.velocity.y);
            ResetAnimationBools();                   //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Crouch", true);        //Transition to Correct animation
        }
        if (Input.GetKeyUp(KeyCode.S))         // check for then the "S" key is released to re enable movement
        {
           // float xVel = 0;
            //PlayerRB.velocity = new Vector2(xVel, PlayerRB.velocity.y);

            //moveSpeed = moveSpeedStore;                          //Move speed = 3 (Should be set to a variable to preserve data)
            ResetAnimationBools();                  //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool("Idle", true);         //Transition to Correct animation
        }

        

        
        //Check if the player is not grounded to enable Animation
        if (isGrounded == false)
        {
            ResetAnimationBools();                             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Jumping", true);                 //Transition to Correct animation

            if (PlayerRB.velocity.y < maxJumpHeight)
            {
                falling = true;
            }
        }

        //To Check if the sprite needs to be flipped
        FlipSprite();

        if (falling == true)
        {
            Vector2 newVel = PlayerRB.velocity;
            newVel.y += GravityScale * Time.deltaTime;
            PlayerRB.velocity = newVel;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
                                                       //FaceRight = false;                         //Set RaceRight to false to flip the sprite 
            PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
           // PlayerRB.MovePosition(Pos);                                         //PlayerRB.transform.position cannot be added to
            
            //ResetAnimationBools();                    //Reset the animation bools for the next transition  
            //PlayerAnimator.SetBool("Moving", true);   //Transition t0 Correct animation
        }

        // -- Move Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x += moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
                                                       //FaceRight = true;                          //Set RaceRight to True to flip the sprite 
            PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
            //PlayerRB.MovePosition(Pos);                                          //PlayerRB.transform.position cannot be added to

            //ResetAnimationBools();                     //Reset the animation bools for the next transition  
            //PlayerAnimator.SetBool("Moving", true);   //Transition to Correct animation
        }

        // -- Crouch 
        if (Input.GetKey(KeyCode.S))
        {
            float xVel = 0;
            PlayerRB.velocity = new Vector2(xVel, PlayerRB.velocity.y);
            //ResetAnimationBools();                   //Reset the animation bools for the next transition  
            //PlayerAnimator.SetBool("Crouch", true);        //Transition to Correct animation
        }
        if (Input.GetKeyUp(KeyCode.S))         // check for then the "S" key is released to re enable movement
        {
            float xVel = 0;
            PlayerRB.velocity = new Vector2(xVel, PlayerRB.velocity.y);

            moveSpeed = moveSpeedStore;                          //Move speed = 3 (Should be set to a variable to preserve data)
            //ResetAnimationBools();                  //Reset the animation bools for the next transition  
            //PlayerAnimator.SetBool("Idle", true);         //Transition to Correct animation
        }
        //Get the users input and check if it is the Space bar
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {

            PlayerRB.AddForce(new Vector2(0f, JumpHeight) * Time.deltaTime, ForceMode2D.Impulse); //Add force to the PlayerRB
                                                                                                  //isGrounded = false;                             //Set grounded to false to dissable infinate jump
            ResetAnimationBools();                         //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Idle", true);          //Transition to Correct animation
            falling = false;
        }
    }


    //Check if Grounded
    void OnCollisionStay2D(Collision2D coll) //coll = Other Collider
    {
        
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
}