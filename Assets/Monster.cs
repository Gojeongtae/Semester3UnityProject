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
        if (currentState == State.WALK && Input.GetMouseButtonDown(0))
        {
            isNewState = true;
            SetState(State.CHASE);
        }
        else if (currentState == State.CHASE && Input.GetMouseButtonDown(1))
        {
            isNewState = true;
            SetState(State.ATTACK);
        }
    }

    protected override IEnumerator IDLE()
    {
        while(isNewState == false)
        {
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(7f);
            Debug.Log("Player" + currentState + "Done");
            SetState(State.WALK);
            yield return null;
        }
       
       
    }
    protected override IEnumerator WALK()
    {
        float timer = 0f;

        while (isNewState == false)
        {
            timer += Time.deltaTime;

            if(timer >= 7f)
            {
                Debug.Log("Player" + currentState + "DONE");
                SetState(State.IDLE);
            }
            yield return null;
        }
    }
    protected override IEnumerator CHASE()
    {
        Debug.Log("Player" + currentState + "Start");
        float timer = 0f;

        while (isNewState == false)
        {
            timer += Time.deltaTime;

            if (timer >= 7f)
            {
                Debug.Log("Player" + currentState + "DONE");
                SetState(State.WALK);
            }
            yield return null;
        }
    }
    protected override IEnumerator ATTACK()
    {
        while (isNewState == false)
        {
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "ING");
            yield return new WaitForSeconds(1f);
            Debug.Log("Player" + currentState + "Done");
            SetState(State.DEAD);
            yield return null;
        }

    }
    protected override IEnumerator DEAD()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Player" + currentState + "Done");
        Destroy(gameObject);
        yield return null;
    }
}
