using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour 
{
	public GameObject FeedbackPrefab;
	public GameObject self;
	public Rigidbody2D CherryBod;
	// Use this for initialization
	void Start () 
	{
		FeedbackPrefab = GameObject.Find("ItemFeedback");
		CherryBod = GetComponent<Rigidbody2D>();
		self = GameObject.Find("Cherry");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Vector3 CherryPos = CherryBod.position;
		//Instantiate (FeedbackPrefab, CherryPos, Quaternion.identity);
		Destroy(self);
	}
}
