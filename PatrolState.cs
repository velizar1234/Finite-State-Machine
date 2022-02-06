using System;
using Assets.Scenes.code.StateMachine.States;
using Assets.Scenes.code.NPCCode;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scenes.code.NPCCode
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "UnityFSM/States/Patrol", order = 2)]
    public class PatrolState : AbstractFSMClass
    {
        NPCPatrolPoint[] _patrolPoints;
        int patrolPointIndex;

        public override void OnEnable()
        {
            
            base.OnEnable();
            StateType = FSMStateType.PATROL;
            patrolPointIndex = -1;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                //grab and store patrol points
                _patrolPoints = _npc.PatrolPoints;

                if (_patrolPoints == null || _patrolPoints.Length == 0)
                {
                    Debug.LogError("PatrolState: failed to grab patrol points");
                }
                else
                {
                    //generates a random number to determine where to go
                    if (patrolPointIndex < 0)
                    {
                        patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                    }
                    else
                    {
                        patrolPointIndex = (patrolPointIndex + 1) % _patrolPoints.Length;
                    }

                    SetDestination(_patrolPoints[patrolPointIndex]);
                    EnteredState = true;
                }
            }
            return EnteredState;
           
        }

        public override void UpdateState()
        {
            //checks if the player is close and enters chasing state if yes
            if (Vector3.Distance(_npc.transform.position, player.transform.position) <= 10f)
            {
                
                _fsm.EnterState(FSMStateType.CHASE);
                Debug.Log("entering chasing state");
            }
            else
            {

            }
            //need to make sure we've successfully entered the state
            if (EnteredState)
            {
               //enters idle state if patrol point is reached successfully
                if (Vector3.Distance(_navMeshAgent.transform.position,_patrolPoints[patrolPointIndex].transform.position) <=2f)
                {
                    _fsm.EnterState(FSMStateType.IDLE);
                }
            }
        }


        //movement function
        private void SetDestination(NPCPatrolPoint destination)
        {
            if(_navMeshAgent != null && destination != null)
            {
                _navMeshAgent.SetDestination(destination.transform.position);
            }
        }
    }
}
