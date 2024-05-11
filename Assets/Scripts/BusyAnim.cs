using UnityEngine;

public class BusyAnim : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out XBotLogic logic))
        {
            logic.SetBusy(true);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out XBotLogic logic))
        {
            logic.SetBusy(false);
        }
    }
}
