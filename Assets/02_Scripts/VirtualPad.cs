using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 70; //���� �����̴� �ִ� �Ÿ�
    public bool is4Dpad = false; // �����¿�� �����̴��� ����
    GameObject player;          //���� �� �÷��̾� gameobject
    Vector2 defPos;             //���� �ʱ� ��ǥ
    Vector2 downPos;            // ��ġ ��ġ

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defPos = GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PadDown()
    {
        downPos = Input.mousePosition;//���콺 ��ũ�� ��ǥ
    }
    public void PadDrag()
    {
        //���콺 ����Ʈ�� ��ũ�� ��ǥ
        Vector2 mousePosition = Input.mousePosition;
        // ���ο� �� ��ġ ���ϱ�
        Vector2 newTabPos = mousePosition - downPos; // ���콺 �ٿ� ��ġ�� ������ �̵� �Ÿ�
        if(is4Dpad == false)
        {
            newTabPos.y = 0; // Ⱦ��ũ�� �� ���� y ���� 0���� �Ѵ�.
        }
        //�̵� ���� ����ϱ�
        Vector2 axis = newTabPos.normalized; // ���͸� ����ȭ
        // �� ���� �Ÿ� ���ϱ�
        float len = Vector2.Distance(defPos, newTabPos);
        if(len > MaxLength)
        {
            //�Ѱ�Ÿ��� �Ѱ�� ������ �Ѱ� ��ǥ�� ����
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;

        }
        //�� �̵� ��Ű��
        GetComponent<RectTransform>().localPosition = newTabPos;
        //�÷��̾� ĳ���� �̵� ��Ű��
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(axis.x, axis.y);
    }
    //�� �̺�Ʈ
    public void PadUp()
    {
        //�� ��ġ�� �ʱ�ȭ
        GetComponent<RectTransform>().localPosition = defPos;
        //�÷��̾� ĳ���� ���� ��Ű��
        PlayerController plcnt = player.GetComponent<PlayerController>();
        plcnt.SetAxis(0, 0);
    }

    //����
    public void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ArrowShoot shoot = player.GetComponent<ArrowShoot>();
        shoot.Attack();
    }
}
