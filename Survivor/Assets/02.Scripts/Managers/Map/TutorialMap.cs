using UnityEngine;

public class TutorialMap : Chunk
{
#if UNITY_EDITOR //Awake보다 먼저 실행
    private void OnValidate()
    {
        /* 확인필요
        GameObject를 사용할 수 없는 이유 확인필요
        MonoBehaviour에 참조되거나 interface로 참조해야 한다?
        */
        chunkPrefabs = chunkParent.GetComponentsInChildren<ChunkSlot>();
    }
#endif

    //플레이어 오브젝트 자동으로 불러오기

    void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        base.Update();
    }
}
