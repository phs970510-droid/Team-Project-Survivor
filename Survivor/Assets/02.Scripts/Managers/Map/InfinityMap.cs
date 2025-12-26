using System.Linq;
using UnityEngine;

public class InfinityMap : Chunk
{
    #region field
    private enum Type
    {
        type01 = 1,
        type02,
        type03
    }

    private Type selectNumb;

    /* 보류
    [Header("rotate interval")]
    private float currentTime;
    private int count = 1;
    */
    [Header("ResetActive")]
    private GameObject type01Bg;
    private GameObject type02Bg;
    private GameObject type03Bg;
    /* 장애물 작업 완료 후 진행 예정
    private GameObject obstacleA01;
    private GameObject obstacleB01;
    private GameObject obstacleA02;
    private GameObject obstacleB02;
    private GameObject obstacleA03;
    private GameObject obstacleB03;
    */
    
    #endregion

    protected override void Awake()
    {
        BaseSetting();
        LotationChunkType();
        base.Awake();
        LoadChunk();
    }

    protected override void Update()
    {
        base.Update();
        LotationChunkType();
        /* 보류
        currentTime += Time.deltaTime; // = 사용할 경우 1f 단위로 계속 초기화 +=증감식 사용

        if (currentTime > 5) //test 1이 포함이 안되고 초기화 작업이 없으니 청크가 변경되지 않는다
        {
            count++;

            selectNumb = (Type)count;

            if (count >= 3) count = 1;
        }
        */
    }

    #region method
    private void BaseSetting()
    {
        player = GameObject.Find("Player").transform;
        ResetActive();
    }

    private void LotationChunkType()
    {
        selectNumb = (Type)ChunkManager.Instance.typeNumb;

        switch (selectNumb)
        {
            case Type.type01:
                {
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type01_Background")
                        .gameObject;

                    if (chunkParent != null) chunkParent.SetActive(true);

                    chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
                        .Where(x => x != chunkParent.transform).ToArray();

                    createdParent = GameObject.Find("CreatedParent");
                }
                break;
            case Type.type02:
                {
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type02_Background")
                        .gameObject;

                    if (chunkParent != null) chunkParent.SetActive(true);

                    chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
                        .Where(x => x != chunkParent.transform).ToArray();

                    createdParent = GameObject.Find("CreatedParent");
                }
                break;
            case Type.type03:
                {
                    chunkParent = GameObject.Find("Grid")
                        .transform.Find("Type03_Background")
                        .gameObject;

                    if (chunkParent != null) chunkParent.SetActive(true);

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
    }

    private void LoadChunk()
    {
        createdPrefabs = createdParent.GetComponentsInChildren<Transform>()
            .Where(x => x != createdParent.transform).ToArray();
    }
    #endregion
}