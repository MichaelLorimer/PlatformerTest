using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalkEnemy : MonoBehaviour
{

    // notes --
    // ray cast in 4 directions t odetermine if grounded
    //
    // Start is called before the first frame update

    Rigidbody2D EnemyRB; //Store objects rigidbody2D
    public bool isGrounded;
    public float raycastDistance = 0;

    // movement bools 
    bool MoveLeft;
    bool MoveRight;
    bool MoveUp;
    bool MoveDown;

    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Cast ray straight down
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance);

        if (hitDown.collider != null)
        {
            ResetBools();
            MoveLeft = true;
        }

        if (hitRight.collider != null)
        {
            ResetBools();
            MoveDown = true;
        }


        








        if (MoveLeft == true)
        {
            Vector2 Pos = EnemyRB.transform.position; 
            Pos.x -= 3 * Time.deltaTime;
            EnemyRB.transform.position = Pos;
        }

        if (MoveRight == true)
        {
            Vector2 Pos = EnemyRB.transform.position;
            Pos.x += 3 * Time.deltaTime;
            EnemyRB.transform.position = Pos;
        }

        if (MoveUp == true)
        {
            Vector2 Pos = EnemyRB.transform.position;
            Pos.y += 3 * Time.deltaTime;
            EnemyRB.transform.position = Pos;
        }

        if (MoveDown == true)
        {
            Vector2 Pos = EnemyRB.transform.position;
            Pos.y -= 3 * Time.deltaTime;
            EnemyRB.transform.position = Pos;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBools()
    {
        MoveDown = false;
        MoveUp = false;
        MoveRight = false;
        MoveLeft = false;
    }
}
