using UnityEngine;

public class TrailAnimRightHand : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out TrailLogic logic))
        {
            logic.ShowRightHandTrail();
            logic.HideLeftHandTrail();
        }
    }
}