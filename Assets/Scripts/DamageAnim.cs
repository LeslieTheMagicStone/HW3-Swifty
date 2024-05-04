using UnityEngine;

public class DamageAnim : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out PlayerDamageLogic damageLogic))
        {
            damageLogic.HideLeftHand();
            damageLogic.HideRightHand();
        }
    }
}
