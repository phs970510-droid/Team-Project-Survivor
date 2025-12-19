using System.Linq;
using UnityEngine;

public class StageMap : Chunk
{
    #region field
    private enum Type
    {
        type01 = 1,
        type02,
        type03
    }

    private Type selectNumb;

    [Header("SetActive")]
    private GameObject obstacleType;

    [Header("ResetActive")]
    private GameObject type01Bg;
    private GameObject type02Bg;
    private GameObject type03Bg;
    private GameObject Obstacle01;
    private GameObject Obstacle02;
    private GameObject Obstacle03;

#if UNITY_EDITOR
    private void OnValidate()
    {
        player = GameObject.Find("Player").transform;

        ResetActive(); //다른 프리팹이 활성화 되어있다면 비활성화
    }
#endif
    #endregion

    protected override void Awake()
    {
        SelectChunkType();
        base.Awake();
        LoadChunk();
    }

    protected override void Update()
    {
        base.Update();
        
    }

    #region method
    private void SelectChunkType()
    {
        selectNumb = (Type)ChunkManager.Instance.typeNumb;

        switch(selectNumb)
        {
            case Type.type01:
                {
                    //부모 오브젝트 할당
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type01_Background")
                        .gameObject;
                    obstacleType = GameObject.Find("Grid")
                        .transform.Find("Type01_Obstacle")
                        .gameObject;

                    //규격 제한 장애물 타입에 맞춰서 SetActive:true
                    if (chunkParent != null) chunkParent.SetActive(true);
                    if (obstacleType != null) obstacleType.SetActive(true);

                    //자식오브젝트 할당
                    chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
                        .Where(x => x != chunkParent.transform).ToArray();

                    createdParent = GameObject.Find("CreatedParent");
                }
                break;
            case Type.type02:
                {
                    //부모 오브젝트 할당
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type02_Background")
                        .gameObject;
                    obstacleType = GameObject.Find("Grid")
                        .transform.Find("Type02_Obstacle")
                        .gameObject;

                    //규격 제한 장애물 타입에 맞춰서 SetActive:true
                    if (chunkParent != null) chunkParent.SetActive(true);
                    if (obstacleType != null) obstacleType.SetActive(true);

                    //자식오브젝트 할당
                    chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
                        .Where(x => x != chunkParent.transform).ToArray();

                    createdParent = GameObject.Find("CreatedParent");
                }
                break;
            case Type.type03:
                {
                    //부모 오브젝트 할당
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type03_Background")
                        .gameObject;
                    obstacleType = GameObject.Find("Grid")
                        .transform.Find("Type03_Obstacle")
                        .gameObject;

                    //규격 제한 장애물 타입에 맞춰서 SetActive:true
                    if (chunkParent != null) chunkParent.SetActive(true);
                    if (obstacleType != null) obstacleType.SetActive(true);

                    //자식오브젝트 할당
                    chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
                        .Where(x => x != chunkParent.transform).ToArray();

                    createdParent = GameObject.Find("CreatedParent");
                }
                break;
        }
    }

    private void ResetActive()
    {
        type01Bg = GameObject.Find("Grid")
            .transform.Find("Type01_Background")
            .gameObject;
        type02Bg = GameObject.Find("Grid")
            .transform.Find("Type02_Background")
            .gameObject;
        type03Bg = GameObject.Find("Grid")
            .transform.Find("Type03_Background")
            .gameObject;
        Obstacle01 = GameObject.Find("Grid")
            .transform.Find("Type01_Obstacle")
            .gameObject;
        Obstacle02 = GameObject.Find("Grid")
            .transform.Find("Type02_Obstacle")
            .gameObject;
        Obstacle03 = GameObject.Find("Grid")
            .transform.Find("Type03_Obstacle")
            .gameObject;

        type01Bg.SetActive(false);
        type02Bg.SetActive(false);
        type03Bg.SetActive(false);
        Obstacle01.SetActive(false);
        Obstacle02.SetActive(false);
        Obstacle03.SetActive(false);
    }

    private void LoadChunk()
    {
        createdPrefabs = createdParent.GetComponentsInChildren<Transform>()
            .Where(x => x != createdParent.transform).ToArray();
    }
    #endregion
}