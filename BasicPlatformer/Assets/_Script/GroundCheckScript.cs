using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GenericPlayerMove.isGrounded = true;
        }
        else
        {
            GenericPlayerMove.isGrounded = false;
        }
    }
}
