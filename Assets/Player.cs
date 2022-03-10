using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ĳ������ ���¸� �����ϰ�
//ĳ���͸��� ���¿� ���� �ڵ尡 �����ϰ� ����
// - �÷��̾� : ĳ���� ���� / �Է�ó��
// - ���� : ĳ���� ���� / AI ����
// - NPC : ĳ���� ���� / ��ȣ�ۿ� ó��


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
