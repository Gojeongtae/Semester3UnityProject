using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ĳ������ ���¸� �����ϰ�
//ĳ���͸��� ���¿� ���� �ڵ尡 �����ϰ� ����
// - �÷��̾� : ĳ���� ���� / �Է�ó��
// - ���� : ĳ���� ���� / AI ����
// - NPC : ĳ���� ���� / ��ȣ�ۿ� ó��



public class FSM : MonoBehaviour
{
    public int IDLE = 0;
    public int WALK = 1;
    public int CHASE = 2;
    public int ATTACK = 3;
    public int DEAD = 4;

 

}
public enum State
{
    IDLE = 0,
    WALK =1,
    CHASE = 2,
    ATTACK = 3,
    DEAD = 4,
}

public class Player : FSM
{
    public State currentState = State.IDLE;

    private void OnMouseDown()
    {
        currentState = State.DEAD;

        if (currentState == State.DEAD)
        {
            Debug.Log("a");
        }
    }
}
