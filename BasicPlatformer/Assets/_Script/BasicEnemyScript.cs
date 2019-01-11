using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{

    public float PointA;                //Left side point to move to  (Set in Inpector)
    public float PointB;                //Right Side point ot move to (Set in Inpector)

    public Rigidbody2D EnemyRB;        //Store the Enemy Rigid Body2D (Set in the Inspector)
    public Animator EnemyAnimator;     //Store the Enemy Animator for transitions (Set in the Inspector)

    public float moveSpeed;            //Enemy move speed (Set in Inspector)
    public bool FaceRight;             //Check if Enemy is facing right (Change True/ False dependant on circumstance)

    // Use this for initialization
    void Start ()
    {
        EnemyRB = GetComponent<Rigidbody2D>();     //Get the Enemy RigidBody Component
        EnemyAnimator = GetComponent<Animator>();  //Get the Enemy Animator Component
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (FaceRight == false)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the Enemy position 
            Pos.x -= moveSpeed * Time.deltaTime;      //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 
            
            if (EnemyRB.position.x <= PointA)         // If enemy reaches point A:flip the sprite
            {
                FaceRight = true;
            }
        }

        if (FaceRight == true)
        {
            Vector2 Pos = EnemyRB.transform.position; //Define new Vectror2 to temperarily store the Enemy position 
            Pos.x += moveSpeed * Time.deltaTime;      //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            EnemyRB.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 

            if (EnemyRB.position.x >= PointB)         // If enemy reached point B:flip the sprite
            {
                FaceRight = false;
            }
        }

        FlipSprite(); //Call flip sprite Method to see if sprite needs flipping
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
