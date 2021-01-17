using UnityEngine;

[CreateAssetMenu(fileName ="NPC資料",menuName ="Script/NPC資料")]
public class NPCData : ScriptableObject
{
    [Header("第一段對話"),TextArea(1,4)]
    public string dialougA;
    [Header("第二段對話"),TextArea(1, 4)]
    public string dialougB;
    [Header("第三段對話"),TextArea(1, 4)]
    public string dialougC;
    [Header("任務項目需要數量")]
    public int count;
    [Header("已經取得項目數量")]
    public int countCurrent;
}
