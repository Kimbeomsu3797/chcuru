using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동 속도
    public float speed = 3f;

    // 애니메이션 이름
    public string upAnim = "PlayerUp";
    public string downAnim = "PlayerDown";
    public string rightAnim = "PlayerRight";
    public string leftAnim = "PlayerLeft";
    public string deadAnim = "PlayerDead";

    // 현재 애니메이션
    string nowAnim = "";
    // 이전 애니메이션
    string oldAnim = "";

    float axisH;
    float axisV;
    public float angleZ = -90.0f;
    Rigidbody2D rbody;
    Rigidbody2D rig;
    bool isMoving = false;

    public static int hp = 3; //플레이어 HP
    public static string gameState; // 게임 상태
    bool inDamage = false; // 데미지 받는 중
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        oldAnim = downAnim;
        //게임 상태를 플레이 중으로 하기
        gameState = "Playing";
        rbody = GetComponent<Rigidbody2D>();
        //HP 갱신
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    
    void Update()
    {
        //게임 중이 아니거나 데미지를 받는 중에는 아무것도 하지 않음.
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
        //게임 중이 아니면 아무것도 하지 않음.
        if(gameState != "Playing")
        {
            return;
        }
        if (inDamage)
        {
            //데미지 받는 중엔 점멸 시키기
            float val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            if( val > 0)
            {
                //스프라이트 표시
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //스프라이트 비표시
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            return;
        }
        rig.velocity = new Vector2(axisH, axisV) * speed;
    }

    // 조이스틱용 함수
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
            // 이동중이면 각도를 변경
            // p1과 p2차이 구하기 (원점을 0으로 하기 위해)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            // 아크탄젠트 함수로 각도 구하기
            float rad = Mathf.Atan2(dy, dx);
            // 라디안 각으로 변환
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            // 정지일 경우 이전 각도 유지
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
            hp--; // HP감소
            //HP갱신
            PlayerPrefs.SetInt("PlayerHP", hp);
            if (hp > 0)
            {
                //이동 중지
                rbody.velocity = new Vector2(0, 0);
                //적 캐릭터의 반대 방향으로 히트백
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
                //데미지 받는 중으로 설정
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            }
            else
            {
                //게임 오버
                GameOver();
            }
        }
    }
    void DamageEnd()
    {
        //데미지 받는 중이 아님으로 설정
        inDamage = false;
        //스프라이트 되돌리기
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void GameOver()
    {
        Debug.Log("게임 오버!");
        gameState = "gameover";
        //=========================
        //게임 오버 연출
        //=========================
        //플레이어 충돌 판정 비활성
        GetComponent<CircleCollider2D>().enabled = false;
        //이동 중지
        rbody.velocity = new Vector2(0, 0);
        //중력을 적용하여 플레이어를 위로 튀어오르게 하는 연출
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // 애니메이션 변경하기
        GetComponent<Animator>().Play(deadAnim);
        //1초후에 플레이어 캐릭터 제거하기
        Destroy(gameObject, 1.0f);
    }

}
