using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BaseData baseData; //기본능력치 SO데이터
    [SerializeField] private PlayerData playerData; //플레이어 SO데이터
    [SerializeField] private Transform arrow;   //플레이어 바라보는 화살표

    private float inputX;
    private float inputY;

    private JoyStick joystick;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private CommonHP hp;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        hp = GetComponent<CommonHP>();
        baseData = DataManager.Instance.baseData;

        //신에 있는 조이스틱 스크립트 자동찾기
        if (joystick == null)
        {
            joystick = FindFirstObjectByType<JoyStick>();
        }
    }

    void Update()
    {
        //죽었으면 플레이어 이동x
        if (hp.isDead)
        {
            Dead();
            return;
        }

        //조이스틱 벡터 가져오기
        Vector2 joystickVector = joystick.InputVector;

        //조이스틱 입력있으면 조이스틱으로 이동
        if (joystickVector.magnitude > 0.1f)
        {
            inputX = joystickVector.x / joystick.moveRange;
            inputY = joystickVector.y / joystick.moveRange;
        }
        //조이스틱 입력이 아니면 키보드로 이동
        else
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }
        MoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 dir = new Vector2(inputX, inputY);

        float magnitude = dir.magnitude;

        //대각선 정규화
        if(magnitude > 1.0f)
        {
            dir = dir.normalized;
        }

        rb.velocity = dir * baseData.moveSpeed;

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
        if (arrow == null) return;

        Vector2 dir = new Vector2(inputX, inputY);

        if (dir.magnitude == 0) return;

        //벡터방향 정규화
        Vector2 normalizedDir = dir.normalized;

        //부드럽게 이동
        Vector2 targetPos = normalizedDir * playerData.arrowOffset;

        //화살표 위치
        arrow.localPosition = Vector2.Lerp(arrow.localPosition, targetPos, Time.deltaTime * 10.0f);

        //벡터 방향에서 각도로 변환
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

        //회전값
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        //화살표 회전
        arrow.rotation = Quaternion.Lerp(arrow.rotation, targetRotation, Time.deltaTime * 10.0f);
    }

    //죽었을 때 움직임처리
    private void Dead()
    {
        inputX = 0f;
        inputY = 0f;
    }
}
