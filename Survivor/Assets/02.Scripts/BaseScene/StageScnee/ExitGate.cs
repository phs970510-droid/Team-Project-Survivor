
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private MissionBoard stageSceneLode;
    private void Awake()
    {
        stageSceneLode = FindObjectOfType<MissionBoard>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            stageSceneLode.BaseSceneLoder();
        }
    }
}
