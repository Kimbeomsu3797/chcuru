using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;          //��ŸƮ ��ư
    public GameObject continueButton;       //�̾��ϱ� ��ư
    public string firstSceneName;           //���� ���� �� �̸�
    // Start is called before the first frame u��pdate
    void Start()
    {
        string sceneName = PlayerPrefs.GetString("LastScene");  //���� �� ��
        if(sceneName == "")
        {
            continueButton.GetComponent<Button>().interactable = false; // ��Ȱ��
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = true; // Ȱ��
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClicked()
    {
        //���� �����͸� ����
        PlayerPrefs.DeleteAll();
        //HP �ʱ�ȭ
        PlayerPrefs.SetInt("PlayerHP", 3);
        //����� �������� ������ ����
        PlayerPrefs.SetString("LastScene", firstSceneName); //�� �̸� �ʱ�ȭ
        RoomManager.doorNumber = 0;
        
        SceneManager.LoadScene("SampleScene");
    }
    public void ContinueButtonClicked()
    {
        string sceneName = PlayerPrefs.GetString("LastScene"); //����� ��
        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor"); //�� ��ȣ
        SceneManager.LoadScene(sceneName);
    }
}
