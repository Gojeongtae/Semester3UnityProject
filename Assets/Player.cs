using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//캐릭터의 상태를 정의하고
//캐릭터마다 상태에 따라 코드가 동작하게 구현
// - 플레이어 : 캐릭터 상태 / 입력처리
// - 몬스터 : 캐릭터 상태 / AI 로직
// - NPC : 캐릭터 상태 / 상호작용 처리



public class Player : FSM
{
    public float maxHP;
    float currentHP;
    public GameObject marker;
    public GameObject Enemy;
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        base.Start();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//메인 카메라
            RaycastHit hitInfo;         //3.24 11:06 1교시주역 

            int test = 1 << 0;
            test = 1 << 1;
            test = 1 << 2;
            if (Physics.Raycast(ray, out hitInfo, 100f, 1 << LayerMask.NameToLayer("Enemy")))           //적을 클릭 했을 때
            {
                Debug.Log("Target lock on! Chase that!: " + hitInfo.point);
                //transform.position.position = hitInfo.point;
                marker.transform.SetParent(hitInfo.transform);
                marker.transform.localPosition = Vector3.zero;
                SetState(State.CHASE);
            }
            else if (Physics.Raycast(ray, out hitInfo, 100f, 1 << LayerMask.NameToLayer("MoveArea")))         //out 변수는 ...  1 << LayerMask.NameToLayer("Default"): Default라는 것을 인식할 수 있게 해준다.
            {
                Debug.Log("RayCast Hit : " + hitInfo.point);
               
                marker.transform.SetParent(null);
                marker.transform.position = hitInfo.point;
                
                SetState(State.WALK);
                
                //SetState(State.WALK);
            }

        }
        
    }
    public void Damage(float attack)
    {
        currentHP = currentHP - attack;

        /*if (currentHP <= 0f)
        {

        }*/
    }
    protected override IEnumerator IDLE()
    {
        //이 상태에 오면 애니메이션 전환
        //Debug.Log("Player." + currentState + ".Start");

        //StartCoroutine(base.IDLE());

        while (isNewState == false)
        {
            //대기시간이 지나면 랜덤하게 특정 지점을 선택해서 이동한다.
            //SetState(State.Walk);
            //Debug.Log("Player." + currentState + ".Start");
            yield return null;
        }
    }
    float moveSpeed = 5f;
    protected override IEnumerator CHASE()
    {

        while (isNewState == false)
        {

            //적과의 일정 거리에 다다르면 어택
            //Vector3 dir = marker.transform.position - transform.position;   //가야되는 방향이 나온다.
            //Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);                  //

            
            Vector3 dest = new Vector3(marker.transform.position.x, transform.position.y, marker.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed *2* Time.deltaTime);


            //Debug.Log("dirXZ : " + dirXZ);
            if (Vector3.Distance(transform.position, dest) <= 1f)           //접근 거리
            {
                SetState(State.ATTACK);
            }
            yield return null;
        }
    }
    protected override IEnumerator ATTACK()
    {
       while(isNewState ==false)
       {
            Vector3 dest = new Vector3(marker.transform.position.x, transform.position.y, marker.transform.position.z);

           //transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed *2* Time.deltaTime);

            if (Vector3.Distance(transform.position, dest) >= 3f)       //사정거리가 벗어나면 chase하게 한다.
            { 
                SetState(State.CHASE);
            }

             
            yield return null;
       }
    }
    protected override IEnumerator WALK()
    {
        //Debug.Log("Player." + currentState + ".Start");
        while (isNewState == false)
        {

            //Vector3 dir = marker.transform.position - transform.position;   //가야되는 방향이 나온다.
            //Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);                  //

            float moveSpeed = 5f;
            Vector3 dest = new Vector3(marker.transform.position.x, transform.position.y, marker.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);


            //Debug.Log("dirXZ : " + dirXZ);
            if(Vector3.Distance(transform.position, dest) <= 0.1f)
            {
                SetState(State.IDLE);
            }
            //특정 지점까지 이동
            //완료되면 IDLE로 전환
            yield return null;
        }
    }
    protected override IEnumerator DEAD()
    {
        yield return null;

    }
    private void OnMouseDown()
    {
        currentState = State.DEAD;

        if (currentState == State.DEAD)
        {
            Debug.Log("a");
        }
    }
}
