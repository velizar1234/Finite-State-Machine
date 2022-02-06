using Assets.Scenes.code.StateMachine.States;
using Assets.Scenes.code.NPCCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//execution process
public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

//states
public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE
};

public abstract class AbstractFSMClass : ScriptableObject
{
    protected NavMeshAgent _navMeshAgent;
    protected NPC _npc;
    protected FiniteStateMachine_2 _fsm;
    protected GameObject player;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public virtual bool EnterState()
    {
        //safety checks
        bool successNavMesh = true;
        bool successNPC = true;

        ExecutionState = ExecutionState.ACTIVE;

        //does the navMeshAgent exists?
        successNavMesh = (_navMeshAgent != null);

        //does the executing agent exist?
        successNPC = (_npc != null);


        return successNavMesh & successNPC;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    //assigning values to the main variables
    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent != null)
        {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine_2 fsm)
    {
        if(fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC npc)
    {
        if(npc != null)
        {
            _npc = npc;
        }
    }
}
