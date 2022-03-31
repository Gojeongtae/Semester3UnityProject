using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    IDLE = 0,
    WALK = 1,
    CHASE = 2,
    ATTACK = 3,
    DEAD = 4,
}
public class FSM : MonoBehaviour
{
    public State currentState = State.IDLE;
    protected bool isNewState = false;//새로운 상태로 바뀌는 중인지 아닌지? 파악하는 변수
    //Animator animator;
    // Start is called before the first frame update
    protected void Start()
    {
        //animator = GetComponent<Animator>();
        StartCoroutine(FsmMain());

    }

    private IEnumerator FsmMain()
    {
        Debug.Log("FSM.FsmMain.Start");

        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(currentState.ToString());
        }
    }

    protected virtual IEnumerator IDLE()
    {
        yield return null;
    }

    protected virtual IEnumerator WALK()
    {
        yield return null;
    }
    protected virtual IEnumerator ATTACK()
    {
        yield return null;
    }
    protected virtual IEnumerator CHASE()
    {
        yield return null;
    }
    protected virtual IEnumerator DEAD()
    {
        yield return null;
    }

    public void SetState(State newState)
    {
        isNewState = true;
        currentState = newState;
        //animator.SetInteger("state",(int)currentState);
    }

}
