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

	public Rigidbody2D PlayerRB; 		//Store the players Rigid Body (Set in the Inspector)
	public Collider2D  PlayerCol;   	//Store the players BoxCollider2D (Set in the Inspector) ---------- NOT SURE IF NEEDED ----------
	public Animator    PlayerAnimator;	//Store the Players Animator for transitions (Set in the Inspector)

	//-- Player Variables --
    // - Stored Player Specific variables EG: Health, Speed, 
    //

	public float moveSpeed;			    //Players move speed (Set in Inspector)
	public float JumpHeight;		    //Players JumpHeight (Set in Inspector)

    //-- Player Bools --
    // - Store Player Specific bools EG: Facing status, has jumped, current state, is living
    //

	public bool FaceRight = true;       //Check if Player is facing right (Change True/ False dependant on circumstance)
	public bool isGrounded;             //Check if the Player is grounded to prevent infinate jumping 

    //Initiasise variables on startup of application
	void Start () 
	{
        //Cache Components 
        //

		PlayerRB = GetComponent<Rigidbody2D> ();     //Get the Players RigidBody Component
		PlayerAnimator = GetComponent<Animator> ();  //Get the Players Animator Component

        // Initialised variables
        //

		isGrounded = false;                          //Initialised to false because the player in this build starts in the air 
	}

    //Fixed update should be used for physics, it has its own update loop/ time? -------------- Fill out more -------------------------
    void FixedUpdate()
    {
    }

	// Update is called once per frame
	void Update () 
	{
		//-- Keyboard Controlls --
        // -- Get the users Keyboard input and decide what to do with it 
        //

		// -- Move Left
		if (Input.GetKey (KeyCode.A)) 
		{
			Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
			Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
			FaceRight = false;                         //Set RaceRight to false to flip the sprite 
			PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added to

			ResetAnimationBools ();                    //Reset the animation bools for the next transition  
			PlayerAnimator.SetBool ("Moving", true);   //Transition t0 Correct animation
		}

		// -- Move Right
		if (Input.GetKey (KeyCode.D)) 
		{
			Vector2 Pos = PlayerRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x += moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            FaceRight = true;                          //Set RaceRight to True to flip the sprite 
            PlayerRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added to

            ResetAnimationBools();                     //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool ("Moving", true);   //Transition to Correct animation
        }

        // -- Crouch 
		if (Input.GetKey (KeyCode.S)) 
		{
			moveSpeed = 0f;                           //While crouching movement is stopped = 0;
			ResetAnimationBools ();                   //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool ("Crouch", true);        //Transition to Correct animation
        } 
		else if (Input.GetKeyUp (KeyCode.S))         // check for then the "S" key is released to re enable movement
		{
			moveSpeed = 3f;                          //Move speed = 3 (Should be set to a variable to preserve data)
			ResetAnimationBools ();                  //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool ("Idle", true);         //Transition to Correct animation
        }

        // -- Jump
        //Check if the player is grounded to enable jumps
        if (isGrounded == true)         
        {
            //Get the users input and check if it is the Space bar
			if (Input.GetKey (KeyCode.Space))
            {
				PlayerRB.AddForce (new Vector2 (0, JumpHeight)); //Add force to the PlayerRB
				isGrounded = false;                             //Set grounded to false to dissable infinate jump

				ResetAnimationBools ();                         //Reset the animation bools for the next transition
                PlayerAnimator.SetBool ("Idle", true);          //Transition to Correct animation
            }
		}
        //Check if the player is not grounded to enable Animation
        else if (isGrounded == false) 
		{
			ResetAnimationBools ();                             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool ("Jumping", true);                 //Transition to Correct animation
        }
        //To Check if the sprite needs to be flipped
        FlipSprite(); 
	}

	//Check if Grounded
	void OnCollisionStay2D(Collision2D coll) //coll = Other Collider
	{
        //If Grounded: 
        //If the Player interacts with the layer 8 the player is grounded
        //Note: "Ground" in the Inspector is Layer 8
        if (coll.gameObject.layer == 8) 
		{
			isGrounded = true;                  //Set isGrounded to true 
			ResetAnimationBools ();             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool ("Idle", true);    //Transition to Correct animation
		} 
		else 
		{
			isGrounded = false;                 //Set isGrounded to true
            ResetAnimationBools ();             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool ("Jumping", true); //Transition to Correct animation
        }
	}

    //Function to flip sprite around 
	void FlipSprite()
	{
		if (FaceRight == true) 
		{
			transform.localScale = new Vector2 (1, 1); //If Right: flip to the Right = (x1, y1)
		}
		if (FaceRight == false) 
		{
			transform.localScale = new Vector2 (-1, 1); //If Left: flip to the left = (x-1, y1)
        }
	}

    //Inneficient and messy way of making sure the correct bool is active at any time
	void ResetAnimationBools()
	{
		PlayerAnimator.SetBool ("Idle", false);
        PlayerAnimator.SetBool ("Jumping", false);
        PlayerAnimator.SetBool ("Hurt", false);
        PlayerAnimator.SetBool ("Crouch", false);
        PlayerAnimator.SetBool ("Climb", false);
        PlayerAnimator.SetBool ("Moving", false);
	}
}
