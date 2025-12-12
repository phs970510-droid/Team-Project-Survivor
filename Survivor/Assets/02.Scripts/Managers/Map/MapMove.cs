using Unity.VisualScripting;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    #region field
    private float inputX;
    private float inputY;

    private float moveSpeed = 10.0f;
    #endregion

    void Update()
    {
        MoveHandler();
    }

    #region method
    private void MoveHandler()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Move();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(inputX, inputY, 0.0f).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
    #endregion
}