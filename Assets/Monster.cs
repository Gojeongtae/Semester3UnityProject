using System.Collections;
using UnityEngine;

public class Monster : FSM
{
    public float idletime = 10f;
    Animator animator;
    //IDLE / WALK / CHASE / ATTACK / DEAD

    //IDLE�� ��쿡 Ư�� �ð��� ������ WALK�� ����

    //WALK�ϰ�쿣 Ư���ð��� ������ IDLE�� ����
    //WALK�� ��쿡 Ŭ���ϸ� �÷��̾ �Ѵ� CHASE ���·� ����

    //CHASE�� ��쿣 Ư�� �ð��� ������ WALK�� ����
    //CHASE�� ��쿣 Ŭ���ϸ� �÷��̾ �����ϴ� ATTACK ���·� ����
    //ATTACK�� ��쿣 1�ʸ��� �����ߴ� �α׸� ��´�
    //ATTACK�� ��쿣 Ư���ð��� ������ DEAD�� ����

    //DEAD�� ��쿣 Ư�� �ð��� ������ ���� ������Ʈ�� ���� ��Ų��.
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
                //Ư�� �������� �̵�
                //�Ϸ�Ǹ� IDLE�� ��ȯ
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
                //Ư�� �������� �̵�
                //�Ϸ�Ǹ� IDLE�� ��ȯ
                
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
            //Ư�� �������� �̵�
            //�Ϸ�Ǹ� IDLE�� ��ȯ
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
    //2�� �۾�
    //���뺯�� : �÷��̾���ǰŸ��� = 10f; �̵��ӵ� = 2f;
    /*protected override IEnumerator IDLE2()
    {
        float timer = 10f;
        while (isNewState == false)
        {
            timer -= Time.deltaTime;
            if (timer == 0)
            {
                //Ư�� �������� �̵�
                //�Ϸ�Ǹ� IDLE�� ��ȯ
                SetState(State.WALK2);
            }
            //else if() �÷��̾ �þ߹����� ������(Ư�� Ű �Է����� ��ü) CHASE ���·� ��ȯ
        }
    }
    //IDLE
    //���� : �����ϴ� �ð�
    //�÷��̾ �þ߹����� ������(Ư�� Ű �Է����� ��ü) CHASE ���·� ��ȯ
    //�����ϴ� �ð� ���Ŀ� WALK�� ��ȯ
    enum point {A,B};
    protected override IEnumerator WALK2()
    {
        float x=0.0f;    //x��ġ��
        
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
    //���� : ������ �ٴٸ��� �ð�, �ι���������Ʈ, ��������
    //�̵��ӵ��� 2f�� ����
    //�ι����� �� �ϳ��� �����ؼ� �������� �̵� - �ι������� enum���� ����
    //�̵� �� �÷��̾ �þ߹����� ������(Ư�� Ű �Է����� ��ü) CHASE ���·� ��ȯ
    //������ �ٴٸ��� IDLE�� ��ȯ
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
    //���� : ���ݰ��ɹ���, ��׷νð� 
    //�̵��ӵ��� 2�� ����
    //�÷��̾���ǰŸ����� ���� �پ���
    //���ݰ��ɹ������� �÷��̾���ǰŸ����� ������ Ư��Ű�Է��� �ϸ� ATTACK ���·� ��ȯ
    //��׷νð��� ������ IDLE�� ��ȯ
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
    //�÷��̾���ǰŸ��� �ð��� ���ݾ� �����Ѵ�.
    //�÷��̾ ���ݰ��� ������ �ִٸ� 1�ʿ� 1ȸ�� ����
    //Ư��Ű�� ������ CHASE ���·� ��ȯ(���ݰ��ɹ����� ����ٰ� �Ǵ�)
    //�����Ҷ����� �α׸� ����
    //�÷��̾ ����ϸ�(Ư�� Ű �Է�) IDLE�� ��ȯ
    //HP�� 0���� ������(Ư�� Ű �Է�)�ϸ� DEAD�� ��ȯ
    protected override IEnumerator DEAD2()
    {
        float timer = 3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //����
        }
        Destroy(this);
    }*/
    //DEAD
    //���� : �������ð�
    //�������ð� ���� ���ӿ�����Ʈ�� �����ȴ�.
}
