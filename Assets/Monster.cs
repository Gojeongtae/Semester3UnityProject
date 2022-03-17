using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : FSM
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetState(State.CHASE);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SetState(State.ATTACK);
        }
    }

    protected override IEnumerator IDLE()
    {
        
        Debug.Log("Player" + currentState + "ING");
        new WaitForSeconds(3f);
        Debug.Log("Player" + currentState + "Done");
        SetState(State.WALK);
        yield return null; 
    }
    protected override IEnumerator WALK()
    {
        while (isNewState == false)
        {
            Debug.Log("Player" + currentState + "ING");
        }
        yield return null;
    }
    protected override IEnumerator CHASE()
    {
        Debug.Log("Player" + currentState + "ING");
        yield return new WaitForSeconds(3f);
        Debug.Log("Player" + currentState + "Done");
        SetState(State.WALK);
        yield return null;
    }
    protected override IEnumerator ATTACK()
    {
        while (isNewState == false)
        {
            Debug.Log("Player" + currentState + "ING");
        }
        SetState(State.WALK);
        yield return null;
    }
    protected override IEnumerator DEAD()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Player" + currentState + "Done");
        Destroy(gameObject);
        yield return null;
    }
}
