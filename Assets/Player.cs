using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ĳ������ ���¸� �����ϰ�
//ĳ���͸��� ���¿� ���� �ڵ尡 �����ϰ� ����
// - �÷��̾� : ĳ���� ���� / �Է�ó��
// - ���� : ĳ���� ���� / AI ����
// - NPC : ĳ���� ���� / ��ȣ�ۿ� ó��



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
        //�� ���¿� ���� �ִϸ��̼� ��ȯ
        Debug.Log("Player." + currentState + ".Start");
        while (isNewState == false)
        {
            //���ð��� ������ �����ϰ� Ư�� ������ �����ؼ� �̵��Ѵ�.
            //SetState(State.Walk);
            yield return null;
        }
    }

    protected override IEnumerator WALK()
    {
        Debug.Log("Player." + currentState + ".Start");
        while(isNewState == false)
        {
            //Ư�� �������� �̵�
            //�Ϸ�Ǹ� IDLE�� ��ȯ
            yield return null;
        }
    }
}
