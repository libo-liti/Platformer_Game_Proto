using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    List<GameObject> CollectionObject;
    public GameObject colltion;
    void Awake()
    {
        CollectionObject = new List<GameObject>();
    }
    void Start()
    {

    }
    void Update()
    {

    }
    public int Id_Check(GameObject obj)
    {
        int id = obj.GetComponent<InterectiveObject>().id;
        for (int i = 0; i < CollectionObject.Count; i++)
        {
            if (id == CollectionObject[i].GetComponent<InterectiveObject>().id)
                return 1;
        }
        CollectionObject.Add(obj);
        Transform slot = colltion.transform.GetChild(CollectionObject.Count - 1);
        Transform slotChild = slot.GetChild(0);
        Image slotImage = slotChild.GetComponent<Image>();
        slotImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
        return 0;
    }
}
