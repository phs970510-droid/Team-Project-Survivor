using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class JoyStick : MonoBehaviour
{
    [Header("조이스틱")]
    public float moveRange = 50f;  //조이스틱 움직이는 범위
    public Image back;
    public Image joyStick;

    private Vector2 startPos;   //터치시작위치
    private Vector2 inputVector;//조이스틱 입력벡터
    private bool isTouching = false;//터치했는지

    public Vector2 InputVector => inputVector;

    private void Awake()
    {
        //조이스틱 핸들 중앙으로
        joyStick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        //터치했으면
        if (Input.GetMouseButtonDown(0))
        {
            isTouching = true;
            UpdateJoyStick(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //터치 끝나면 조이스틱 원래대로
            isTouching = false;
            joyStick.rectTransform.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
        }

        //터치중일 때만 조이스틱 업데이트 가능
        if (isTouching && Input.GetMouseButton(0))
        {
            UpdateJoyStick(Input.mousePosition);
        }
    }

    private void UpdateJoyStick(Vector2 pos)
    {
        Vector2 currentPos;

        //터치 위치를 UI좌표로 변환하기
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            back.rectTransform, pos, null, out currentPos);

        //조이스틱 시작 위치를 기준으로 벡터 계산
        inputVector = currentPos - startPos;

        //범위 내에서만 조이스틱가능
        if (inputVector.magnitude > moveRange)
        {
            inputVector = inputVector.normalized * moveRange;
        }

        //조이스틱 이미지를 현재 입력 벡터위치로 이동
        joyStick.rectTransform.anchoredPosition = inputVector;
    }

    private void OnDisable()
    {
        ResetJoystick();
    }

    public void ResetJoystick()
    {
        isTouching = false;
        inputVector = Vector2.zero;
        startPos = Vector2.zero;
        joyStick.rectTransform.anchoredPosition = Vector2.zero;
    }
}