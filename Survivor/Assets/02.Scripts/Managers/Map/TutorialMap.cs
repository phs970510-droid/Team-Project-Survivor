using System.Linq;
using UnityEngine;

public class TutorialMap : Chunk
{
#if UNITY_EDITOR //Awake보다 먼저 실행
    private void OnValidate()
    {
        //플레이어 오브젝트 자동으로 불러오기
        player = GameObject.Find("Player").transform;

        //맵 스프라이트 부모 오브젝트 자동으로 찾아서 할당
        chunkParent = GameObject.Find("Grid").transform.Find("Type01_Background");

        /* 주의사항
        GameObject는 컴포넌트가 아니므로 사용불가
        - gameObject에 붙어있는 Transform 컴포넌트 활용
        */
        //자식 오브젝트 배열에 할당
        chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
            .Where(x => x != chunkParent.transform).ToArray();
    }
#endif

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }
}