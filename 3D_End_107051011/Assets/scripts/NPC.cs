using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject diaulog;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話名稱")]
    public Text textname;
    [Header("對話間隔")]
    public float interval = 0.01f;

    public bool PlayerInArea;

    public enum NPCState 
    {
        FirstDialog,Missioning,Finish
    }
    public NPCState state = NPCState.FirstDialog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Wallen") 
        {
            PlayerInArea = true;
            StartCoroutine(Dialog());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Wallen") 
        {
            PlayerInArea = false;
            StopDialog();
        }
    }

    private void StopDialog() 
    {
        diaulog.SetActive(false);
        StopAllCoroutines();
    }
    private IEnumerator Dialog() 
    {
        diaulog.SetActive(true);
        textContent.text = "";
        textname.text = name;

        string dialogString = data.dialougA;

        switch (state)
        {
            case NPCState.FirstDialog:
                dialogString = data.dialougA;
                break;
            case NPCState.Missioning:
                dialogString = data.dialougB;
                break;
            case NPCState.Finish:
                dialogString = data.dialougC;
                break;
        }

        for (int i = 0; i < dialogString.Length; i++)
        {
            textContent.text += dialogString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }

    
}
