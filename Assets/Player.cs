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
