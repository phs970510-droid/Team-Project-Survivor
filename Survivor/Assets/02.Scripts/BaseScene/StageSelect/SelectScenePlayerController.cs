using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectScenePlayerController : MonoBehaviour
{

    private float inputX;
    private float inputY;

    private float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveHandler();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void MoveHandler()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        MoveDirection();

    }
    private void Move()
    {
        Vector2 dir = new Vector2(inputX, inputY).normalized;


        rb.velocity = dir * moveSpeed;

        //좌우반전
        if (inputX != 0f)
        {
            if (inputX < 0.0f)
            {
                sr.flipX = true;

            }
            else
            {
                sr.flipX = false;
            }
        }
        float speed = rb.velocity.magnitude;
        anim.SetFloat("Speed", speed);
    }

    //캐릭터가 바라보는 방향
    private void MoveDirection()
    {

        Vector2 dir = new Vector2(inputX, inputY);

        if (dir.magnitude == 0) return;

        //벡터방향 정규화
        Vector2 normalizedDir = dir.normalized;


        //벡터 방향에서 각도로 변환
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

        //회전값
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

    }
}
