using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogueData;
    public Text text;
    public GameObject bubble;       // Canvas of dialogue
    public int dialogueIndex;
    public bool isActive = false;
    void Awake()
    {
        dialogueData = new Dictionary<int, string[]>();
        CreateData();
    }
    void CreateData()
    {
        dialogueData.Add(10, new string[] { "안녕?", "배고프다 너는 어떻니?" });
        dialogueData.Add(11, new string[] { "피곤한데 어디서 쉴 곳 없을까?", "야니야 그래도 공부해야돼..." });
        dialogueData.Add(12, new string[] { "안녕하세요.", "잘가세요." });
    }
    public void Action(GameObject obj)
    {
        NPC npc = obj.GetComponent<NPC>();
        Talk(npc.id);
        bubble.SetActive(isActive);
    }
    void Talk(int id)
    {
        string dialogueData = GetDialogue(id, dialogueIndex);
        if (dialogueData == null)
        {
            isActive = false;
            dialogueIndex = 0;
            return;
        }
        text.text = dialogueData;
        isActive = true;
        dialogueIndex++;
    }
    public string GetDialogue(int id, int dialogueIndex)
    {
        if (dialogueIndex == dialogueData[id].Length)
            return null;
        return dialogueData[id][dialogueIndex];
    }
}
