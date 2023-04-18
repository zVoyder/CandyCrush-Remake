using UnityEngine;

public class DestroyOnEnterAnimation : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.transform.root.gameObject);
    }
}