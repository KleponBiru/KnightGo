using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;

    void Update()
    {
        if(playerMovement.canDash == true){
            animator.SetBool("canDash", true);
        }
        else{
            animator.SetBool("canDash", false);
        }
    }
}
