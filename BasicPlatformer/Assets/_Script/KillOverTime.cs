using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOverTime : MonoBehaviour
{
    GameObject Self;
    public float LiveFor; //Set Howlong the objectlives in inpector

	// Use this for initialization
	void Start ()
    {
        Self = this.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (LiveFor >= 0f)
        {
            LiveFor -= Time.deltaTime;
        }

        if (LiveFor < 0)
        {
            Destroy(Self);
        }
	}
}
