using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//캐릭터의 상태를 정의하고
//캐릭터마다 상태에 따라 코드가 동작하게 구현
// - 플레이어 : 캐릭터 상태 / 입력처리
// - 몬스터 : 캐릭터 상태 / AI 로직
// - NPC : 캐릭터 상태 / 상호작용 처리



public class FSM : MonoBehaviour
{
    public int IDLE = 0;
    public int WALK = 1;
    public int CHASE = 2;
    public int ATTACK = 3;
    public int DEAD = 4;

    private void OnMouseDown()
    {
        if(DEAD == 4)
        {
            Debug.Log("a");
        }
    }

}

public class Player : FSM
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
