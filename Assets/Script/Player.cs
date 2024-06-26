using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    const int Play = 1;
    const int Interaction = 2;
    const int Map = 3;
    Animator anime;
    BoxCollider2D boxcollider;
    Camera mapCam;
    GameObject mainCamera;
    GameObject mapCamera;
    Rigidbody2D rig;
    Vector3 firstMousePos;
    Vector3 move;
    Vector3 mapMove;
    float jumpPower;
    float moveSpeed;
    float mapMoveSpeed;
    float wheel;
    int status;
    public bool canInteraction;
    void Awake()
    {
        anime = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
        move = Vector3.zero;
        mapMove = Vector3.zero;
        jumpPower = 40f;
        moveSpeed = 7f;
        mapMoveSpeed = 5f;
        wheel = 0;
        status = Play;
        canInteraction = false;
    }
    void Start()
    {
        mainCamera = transform.GetChild(0).gameObject;
        mapCamera = transform.GetChild(1).gameObject;
        mapCam = mapCamera.GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        if (status == Play)
            PlayerMove();
    }
    void Update()
    {
        Status();
    }
    void Status()
    {
        switch (status)
        {
            case Play:
                Interaction_Check();
                Map_Check();
                break;
            case Interaction:
                Interaction_Active();
                break;
            case Map:
                Map_Active();
                break;
        }
    }
    void PlayerMove()
    {
        // Player move
        move.x = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Character direction when player move
        if (move.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (move.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        // Run Animation Trigger
        if (Math.Abs(move.x) >= 0.1f)
            anime.SetFloat("MoveSpeed", Math.Abs(move.x));
        else
            anime.SetFloat("MoveSpeed", 0);

        // Jump Animation Trigger
        anime.SetBool("IsGround", Mathf.Abs(rig.velocity.y) <= 0.1f);
        if (Mathf.Abs(rig.velocity.y) > 0.1f) anime.SetFloat("JumpBlend", rig.velocity.normalized.y);
        if (Input.GetKeyDown(KeyCode.Space))
            if (IsGrounded())
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void Interaction_Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (canInteraction)
            {
                status = Interaction;
                move.x = 0;
            }
    }
    void Interaction_Active()
    {
        if (Input.GetKeyDown(KeyCode.E))
            status = Play;
    }
    void Map_Check()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            status = Map;
    }
    void Map_Active()
    {
        transform.localScale = new Vector3(3f, 3f, 1f);

        // Camera change
        mapCamera.SetActive(true);
        mainCamera.SetActive(false);

        // Camera move to keyborad
        mapMove.x = Input.GetAxisRaw("Horizontal") * mapMoveSpeed * Time.deltaTime;
        mapMove.y = Input.GetAxisRaw("Vertical") * mapMoveSpeed * Time.deltaTime;
        mapCamera.transform.position += mapMove;

        // Camera move to mouse
        if (Input.GetMouseButtonDown(0))
            firstMousePos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            mapCamera.transform.position -= (Input.mousePosition - firstMousePos) * Time.deltaTime;
            firstMousePos = Input.mousePosition;
        }

        // Camera zoom in, zoom out
        wheel = Input.GetAxis("Mouse ScrollWheel");
        mapCam.fieldOfView -= wheel * 20f;

        // Exit map
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            status = Play;
            mainCamera.SetActive(true);
            mapCamera.SetActive(false);
            mapCamera.transform.localPosition = new Vector3(0f, 0f, mapCamera.transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);
            mapCam.fieldOfView = 110f;
        }
    }
    bool IsGrounded()
    {
        var ray = Physics2D.BoxCast(
           boxcollider.bounds.center,
           new Vector2(boxcollider.bounds.size.x, 0.2f),
           0f,
           Vector2.down,
           0.8f,
           1 << LayerMask.NameToLayer("Ground"));
        return ray.collider != null;
    }
}
