using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("카메라 위치 설정")]
    [SerializeField] private Vector3 offset;

    [Header("카메라 LookAhead 설정")]
    [SerializeField] private float lookAheadDist;
    [SerializeField] private float lookAheadSpeed;

    [Header("카메라 제한 설정")]
    [SerializeField] private float maxDistance;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (player == null) return;

        //플레이어 입력 감지
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //플레이어 이동방향
        Vector3 moveDir = new Vector3 (h, v, 0f).normalized;

        Vector3 cameraPos;

        if (moveDir.magnitude > 0.1f)
        {
            //플레이어 입력을 받으면 그 방향으로 lookAheadDist만큼 보기
            Vector3 cameraLookAhead = moveDir * lookAheadDist;
            cameraPos = player.position + offset + cameraLookAhead;
        }
        else
        {
            //플레이어 입력 없으면 카메라 포지션은 플레이어 포지션에 오프셋만큼
            cameraPos = player.position + offset;
        }

        //카메라 위치 제한
        cameraPos.x = Mathf.Clamp(cameraPos.x, -maxDistance, maxDistance);
        cameraPos.y = Mathf.Clamp(cameraPos.y, -maxDistance, maxDistance);

        transform.position = Vector3.Lerp(transform.position, cameraPos, lookAheadSpeed * Time.deltaTime);
    }
}
