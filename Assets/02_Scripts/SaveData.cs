using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//"�� Ŭ������ ������ ����̴�." ������ �ǹ� �켱�� Json���� ����� ���� �ʿ��� ������ ����� ����
[System.Serializable]
public class SaveData
{
    public int arrangeId = 0; // ��ġ ID
    public string objTag = "";
}
[System.Serializable]
public class SaveDataList
{
    public SaveData[] saveDatas; //SaveData �迭
}

