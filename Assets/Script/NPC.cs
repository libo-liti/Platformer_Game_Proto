using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    Camera ccamera;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Player").GetComponent<Player>().canInteraction = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (transform.position.x - other.transform.position.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).transform.GetChild(0).transform.position = ccamera.WorldToScreenPoint(transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().canInteraction = false;
        }
    }
}
