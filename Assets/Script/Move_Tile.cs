using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Tile : MonoBehaviour
{
    Rigidbody2D rig;
    float moveSpeed;
    [SerializeField]
    int tileNum;
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
    }
    private void Update()
    {
        if (transform.position.y >= 8)
            rig.velocity = Vector3.down * moveSpeed;
        else if (transform.position.y <= 0)
            rig.velocity = Vector3.up * moveSpeed;
    }
}
