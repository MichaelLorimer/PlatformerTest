using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // ---------- Generic Projectile Script ------
    //--------------------------------------------
    //
    // -- This Script is Designed to wprk wit hthe previouse Scripts made
    // -- its primary use is for fast prototyping 
    //
    // -- Notes: 
    // ----- For Graviity change objects RigidBody2D to Dynamic
    //

    // ---- Chached objects ----
    //

    public Rigidbody2D ProjectileBod; // Store Projectile RibidBody2D
    GameObject Self;                  // Store Projectiles own GameObject
    
    // ---- Object Variables ----
    //

    public float MoveSpeed;          // How fast the Object moves (Set in Inpector)
    public float LiveFor;            // How long the object will live for (Set in Inspector)

    //-- Bools to determine shooting direction, can be combines for diagonal movement
    //

    public bool ShootLeft;           //Bool to determine if moving Left
    public bool ShootRight;          //Bool to determine if moving Right
    public bool ShootUp;             //Bool to determine if moving Up
    public bool ShootDown;           //Bool to determine if moving Down

    public bool UseTiming;           //Bool to determine if the object needs to die overtime

    // Start is called before the first frame update
    void Start()
    {
        ProjectileBod = GetComponent<Rigidbody2D>(); //Get and store the Projectiles Rigid Body
        Self = this.gameObject;                      //Get and store Projectiles Own Game Object
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootLeft == true)
        {
            Vector2 Pos = ProjectileBod.transform.position; //Define new Vectror2 to temperarily store the Objects position 
            Pos.x -= MoveSpeed * Time.deltaTime;            //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            ProjectileBod.transform.position = Pos;         //set the Objects transform to equal the temp position variable because
        }
        if (ShootRight == true)
        {
            Vector2 Pos = ProjectileBod.transform.position; //Define new Vectror2 to temperarily store the Objects position 
            Pos.x += MoveSpeed * Time.deltaTime;            //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            ProjectileBod.transform.position = Pos;         //set the players transform to equal the temp position variable because //PlayerRB.transform.position cannot be added 
        }
        if (ShootUp == true)
        {
            Vector2 Pos = ProjectileBod.transform.position; //Define new Vectror2 to temperarily store the Objects position 
            Pos.y += MoveSpeed * Time.deltaTime;       //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            ProjectileBod.transform.position = Pos;         //set the players transform to equal the temp position variable because...
                                                      //PlayerRB.transform.position cannot be added 
        }
        if (ShootDown == true)
        {
            Vector2 Pos = ProjectileBod.transform.position; //Define new Vectror2 to temperarily store the Objects position 
            Pos.y -= MoveSpeed * Time.deltaTime;            //Add the new speed to the temp variable (*Time.deltaTime = same movement regardless of FPS)
            ProjectileBod.transform.position = Pos;         //set the 10.96.16.200 transform to equal the temp position variable because
        }

        // -- Kill Object after countdown
        if (UseTiming == true)
        {
            DeathTime();
        }
    }

    void DeathTime()
    {
        if (LiveFor > 0f)
        {
            LiveFor -= Time.deltaTime;
        }
        else if (LiveFor <= 0f)
        {
            Destroy(Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(Self);
    }

}
