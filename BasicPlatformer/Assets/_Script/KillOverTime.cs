using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOverTime : MonoBehaviour
{
    GameObject Self;      //Store Own GameObject
    public float LiveFor; //Set How long the object lives (in inpector)

	// Use this for initialization
	void Start ()
    {
        Self = this.gameObject; //Cache own gameobject
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LiveFor >= 0f)
        {
            LiveFor -= Time.deltaTime; //Start timer until death
        }

        if (LiveFor < 0)
        {
            Destroy(Self);             //Destroy own game object
        }
	}
}
