using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//캐릭터의 상태를 정의하고
//캐릭터마다 상태에 따라 코드가 동작하게 구현
// - 플레이어 : 캐릭터 상태 / 입력처리
// - 몬스터 : 캐릭터 상태 / AI 로직
// - NPC : 캐릭터 상태 / 상호작용 처리


public enum State
{
    IDLE = 0,
    WALK =1,
    CHASE = 2,
    ATTACK = 3,
    DEAD = 4,
}

public class Player : MonoBehaviour
{
    public State currentState = State.IDLE;

    private void Start()
    {
        StartCoroutine(FsmMain());

    }

    IEnumerator FsmMain()
    {
        Debug.Log("A");
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
