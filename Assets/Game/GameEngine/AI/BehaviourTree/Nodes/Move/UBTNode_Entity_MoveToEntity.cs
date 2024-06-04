using AI.Blackboards;
using AI.BTree;
using Elementary;
using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Game.GameEngine.AI
{
    [AddComponentMenu(BehaviourTreePaths.MENU_PATH + "BTNode «Move To Entity» (Entity)")]
    public sealed class UBTNode_Entity_MoveToEntity : UnityBehaviourNode, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }

        [Space]
        [SerializeField]
        private FloatAdapter stoppingDistance; //0.15f;

        [Space]
        [BlackboardKey]
        [SerializeField]
        private string unitKey;

        [BlackboardKey]
        [SerializeField]
        private string targetKey;
        
        private Agent_Entity_MoveToPosition moveAgent;

        private void Awake()
        {
            this.moveAgent = new Agent_Entity_MoveToPosition();
            this.moveAgent.SetStoppingDistance(this.stoppingDistance.Current);
        }

        protected override void Run()
        {
            if (!this.Blackboard.TryGetVariable(this.unitKey, out IEntity myUnit))
            {
                this.Return(false);
                return;
            }

            if (!this.Blackboard.TryGetVariable(this.targetKey, out IEntity targetUnit))
            {
                this.Return(false);
                return;
            }

            this.moveAgent.OnTargetReached += this.OnTargetReached;
            this.moveAgent.SetMovingEntity(myUnit);

            var targetPosition = targetUnit.Get<IComponent_GetPosition>().Position;
            this.moveAgent.SetTarget(targetPosition);
            this.moveAgent.Play();
        }

        private void OnTargetReached(bool isReached)
        {
            if (isReached)
            {
                this.Return(true);
            }
        }

        protected override void OnDispose()
        {
            this.moveAgent.OnTargetReached -= this.OnTargetReached;
            this.moveAgent.Stop();
        }
    }
}