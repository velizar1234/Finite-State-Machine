using System;
using System.Collections.Generic;
using Assets.Scenes.code.StateMachine.States;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scenes.code.NPCCode 
{
    [RequireComponent(typeof(NavMeshAgent),typeof(FiniteStateMachine_2))]
    public class NPC: MonoBehaviour
    {
        [SerializeField]
        NPCPatrolPoint[] _patrolPoints;
        


        NavMeshAgent navMeshAgent;
        FiniteStateMachine_2 finiteStateMachine;

       

        public void Awake()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            finiteStateMachine = this.GetComponent<FiniteStateMachine_2>();
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
           
        }

       public NPCPatrolPoint[] PatrolPoints
        {
            get
            {
                return _patrolPoints;
            }
        }

        
    }
}
