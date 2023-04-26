using UnityEngine;

public class DestroyOnEnterAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.transform.root.gameObject);
    }
}