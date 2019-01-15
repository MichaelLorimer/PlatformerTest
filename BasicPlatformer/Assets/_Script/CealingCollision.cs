using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CealingCollision : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GenericPlayerMove.Crouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GenericPlayerMove.ResetAnimationBools();
            GenericPlayerMove.Crouching = true;
        }
    }
}
