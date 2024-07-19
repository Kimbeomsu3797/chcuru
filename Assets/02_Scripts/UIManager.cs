using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int hasKeys = 0;                    //열쇠 수
    int hasArrows = 0;                  //화살 소지 수
    int hp = 0;                         //플레이어의 HP
    public GameObject arrowText;        //화살수를 표시하는 Text
    public GameObject keyText;          //열쇠 수를 표시하는 Text
    public GameObject hpImage;          //HP의 수를 표시하는 Image
    public Sprite life3Image;           //HP3 이미지
    public Sprite life2Image;           //HP2 이미지
    public Sprite life1Image;           //HP1 이미지
    public Sprite life0Image;           //HP0 이미지
    public GameObject mainImage;        //이미지를 가지는 GameObject
    public GameObject resetButton;      //리셋 버튼
    public Sprite gameOverSpr;          // Game Over 이미지
    public Sprite gameClearSpr;         // Game Clear 이미지
    public GameObject inputPanel;       // 버추얼 패드와 공격 버튼을 배치한 조작 패널

    public string retrySceneName = "";  //재시도하는 씬 이름
    // Start is called before the first frame update
    void Start()
    {
        UpdateItemCount();  //아이템 수 갱신
        UpdateHP();         //HP갱신
        //이미지를 숨기기
        Invoke("InactiveImage", 1.0f);
        resetButton.SetActive(false); //리셋 버튼 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemCount();  //아이템 수 갱신
        UpdateHP();         //HP갱신
    }

    void UpdateItemCount()
    {
        //화살
        if(hasArrows != Itemkeeper.hasArrows)
        {
            arrowText.GetComponent<Text>().text = Itemkeeper.hasArrows.ToString();
            hasArrows = Itemkeeper.hasArrows;
        }
        //열쇠
        if(hasKeys != Itemkeeper.hasKeys)
        {
            keyText.GetComponent<Text>().text = Itemkeeper.hasKeys.ToString();
            hasKeys = Itemkeeper.hasKeys;
        }
    }
    
    void UpdateHP()
    {
        //Player 가져오기
        if(PlayerController.gameState != "gameend")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                if(PlayerController.hp != hp)
                {
                    hp = PlayerController.hp;
                    if(hp <= 0)
                    {
                        hpImage.GetComponent<Image>().sprite = life0Image;
                        //플레이어 사망!!
                        resetButton.SetActive(true);                            //버튼 표시
                        mainImage.SetActive(true);                              //이미지 표시
                        mainImage.GetComponent<Image>().sprite = gameOverSpr;   //이미지 설정
                        inputPanel.SetActive(false);                            //조작 UI 숨기기
                        PlayerController.gameState = "gameend";                 //게임 종료

                    }
                    else if (hp == 1)
                    {
                        hpImage.GetComponent<Image>().sprite = life1Image;
                    }
                    else if (hp == 2)
                    {
                        hpImage.GetComponent<Image>().sprite = life2Image;
                    }
                    else
                    {
                        hpImage.GetComponent<Image>().sprite = life3Image;
                    }
                    
                }
            }
        }

    }
    public void Retry()
    {
        //HP 되돌리기
        PlayerPrefs.SetInt("PlayerHP", 3);
        //게임 중으로 설정
        SceneManager.LoadScene("retrySceneName"); // 씬 이동
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
