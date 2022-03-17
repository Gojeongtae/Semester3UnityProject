using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : FSM
{
    Transform target;
    float enemyMoveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 100f, 1 << 8);

        if (cols.Length > 0)
        {

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Player")
                {
                    Debug.Log("Physics Enemy : Target found");
                    target = cols[i].gameObject.transform;
                }
            }
        }
        else
        {
            Debug.Log("Physics Enemy : Target lost");
            target = null;
        }
    }

    // Update is called once per frame
        void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyMoveSpeed * Time.deltaTime);
        }

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
