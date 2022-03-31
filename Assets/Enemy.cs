using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FSM {
    // Start is called before the first frame update
    public float maxHP;
    float currentHP;
    public float minThinkTimeRange;
    public float maxThinkTimeRange;
    public List<Transform> movePoints;
    public float walkSpeed;
    public float runSpeed;
    public float sightRange;
    public GameObject player;
    int lastMovePointIndex;
    public float attackRange;
   void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHP = maxHP;
    }

    private void Update()
    {
        if (currentHP <= 0 && currentState != State.DEAD)
        {
            SetState(State.DEAD);

        }
        else {
         float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= sightRange)
        {
            if (currentState != State.CHASE && currentState != State.ATTACK)
            {
                SetState(State.CHASE);
            }
        }
    }
    }
    protected override IEnumerator IDLE()
    {
        Debug.Log(gameObject + "." + currentState.ToString() + ".Start");
        float thinkTime = Random.Range(minThinkTimeRange, maxThinkTimeRange);

        float updateTime=0f;


        while (isNewState == false)
        {
            updateTime += Time.deltaTime;
            if(updateTime >= thinkTime)
            {
                SetState(State.WALK);
            }
            yield return null;
        }
    }
    protected override IEnumerator WALK()
    {
        Debug.Log(gameObject + "." + currentState.ToString() + ".Start");
        //이전에 움직인 이동 포인트를 달리하려고 만든 거
        int index = 0;
        do
        {
            index = Random.Range(0, movePoints.Count);
            yield return null;
            
        } while (lastMovePointIndex == index);
        
        lastMovePointIndex = index;
        
        //int index = Random.Range(0, movePoints.Count);
        
        Transform movePoint = movePoints[index];
        while(isNewState == false)
        {
            Vector3 dest = new Vector3(movePoint.position.x, transform.position.y, movePoint.position.z);
            transform.position = Vector3.MoveTowards(transform.position, dest, walkSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, dest) <= 0.1f)
            {
                SetState(State.IDLE);
            }
            yield return null;
        }
    }
    protected override IEnumerator CHASE()
    {
        Debug.Log(gameObject + "." + currentState.ToString() + ".Start");
        while (!isNewState)
        {
            Vector3 dest = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, dest, walkSpeed *2* Time.deltaTime);

            if (Vector3.Distance(transform.position, dest) <= attackRange)
            {
                SetState(State.ATTACK);
            }

            yield return null;
        }
    }
    protected override IEnumerator DEAD()
    {
        yield return null;

    }
    protected override IEnumerator ATTACK()
    {
        Debug.Log(gameObject + "." + currentState.ToString() + ".Start");

        while (!isNewState)
        {
            Vector3 dest = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            
            if(Vector3.Distance(transform.position, dest) > attackRange){
                SetState(State.CHASE);
            }
            yield return null;
        }
    }

}
