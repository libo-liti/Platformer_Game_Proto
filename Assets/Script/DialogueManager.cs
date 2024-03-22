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
    const int Iceball = 101;
    public Text text;
    public GameObject bubble;       // Canvas of dialogue
    List<Dictionary<string, object>> data;
    List<object> maskDialogue;
    List<object> pinkDialogue;
    List<object> virtualDialogue;
    List<object> iceballDialogue;

    public string language;
    public int dialogueIndex;
    public bool isActive = false;
    void Awake()
    {
        language = "english";
        maskDialogue = new List<object>();
        pinkDialogue = new List<object>();
        virtualDialogue = new List<object>();
        iceballDialogue = new List<object>();
    }
    void Start()
    {
        data = CSVReader.Read("Dialogue");
        InsertDialogue();
    }
    public void InsertDialogue()
    {
        maskDialogue.Clear();
        pinkDialogue.Clear();
        virtualDialogue.Clear();
        iceballDialogue.Clear();
        if (language == "korea")
        {
            for (int i = 0; i < data.Count; i++)
            {
                if ($"{data[i]["language"]}" != "korea")
                    continue;
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
                    case Iceball:
                        iceballDialogue.Add(data[i]["text"]);
                        break;
                }
            }
        }
        else if (language == "english")
        {
            for (int i = 0; i < data.Count; i++)
            {
                if ($"{data[i]["language"]}" != "english")
                    continue;
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
                    case Iceball:
                        iceballDialogue.Add(data[i]["text"]);
                        break;
                }
            }
        }
    }
    public void Action(GameObject obj)
    {
        InterectiveObject interectiveObject = obj.GetComponent<InterectiveObject>();
        Talk(interectiveObject.id, obj);
        bubble.SetActive(isActive);
    }
    void Talk(int id, GameObject obj)
    {
        string dialogueData = GetDialogue(id, dialogueIndex);
        if (dialogueData == null)
        {
            if (obj.GetComponent<InterectiveObject>().isDestructible)
                obj.SetActive(false);
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
            case Iceball:
                if (iceballDialogue.Count == dialogueIndex)
                    return null;
                return $"{iceballDialogue[dialogueIndex]}";
        }
        return null;
    }
}
