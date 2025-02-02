using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    arrow,
    key,
    life,
}
public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;

    public int arrangeId = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(type == ItemType.key)
            {
                //열쇠
                Itemkeeper.hasKeys += 1;

            }
            else if(type == ItemType.arrow)
            {
                ArrowShoot shoot = collision.gameObject.GetComponent<ArrowShoot>();
                Itemkeeper.hasArrows += count;
            }
            else if (type == ItemType.life)
            {
                if(PlayerController.hp < 3)
                {
                    //HP가 3이하면 추가
                    PlayerController.hp++;
                    //HP 갱신
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                }
            }
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
        itemBody.gravityScale = 2.5f;
        itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
        Destroy(gameObject, 0.5f);

        //배치 Id 기록
        SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
    }
}
