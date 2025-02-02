using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;          //스타트 버튼
    public GameObject continueButton;       //이어하기 버튼
    public string firstSceneName;           //게임 시작 씬 이름
    // Start is called before the first frame uㄴpdate
    void Start()
    {
        string sceneName = PlayerPrefs.GetString("LastScene");  //저장 된 씬
        if(sceneName == "")
        {
            continueButton.GetComponent<Button>().interactable = false; // 비활성
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = true; // 활성
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClicked()
    {
        //저장 데이터를 지움
        PlayerPrefs.DeleteAll();
        //HP 초기화
        PlayerPrefs.SetInt("PlayerHP", 3);
        //저장된 스테이지 정보를 지움
        PlayerPrefs.SetString("LastScene", firstSceneName); //씬 이름 초기화
        RoomManager.doorNumber = 0;
        
        SceneManager.LoadScene("SampleScene");
    }
    public void ContinueButtonClicked()
    {
        string sceneName = PlayerPrefs.GetString("LastScene"); //저장된 씬
        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor"); //문 번호
        SceneManager.LoadScene(sceneName);
    }
}
