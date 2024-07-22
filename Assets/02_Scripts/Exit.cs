using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{
    right,
    left,
    down,
    up,
}

public class Exit : MonoBehaviour
{
    public string sceneName = ""; // 이동할 씬 이름
    public int doorNumber = 0; //문 번호
    public ExitDirection direction = ExitDirection.down; // 문의 위치
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
            if (doorNumber == 100)
            {
                //BGM 정지
                //SoundManager.soundManager.StopBgm();
                //SE 재생(게임 클리어)
                //SoundManager.sounManager.SEPlay(SEType.GameClear);
                //게임 클리어
                GameObject.FindObjectOfType<UIManager>().GameClear();
            }
            else
            {
                string nowScene = PlayerPrefs.GetString("LastScene");
                SaveDataManager.SaveArrangeData(nowScene); // 배치데이터 저장
                RoomManager.ChangeScene(sceneName, doorNumber);
            }
        }
    }
}
