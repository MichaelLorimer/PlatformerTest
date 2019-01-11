using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour 
{
	public GameObject FeedbackPrefab;          //Store the prefab of the ItemFeedback effect
	public GameObject self;                    //Store own GameObject for destruction
	public Rigidbody2D ItemRB;                 //Store own RigidBody2D

    public static bool test = false;
	// Use this for initialization
	void Start () 
	{
        ItemRB = GetComponent<Rigidbody2D>(); //Cache RigidBody2D
        self = this.gameObject;               //Cache own GameObject
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Vector3 CurrentPos = ItemRB.position;                        //Get Current posiiton to spawn the feedback effect
        Instantiate(FeedbackPrefab, CurrentPos, Quaternion.identity);//Spawn the feedback effect
		Destroy(self); //Destroy own GameObject
	}
}
