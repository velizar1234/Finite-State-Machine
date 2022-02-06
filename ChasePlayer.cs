using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scenes.code.StateMachine.States;

namespace Assets.Scenes.code.NPCCode
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "UnityFSM/States/Chase", order = 3)]
    public class ChasePlayer : AbstractFSMClass
    {
        public override void OnEnable()
        {
            //reference for the state
            base.OnEnable();
            StateType = FSMStateType.CHASE;
            
            
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            //uses the setDestination function to chase the player
            if (EnteredState)
            {
                Debug.Log("chasing");
                SetDestination(player);

                Debug.Log("chase completed");
            }
            
            return EnteredState;
        }

        public override void UpdateState()
        {
            //checks the distance between NPC and player and executes the chase function

            if(Vector3.Distance(_npc.transform.position, player.transform.position) <=10f)
            {
                
                SetDestination(player);
            }
            else
            {
                //goes back to patrol state
                Debug.Log("player out of range");
                _fsm.EnterState(FSMStateType.PATROL);
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("exiting chase state");
            return true;
        }

        private void SetDestination(GameObject destination)
        {
            
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
