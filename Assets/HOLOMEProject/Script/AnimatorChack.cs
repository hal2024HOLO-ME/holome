using UnityEngine;
using System.Collections;

using Const;


public class AnimatorChack : MonoBehaviour
{
    public GameObject character;

    private Animator animator;

    void Awake()
    {
        Debug.Log(character);
        animator = character.GetComponent<Animator>();
    }


    public void OnClickHappy()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_HAPPY);
    }

    public void OnClickWalk()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_WALK);
    }

    public void OnClickSleep()
    {
        StartCoroutine(boolAnimator(CO.ANIMATOR_BOOL_SLEEP));
    }

    public void OnClickDead()
    {
        StartCoroutine(boolAnimator(CO.ANIMATOR_BOOL_DEAD));
    }

    public void OnClickEat()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_EAT);
    }

    public void OnClickFloat()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_FLOAT);
    }

    public void OnClickRotate()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_ROTATE);
    }

    public void OnClickSit()
    {
        animator.SetTrigger(CO.ANIMATOR_TRIGGER_SIT);
    }

    private IEnumerator boolAnimator(string animatorName)
    {
        animator.SetBool(animatorName, true);
        yield return new WaitForSeconds(15f);
        animator.SetBool(animatorName, false);
    }
}
