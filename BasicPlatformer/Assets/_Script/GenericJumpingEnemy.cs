using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericJumpingEnemy : MonoBehaviour
{
    public float PointA;
    public float PointB;

    public Rigidbody2D EnemyRB;        //Store the players Rigid Body (Set in the Inspector)
    public Animator EnemyAnimator; //Store the Players Animator for transitions (Set in the Inspector)

    public float moveSpeed;             //Players move speed (Set in Inspector)
    public float JumpHeight;
    public bool FaceRight;       //Check if Player is facing right (Change True/ False dependant on circumstance)
    public bool IsGrounded;
    public bool CanJump;

    public float WaitFor = 1f;

    // Use this for initialization
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();     //Get the Players RigidBody Component
        EnemyAnimator = GetComponent<Animator>();  //Get the Players Animator Component
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (FaceRight == false)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 
            if (EnemyRB.position.x <= PointA)
            {
                FaceRight = true;
            }
        }
        if (FaceRight == true)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x += moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 
            if (EnemyRB.position.x >= PointB)
            {
                FaceRight = false;
            }
        }

        if(IsGrounded == true)
        {
            float SpeedStore = moveSpeed;
            CanJump = true;

            moveSpeed = 0;
            WaitFor -= Time.deltaTime;

            if (WaitFor <= 0f)
            {
                moveSpeed = 3;
                WaitFor = 1f;
                IsGrounded = false;

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


        /*if (FaceRight == true)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            FaceRight = true;                         //Set RaceRight to false to flip the sprite 
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added 
        }
        if (FaceRight == false)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the players position 
            Pos.x -= moveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            FaceRight = false;                         //Set RaceRight to false to flip the sprite 
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                       //PlayerRB.transform.position cannot be added 
        }*/
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
