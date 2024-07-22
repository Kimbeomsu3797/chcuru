using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed = 3f;

    // �ִϸ��̼� �̸�
    public string upAnim = "PlayerUp";
    public string downAnim = "PlayerDown";
    public string rightAnim = "PlayerRight";
    public string leftAnim = "PlayerLeft";
    public string deadAnim = "PlayerDead";

    // ���� �ִϸ��̼�
    string nowAnim = "";
    // ���� �ִϸ��̼�
    string oldAnim = "";

    float axisH;
    float axisV;
    public float angleZ = -90.0f;
    Rigidbody2D rbody;
    Rigidbody2D rig;
    bool isMoving = false;

    public static int hp = 3; //�÷��̾� HP
    public static string gameState; // ���� ����
    bool inDamage = false; // ������ �޴� ��
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        oldAnim = downAnim;
        //���� ���¸� �÷��� ������ �ϱ�
        gameState = "Playing";
        rbody = GetComponent<Rigidbody2D>();
        //HP ����
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    
    void Update()
    {
        //���� ���� �ƴϰų� �������� �޴� �߿��� �ƹ��͵� ���� ����.
        if(gameState != " Playing" || inDamage)
        {
            return;
        }
        if (!isMoving)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAnim = rightAnim;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            nowAnim = upAnim;
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            nowAnim = downAnim;
        }
        else
        {
            nowAnim = leftAnim;
        }

        if (nowAnim != oldAnim)
        {
            oldAnim = nowAnim;
            GetComponent<Animator>().Play(nowAnim);
        }
    }

    private void FixedUpdate()
    {
        //���� ���� �ƴϸ� �ƹ��͵� ���� ����.
        if(gameState != "Playing")
        {
            return;
        }
        if (inDamage)
        {
            //������ �޴� �߿� ���� ��Ű��
            float val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            if( val > 0)
            {
                //��������Ʈ ǥ��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //��������Ʈ ��ǥ��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            return;
        }
        rig.velocity = new Vector2(axisH, axisV) * speed;
    }

    // ���̽�ƽ�� �Լ�
    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        if (axisH == 0 && axisV == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            // �̵����̸� ������ ����
            // p1�� p2���� ���ϱ� (������ 0���� �ϱ� ����)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            // ��ũź��Ʈ �Լ��� ���� ���ϱ�
            float rad = Mathf.Atan2(dy, dx);
            // ���� ������ ��ȯ
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            // ������ ��� ���� ���� ����
            angle = angleZ;
        }
        return angle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }
    void GetDamage(GameObject enemy)
    {
        if(gameState == "Playing")
        {
            hp--; // HP����
            //HP����
            PlayerPrefs.SetInt("PlayerHP", hp);
            if (hp > 0)
            {
                //�̵� ����
                rbody.velocity = new Vector2(0, 0);
                //�� ĳ������ �ݴ� �������� ��Ʈ��
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
                //������ �޴� ������ ����
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            }
            else
            {
                //���� ����
                GameOver();
            }
        }
    }
    void DamageEnd()
    {
        //������ �޴� ���� �ƴ����� ����
        inDamage = false;
        //��������Ʈ �ǵ�����
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void GameOver()
    {
        Debug.Log("���� ����!");
        gameState = "gameover";
        //=========================
        //���� ���� ����
        //=========================
        //�÷��̾� �浹 ���� ��Ȱ��
        GetComponent<CircleCollider2D>().enabled = false;
        //�̵� ����
        rbody.velocity = new Vector2(0, 0);
        //�߷��� �����Ͽ� �÷��̾ ���� Ƣ������� �ϴ� ����
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // �ִϸ��̼� �����ϱ�
        GetComponent<Animator>().Play(deadAnim);
        //1���Ŀ� �÷��̾� ĳ���� �����ϱ�
        Destroy(gameObject, 1.0f);
    }

}
