using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericJumpingEnemy : MonoBehaviour
{
    public float PointA;
    public float PointB;

    public Rigidbody2D EnemyRB;        //Store the Enemy Rigid Body (Set in the Inspector)
    public Animator EnemyAnimator;     //Store the Players Animator for transitions (Set in the Inspector)

    public float moveSpeed;            //Enemy move speed (Set in Inspector)
    public float JumpHeight;           //Enemy JumpHeight (Set in Inspector)
    public float MaxHeight;            //Enemy MaxJumpHeight (Set in Inspector)
    public bool FaceRight;             //Check if Enemy is facing right (Change True/ False dependant on circumstance)
    public bool IsGrounded;            //Check if the Enemy is grounded
    public bool CanJump;               //Check if the Enemy is able to jump

    public float WaitFor = 1f;         //Time to wait inbetween jumps

    // Use this for initialization
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();     //Get the Enemy RigidBody Component
        EnemyAnimator = GetComponent<Animator>();  //Get the Enemy Animator Component
        IsGrounded = false;                        //grounded = false by default
    }

    // Update is called once per frame
    void Update()
    {

        if (FaceRight == false)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the Enemy position 
            Pos.x -= moveSpeed * Time.deltaTime;      //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 

            if (EnemyRB.position.x <= PointA)         // If enemy reached point A:flip the sprite
            {
                FaceRight = true;
            }
        }
        if (FaceRight == true)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the enemy position 
            Pos.x += moveSpeed * Time.deltaTime;      //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added
                                                      
            if (EnemyRB.position.x >= PointB)         // If enemy reached point B:flip the sprite
            {
                FaceRight = false;
            }
        }

        if(IsGrounded == true)
        {
            float SpeedStore = moveSpeed;              //Store current speed before editing
            CanJump = true;                            //Redundant?

            moveSpeed = 0;                             //Set speed to 0 to stop the Enemy movement entirely
            float Countdown = WaitFor;
            Countdown -= Time.deltaTime;                 //Start countdown time

            if (Countdown <= 0f)
            {
                moveSpeed = SpeedStore;                //When countdown == 0, Reset movespeed
                IsGrounded = false;                    //Grounded is false to transition to jump
            }
        }

        if (IsGrounded == false)
        {
            if (CanJump == true)
            {
                EnemyRB.AddForce(new Vector2(0, JumpHeight), ForceMode2D.Impulse);
                CanJump = false;
            }
        }



        FlipSprite();
    }

    void FlipSprite()
    {
        if (FaceRight == true)
        {
            transform.localScale = new Vector2(-1, 1); //If Right: flip to the Right = (x1, y1)
        }
        if (FaceRight == false)
        {
            transform.localScale = new Vector2(1, 1); //If Left: flip to the left = (x-1, y1)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IsGrounded = true;
        }
    }
}
