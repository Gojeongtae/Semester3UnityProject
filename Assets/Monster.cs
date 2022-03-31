using System.Collections;
using UnityEngine;

public class Monster : FSM
{
    public float idletime = 10f;
    Animator animator;
    //IDLE / WALK / CHASE / ATTACK / DEAD

    //IDLE일 경우에 특정 시간이 지나면 WALK로 간다

    //WALK일경우엔 특정시간이 지나면 IDLE로 간다
    //WALK일 경우에 클릭하면 플레이어를 쫓는 CHASE 상태로 간다

    //CHASE일 경우엔 특정 시간이 지나면 WALK로 간다
    //CHASE일 경우엔 클릭하면 플레이어를 공격하는 ATTACK 상태로 간다
    //ATTACK일 경우엔 1초마다 공격했다 로그를 찍는다
    //ATTACK일 경우엔 특정시간이 지나면 DEAD로 간다

    //DEAD일 경우엔 특정 시간이 지나면 게임 오브젝트를 삭제 시킨다.
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
    private void Update()
    {

    }
    protected override IEnumerator IDLE()
    {
        float timer = 10f;
        while (isNewState == false)
        {
            timer -= Time.deltaTime;
            if (timer == 0)
            {
                //특정 지점까지 이동
                //완료되면 IDLE로 전환
                yield return null;
            }
        }

    }
    protected override IEnumerator WALK()
    {
        float timer = 10f;
        if (Input.GetMouseButtonDown(0))
        {
            SetState(State.CHASE);
        }
        else
        {
            while (isNewState == false)
            {
                timer-=Time.deltaTime;
                if (timer <= 0f)
                {
                    SetState(State.IDLE);
                }
                //특정 지점까지 이동
                //완료되면 IDLE로 전환
                
                yield return null;
            }

        }

    }
    protected override IEnumerator CHASE()
    {
        float timer = 10f;
        while (isNewState == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                SetState(State.WALK);
            }
            //특정 지점까지 이동
            //완료되면 IDLE로 전환
            if (Input.GetMouseButtonDown(0))
            {
                SetState(State.ATTACK);
            }
            yield return null;
        }

    }
    protected override IEnumerator ATTACK()
    {
        bool hit = false;
        float delay = 10f, timer = 20f;
        
        while(isNewState = false)
        {
            hit = true;
            delay -= Time.deltaTime;
            timer -= Time.deltaTime;
            if (delay<=5f)
            {
                Debug.Log("Attack!");
                delay = 10f;
            }
            if(timer == 0f)
            {
                SetState(State.DEAD);
            }
            yield return null;
        }

    }
    protected override IEnumerator DEAD()
    {
        float timer = 15f;
        while (true)
        {
            timer -= Time.deltaTime;
            if(timer == 0f)
                Destroy(this);
        }

    }
    public void SetState(State newState)
    {
        isNewState = true;
        currentState = newState;
        animator.SetInteger("state", (int)currentState);
    }
    //2차 작업
    //공통변수 : 플레이어와의거리차 = 10f; 이동속도 = 2f;
    /*protected override IEnumerator IDLE2()
    {
        float timer = 10f;
        while (isNewState == false)
        {
            timer -= Time.deltaTime;
            if (timer == 0)
            {
                //특정 지점까지 이동
                //완료되면 IDLE로 전환
                SetState(State.WALK2);
            }
            //else if() 플레이어가 시야범위에 들어오면(특정 키 입력으로 대체) CHASE 상태로 전환
        }
    }
    //IDLE
    //변수 : 생각하는 시간
    //플레이어가 시야범위에 들어오면(특정 키 입력으로 대체) CHASE 상태로 전환
    //생각하는 시간 이후엔 WALK로 전환
    enum point {A,B};
    protected override IEnumerator WALK2()
    {
        float x=0.0f;    //x위치값
        
        transform.Translate(new Vector3(x, 0.0f, 0.0f);//-10 ~ 10
        if (Input.GetMouseButtonDown(0))
        {
            SetState(new State.CHASE2);
        }
        while (x<10f)
        {
            transform.Translate(new Vector3(x, 0.0f, 0.0f));
            x += 2f;
            if (x == -10f)
            {
                while (x > 10f)
                {
                    x -= 2f;
                    transform.Translate(new Vector3(x, 0.0f, 0.0f);
                }
            } 
        }
    }
    //WALK
    //변수 : 지점에 다다르는 시간, 로밍지점리스트, 랜덤변수
    //이동속도는 2f로 고정
    //로밍지점 중 하나를 선택해서 지점까지 이동 - 로밍지점은 enum으로 정의
    //이동 중 플레이어가 시야범위에 들어오면(특정 키 입력으로 대체) CHASE 상태로 전환
    //지점에 다다르면 IDLE로 전환
    protected override IEnumerator CHASE2()
    {

        float pAttack = 2f, Atime = 5f;
        float x = 0.0f;
        while (Atime ==5f)
        {
            x += 4f;
            Atime -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                transform.Translate(new Vector3(x * 2, 0.0f, 0.0f));
                if (pAttack <= 2)
                    SetState(new State.Attack2);
            }
        }
        SetState(new State.IDLE2);
    }
    //CHASE
    //변수 : 공격가능범위, 어그로시간 
    //이동속도가 2배 증가
    //플레이어와의거리차는 점차 줄어든다
    //공격가능범위보다 플레이어와의거리차가 작을때 특정키입력을 하면 ATTACK 상태로 전환
    //어그로시간이 지나면 IDLE로 전환
    protected override IEnumerator ATTACK2()
    {
        bool hit = false;
        float delay = 10f, timer = 20f;

        while (isNewState = false)
        {
            hit = true;
            delay -= Time.deltaTime;
            timer -= Time.deltaTime;
            if (delay <= 5f)
            {
                Debug.Log("Attack!");
                delay = 10f;
            }
            if (timer == 0f)
            {
                SetState(State.DEAD2);
            }
            yield return null;
        }
    }
    //ATTACK
    //플레이어와의거리차 시간은 조금씩 증가한다.
    //플레이어가 공격가능 범위에 있다면 1초에 1회씩 공격
    //특정키를 누르면 CHASE 상태로 전환(공격가능범위를 벗어났다고 판단)
    //공격할때마다 로그를 찍음
    //플레이어가 사망하면(특정 키 입력) IDLE로 전환
    //HP가 0보다 작으면(특정 키 입력)하면 DEAD로 전환
    protected override IEnumerator DEAD2()
    {
        float timer = 3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //연출
        }
        Destroy(this);
    }*/
    //DEAD
    //변수 : 사망연출시간
    //사망연출시간 이후 게임오브젝트가 삭제된다.
}
