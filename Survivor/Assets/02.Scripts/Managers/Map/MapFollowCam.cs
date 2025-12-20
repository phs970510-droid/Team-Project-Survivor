using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /*
    1.Pixel Perfect Camera : 1920 x 1080; : 픽셀 가장자리 선 제거
    2.Timemap Renderer : 끄트머리 여백 제거
        :Detect Chunk Culling : Manual
        :Chunk Culling Bound x:1, y:1, z:1
    */

    #region field
    private float followSpeed = 7.0f;

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

#if UNITY_EDITOR
    private void OnValidate()
    {
        var go = GameObject.Find("Player");
        if (go == null)
            return;

        target = go.transform;
    }
#endif
    #endregion

    void Update()
    {
        Follow();
    }

    #region method
    private void Follow()
    {
        Vector3 targetPos = transform.position;

        targetPos.x = target.position.x + offset.x;
        targetPos.y = target.position.y + offset.y;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
    #endregion
}