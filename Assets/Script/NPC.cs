using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    GameObject UICanvas;
    Player player;
    public int id;
    void Start()
    {
        UICanvas = transform.GetChild(0).gameObject;
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UICanvas.SetActive(true);
            player.canInteraction = true;
            player.coliider = gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Look at the player
            if (transform.position.x - other.transform.position.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UICanvas.SetActive(false);
            player.canInteraction = false;
            player.coliider = null;
        }
    }
}
