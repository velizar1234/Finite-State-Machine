using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scenes.code.StateMachine.States
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="UnityFSM/States/Idle",order =1)]
    public class IdleState : AbstractFSMClass
    {
        [SerializeField]
        float _idleDuration = 3f;

        float _totalDuration;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            //resets timer
            if (EnteredState)
            {
                Debug.Log("entered idle state");
                _totalDuration = 0f;
            }

            return EnteredState;
        }

        public override void UpdateState()
        {
            //checks if player is nearby and enters chase state if yes
            if (Vector3.Distance(_npc.transform.position, player.transform.position) <= 10f)
            {
                _fsm.EnterState(FSMStateType.CHASE);
            }
            else
            {

            }
            if (EnteredState)
            {
                //starts timer and if it reaches 3s, enters patrol state
                _totalDuration += Time.deltaTime;

                Debug.Log("updating idle state" + _totalDuration + "seconds");

                
                if(_totalDuration >= _idleDuration)
                {
                    _fsm.EnterState(FSMStateType.PATROL);
                }
                
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("exiting idle state");
            return true;
        }
    }
}
