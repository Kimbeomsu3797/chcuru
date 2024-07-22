using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //HP
    public int hp = 3;
    //이동 속도
    public float speed = 0.5f;
    //반응 거리
    public float reactionDistance = 4.0f;
    //애니메이션 이름
    public string idleAnime = "EnemyIdle";
    public string upAnime = "EnemyUp";
    public string downAnime = "EnemyDown";
    public string rightAnime = "EnemyRight";
    public string leftAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    //현재 애니메이션
    string nowAnimation = "";
    //이전 애니메이션
    string oldAnimation = "";

    float axisH;
    float axisV;
    Rigidbody2D rbody;

    bool isActive = false;

    public int arrangeId = 0;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            if (isActive)
            {
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);
                float angle = rad * Mathf.Rad2Deg;
                if (angle > -45.0f && angle <= 45.0f)
                {
                    nowAnimation = rightAnime;
                }
                else if (angle > 45.0f && angle <= 135.0f)
                {
                    nowAnimation = upAnime;
                }
                else if (angle >= -135.0f && angle <= -45.0f)
                {
                    nowAnimation = downAnime;
                }
                else
                {
                    nowAnimation = leftAnime;
                }
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;
            }
            else
            {
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if ( dist < reactionDistance)
                {
                    isActive = true;
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if(isActive && hp > 0)
        {
            rbody.velocity = new Vector2(axisH, axisV);
            if(nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hp--;
            if (hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                rbody.velocity = new Vector2(0, 0);
                Animator animator = GetComponent<Animator>();
                animator.Play(deadAnime);
                Destroy(gameObject, 0.5f);

                //배치 Id 기록
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }
}
