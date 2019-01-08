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
	public Collider2D  PlayerGroundCol;   	//Store the players BoxCollider2D (Set in the Inspector) ---------- NOT SURE IF NEEDED ----------
	public Animator    PlayerAnimator;	//Store the Players Animator for transitions (Set in the Inspector)

	//-- Player Variables --
    // - Stored Player Specific variables EG: Health, Speed, 
    //

	public float moveSpeed;			    //Players move speed (Set in Inspector)
	public float JumpHeight;		    //Players JumpHeight (Set in Inspector)
    public float TempGravityScale;      //Players falling gtravity scale (set in inpector)
    public float MaxJumpHeight;
    private float SpeedStore;

    public Transform GroundCheck;
    public Transform GroundCheck2;
    public Transform GroundCheck3;
    public Transform GroundCheck4;
    public Transform GroundCheck5;

    //-- Player Bools --
    // - Store Player Specific bools EG: Facing status, has jumped, current state, is living
    //

    public bool FaceRight = true;       //Check if Player is facing right (Change True/ False dependant on circumstance)
	public bool isGrounded;             //Check if the Player is grounded to prevent infinate jumping 
    public bool Falling;

    //Initiasise variables on startup of application
	void Start () 
	{
        //Cache Components 
        //

		PlayerRB = GetComponent<Rigidbody2D> ();     //Get the Players RigidBody Component
		PlayerAnimator = GetComponent<Animator> ();  //Get the Players Animator Component
        SpeedStore = moveSpeed;
        // Initialised variables
        //

        isGrounded = false;                          //Initialised to false because the player in this build starts in the air 
	}

    //Fixed update should be used for physics, it has its own update loop/ time? -------------- Fill out more -------------------------
    void FixedUpdate()
    {
        // Bit Shift index of layer (8) to get a bit mask 
        int layerMask = 1 << 10;

        //this would cast rays only against collsion in layer 8
        // BUT instead we want t ocollide against everything but the player layer 
        layerMask = ~layerMask; // '~' inverts the bit mask

        RaycastHit2D hit;
        // if ray hits anything that sin the player layer  


        // Make this araycast array later on
       // isGrounded = Physics2D.Linecast(PlayerRB.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //isGrounded = Physics2D.Linecast(PlayerRB.position, GroundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        //isGrounded = Physics2D.Linecast(PlayerRB.position, GroundCheck3.position, 1 << LayerMask.NameToLayer("Ground"));
        //isGrounded = Physics2D.Linecast(PlayerRB.position, GroundCheck4.position, 1 << LayerMask.NameToLayer("Ground"));
        //isGrounded = Physics2D.Linecast(PlayerRB.position, GroundCheck5.position, 1 << LayerMask.NameToLayer("Ground"));


        //if first cast is false, check other casts if all false, not grounded
        RaycastHit2D[] castArray = new RaycastHit2D[5];
        RaycastHit2D[] f = Physics2D.RaycastAll(PlayerRB.position, -transform.up, 50f, 1 << LayerMask.NameToLayer("Ground"));
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i].transform != transform && f[i].collider.tag != "Ground")
            {
                castArray[0] = f[i];
                isGrounded = true;
                Debug.Log("Collsion on: " + castArray[i]);
            }
        }


        if (isGrounded == true)
        {
            ResetAnimationBools();
            PlayerAnimator.SetBool("Idle", true);
            Falling = false;
        }
        else if (isGrounded == false)
        {
            ResetAnimationBools();
            PlayerAnimator.SetBool("Jumping", true);
        }


        // -- Jump
        //Check if the player is grounded to enable jumps
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            PlayerRB.AddForce(new Vector2(0f, JumpHeight), ForceMode2D.Impulse);
            isGrounded = false;
        }
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

        // Add Momentum to movements plox

        // -- Crouch 
		if (Input.GetKey (KeyCode.S)) 
		{
            moveSpeed = 0f;                           //While crouching movement is stopped = 0;
			ResetAnimationBools ();                   //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool ("Crouch", true);        //Transition to Correct animation
        } 
		else if (Input.GetKeyUp (KeyCode.S))         // check for then the "S" key is released to re enable movement
		{
			moveSpeed = SpeedStore;                          //Move speed = 3 (Should be set to a variable to preserve data)
			ResetAnimationBools ();                  //Reset the animation bools for the next transition  
            PlayerAnimator.SetBool ("Idle", true);         //Transition to Correct animation
        }

        

        //Check if the player is not grounded to enable Animation
        else if (isGrounded == false)
        {
            ResetAnimationBools();                             //Reset the animation bools for the next transition
            PlayerAnimator.SetBool("Jumping", true);                 //Transition to Correct animation
            if (PlayerRB.velocity.y > MaxJumpHeight)
            {
                Falling = true;
            }
        }

        if (Falling == true)
        {
            float UpVelocity = PlayerRB.velocity.y;
            UpVelocity -= (TempGravityScale * Time.deltaTime);
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, UpVelocity);
        }
        //To Check if the sprite needs to be flipped
        FlipSprite(); 
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
