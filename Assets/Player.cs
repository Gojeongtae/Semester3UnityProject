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
    void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetState(State.WALK);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetState(State.IDLE);
        }
    }

   protected override IEnumerator IDLE()
    {
        //이 상태에 오면 애니메이션 전환
        Debug.Log("Player." + currentState + ".Start");
        while (isNewState == false)
        {
            //대기시간이 지나면 랜덤하게 특정 지점을 선택해서 이동한다.
            //SetState(State.Walk);
            yield return null;
        }
    }

    protected override IEnumerator WALK()
    {
        Debug.Log("Player." + currentState + ".Start");
        while(isNewState == false)
        {
            //특정 지점까지 이동
            //완료되면 IDLE로 전환
            yield return null;
        }
    }
}
