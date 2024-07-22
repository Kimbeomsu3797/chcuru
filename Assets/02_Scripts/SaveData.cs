using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//"이 클래스는 저장할 대상이다." 정도의 의미 우선은 Json으로 만들기 위해 필요한 것으로 기억해 두자
[System.Serializable]
public class SaveData
{
    public int arrangeId = 0; // 배치 ID
    public string objTag = "";
}
[System.Serializable]
public class SaveDataList
{
    public SaveData[] saveDatas; //SaveData 배열
}

