using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;

    public void Run()
    {
        ResetAllTriggers();
        animator.SetTrigger("run");
    }

    public void Jump()
    {
        ResetAllTriggers();
        animator.SetTrigger("jump");
    }

    public void Finish()
    {
        ResetAllTriggers();
        animator.SetTrigger("finish");
    }

    public void RunAndCarryWood()
    {
        ResetAllTriggers();
        animator.SetTrigger("carry");
    }

    public void RunAndMakeShortcut()
    {
        ResetAllTriggers();
        animator.SetTrigger("shortcut");
    }

    public void Stop()
    {
        animator.enabled = false;
    }

    void ResetAllTriggers()
    {
        foreach (AnimatorControllerParameter p in animator.parameters)
            if (p.type == AnimatorControllerParameterType.Trigger)
                animator.ResetTrigger(p.name);
    }
}
