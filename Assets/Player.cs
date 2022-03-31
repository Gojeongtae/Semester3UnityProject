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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//���� ī�޶�
            RaycastHit hitInfo;         //3.24 11:06 1�����ֿ� 

            int test = 1 << 0;
            test = 1 << 1;
            test = 1 << 2;
            if (Physics.Raycast(ray, out hitInfo, 100f, 1 << LayerMask.NameToLayer("Enemy")))           //���� Ŭ�� ���� ��
            {
                Debug.Log("Target lock on! Chase that!: " + hitInfo.point);
                //transform.position.position = hitInfo.point;
                marker.transform.SetParent(hitInfo.transform);
                marker.transform.localPosition = Vector3.zero;
                SetState(State.CHASE);
            }
            else if (Physics.Raycast(ray, out hitInfo, 100f, 1 << LayerMask.NameToLayer("MoveArea")))         //out ������ ...  1 << LayerMask.NameToLayer("Default"): Default��� ���� �ν��� �� �ְ� ���ش�.
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
        //�� ���¿� ���� �ִϸ��̼� ��ȯ
        //Debug.Log("Player." + currentState + ".Start");

        //StartCoroutine(base.IDLE());

        while (isNewState == false)
        {
            //���ð��� ������ �����ϰ� Ư�� ������ �����ؼ� �̵��Ѵ�.
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

            //������ ���� �Ÿ��� �ٴٸ��� ����
            //Vector3 dir = marker.transform.position - transform.position;   //���ߵǴ� ������ ���´�.
            //Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);                  //

            
            Vector3 dest = new Vector3(marker.transform.position.x, transform.position.y, marker.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed *2* Time.deltaTime);


            //Debug.Log("dirXZ : " + dirXZ);
            if (Vector3.Distance(transform.position, dest) <= 1f)           //���� �Ÿ�
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

            if (Vector3.Distance(transform.position, dest) >= 3f)       //�����Ÿ��� ����� chase�ϰ� �Ѵ�.
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

            //Vector3 dir = marker.transform.position - transform.position;   //���ߵǴ� ������ ���´�.
            //Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);                  //

            float moveSpeed = 5f;
            Vector3 dest = new Vector3(marker.transform.position.x, transform.position.y, marker.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);


            //Debug.Log("dirXZ : " + dirXZ);
            if(Vector3.Distance(transform.position, dest) <= 0.1f)
            {
                SetState(State.IDLE);
            }
            //Ư�� �������� �̵�
            //�Ϸ�Ǹ� IDLE�� ��ȯ
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
