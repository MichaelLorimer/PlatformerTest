using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    //====|    Noted for use    |====
    //===============================
    //
    // -- Attach this script to an empty gameObject that is a child of the player
    // -- Make sure that GameObject has a collider on the bottom of the player 
    // -- I found that having a box collider on the Player and Capsule collider on
    // -- The empty game object works okay provided the Capsule collider is smaller tha nthe box
    public Animator PlayerAnimator;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GenericPlayerMove.isGrounded = true;

            GenericPlayerMove.ResetAnimationBools();
            PlayerAnimator.SetBool("Idle", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GenericPlayerMove.isGrounded = false;
            GenericPlayerMove.ResetAnimationBools();
        }
    }
}
