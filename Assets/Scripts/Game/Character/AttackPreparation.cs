using UnityEngine;

namespace Assets.Scripts.Game.Character
{
    internal class AttackPreparation : StateMachineBehaviour
    {
        private Ai ai;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            ai ??= animator.gameObject.GetComponent<Ai>();

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            ai?.AttackAfterDelay();
    }
}