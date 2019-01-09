using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public Animator PlayerAnimator;

    public bool test;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GenericPlayerMove.isGrounded = true;
            test = true;

            GenericPlayerMove.ResetAnimationBools();
            PlayerAnimator.SetBool("Idle", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            test = false;
            GenericPlayerMove.isGrounded = false;
            GenericPlayerMove.ResetAnimationBools();
            PlayerAnimator.SetBool("Jumping", true);
        }
    }
}
