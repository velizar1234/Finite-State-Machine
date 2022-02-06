using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scenes.code.NPCCode;

namespace Assets.Scenes.code.NPCCode
{
    public class FiniteStateMachine_2: MonoBehaviour
    {
        [SerializeField]
        AbstractFSMClass _startingState;
        AbstractFSMClass currentState;

        [SerializeField]
        List<AbstractFSMClass> _validStates;

        

        Dictionary<FSMStateType, AbstractFSMClass> _fsmStates;

        public void Awake()
        {
            //asssigns values to the variables by getting the nessassary components

            currentState = null;

            _fsmStates = new Dictionary<FSMStateType, AbstractFSMClass>();

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
            NPC npc = this.GetComponent<NPC>();

            foreach(AbstractFSMClass state in _validStates)
            {
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(npc);
                state.SetNavMeshAgent(navMeshAgent);
                _fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            if(_startingState!=null)
            {
                EnterState(_startingState);
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }

        #region STATE MANAGEMENT

        public void EnterState(AbstractFSMClass nextState)
        {
            if (nextState == null)
            {
                return;
            }

            

            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = nextState;
            currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            //checks if the state contains the dictionary key, if yes, transitions to next state
            if(_fsmStates.ContainsKey(stateType))
            {
                AbstractFSMClass nextState = _fsmStates[stateType];


                EnterState(nextState);
            }
        }

        #endregion
    }
}

