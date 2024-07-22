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
    public string sceneName = ""; // �̵��� �� �̸�
    public int doorNumber = 0; //�� ��ȣ
    public ExitDirection direction = ExitDirection.down; // ���� ��ġ
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
                //BGM ����
                //SoundManager.soundManager.StopBgm();
                //SE ���(���� Ŭ����)
                //SoundManager.sounManager.SEPlay(SEType.GameClear);
                //���� Ŭ����
                GameObject.FindObjectOfType<UIManager>().GameClear();
            }
            else
            {
                string nowScene = PlayerPrefs.GetString("LastScene");
                SaveDataManager.SaveArrangeData(nowScene); // ��ġ������ ����
                RoomManager.ChangeScene(sceneName, doorNumber);
            }
        }
    }
}
