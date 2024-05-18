using UnityEngine;

public class TrailAnimLeftHand : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out TrailLogic logic))
        {
            logic.ShowLeftHandTrail();
            logic.HideRightHandTrail();
        }
    }
}