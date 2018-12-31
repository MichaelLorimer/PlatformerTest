using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{

    public float PointA;
    public float PointB;

    public Rigidbody2D EnemyRB;        //Store the players Rigid Body (Set in the Inspector)
    public Animator EnemyAnimator; //Store the Players Animator for transitions (Set in the Inspector)

    public float moveSpeed;             //Players move speed (Set in Inspector)
    public bool FaceRight;       //Check if Player is facing right (Change True/ False dependant on circumstance)

    // Use this for initialization
    void Start ()
    {
        EnemyRB = GetComponent<Rigidbody2D>();     //Get the Players RigidBody Component
        EnemyAnimator = GetComponent<Animator>();  //Get the Players Animator Component
    }
	
	// Update is called once per frame
	void Update ()
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
			transform.localScale = new Vector2 (-1, 1); //If Right: flip to the Right = (x1, y1)
		}
		if (FaceRight == false) 
		{
			transform.localScale = new Vector2 (1, 1); //If Left: flip to the left = (x-1, y1)
        }
	}
}
