using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    const int Mask = 10;
    const int Pink = 11;
    const int Virtual = 12;
    public Text text;
    public GameObject bubble;       // Canvas of dialogue
    List<Dictionary<string, object>> data;
    List<object> maskDialogue;
    List<object> pinkDialogue;
    List<object> virtualDialogue;
    public int dialogueIndex;
    public bool isActive = false;
    void Awake()
    {
        maskDialogue = new List<object>();
        pinkDialogue = new List<object>();
        virtualDialogue = new List<object>();
    }
    void Start()
    {
        data = CSVReader.Read("csvTest");
        InsertDialogue();
    }
    void InsertDialogue()
    {
        for (int i = 0; i < data.Count; i++)
        {
            switch (data[i]["id"])
            {
                case Mask:
                    maskDialogue.Add(data[i]["text"]);
                    break;
                case Pink:
                    pinkDialogue.Add(data[i]["text"]);
                    break;
                case Virtual:
                    virtualDialogue.Add(data[i]["text"]);
                    break;
            }
        }
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
    string GetDialogue(int id, int dialogueIndex)
    {
        switch (id)
        {
            case Mask:
                if (maskDialogue.Count == dialogueIndex)
                    return null;
                return $"{maskDialogue[dialogueIndex]}";
            case Pink:
                if (pinkDialogue.Count == dialogueIndex)
                    return null;
                return $"{pinkDialogue[dialogueIndex]}";
            case Virtual:
                if (virtualDialogue.Count == dialogueIndex)
                    return null;
                return $"{virtualDialogue[dialogueIndex]}";
        }
        return null;
    }
}
