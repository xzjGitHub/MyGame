using FSM;
using UnityEngine;

public class TestCubeMove : MonoBehaviour
{
    public float speed=1;

    private FSMStateMachine fsm;   //状态机
    private FSMState idle;    //闲置状态
    private FSMState move;    //移动状态
    private FSMTransition idleMove;    //从 Idle 到 Move 状态的过渡
    private FSMTransition moveIdle;    //从 Move 到 Idle 状态的过渡
    private bool isMove;

    void Start()
    {
        //初始状态
        idle =new FSMState("Idle");
        idle.OnEnter += Idle_OnEnter;
        //
        move=new FSMState("Move");
        move.OnUpdate += Move_OnUpdate;
        //
        idleMove=new FSMTransition("IdleMove",idle,move);
        idleMove.OnCheck += IdleMove_OnCheck;
        idle.AddTransition(idleMove);
        //
        moveIdle = new FSMTransition("MoveIdle", move, idle);
        moveIdle.OnCheck += MoveIdle_OnCheck;
        move.AddTransition(moveIdle);
        //
        fsm=new FSMStateMachine("Root",idle);
        fsm.AddState(move);
    }

    private bool MoveIdle_OnCheck(object param)
    {
        return !isMove;
    }

    private bool IdleMove_OnCheck(object param)
    {
        return isMove;
    }

    private void Move_OnUpdate(float f,object param)
    {
        transform.position += transform.forward*f*speed;
    }

    private void Idle_OnEnter(IFSMState state)
    {
       LogHelperLSK.Log("进入 Idle 状态");
    }

    void Update()
    {
        fsm.UpdateCallBack(Time.deltaTime);
    }

    void OnGUI()
    {
        if (GUILayout.Button("Move"))
        {
            isMove = true;
        }
        if (GUILayout.Button("Stop"))
        {
            isMove = false;
        }
    }
}
