using System.Linq;
using UnityEngine;

public class TutorialMap : Chunk
{
    protected override void Awake()
    {
        BaseSetting();
        base.Awake();
        LoadChunk();
    }

    protected override void Update()
    {
        base.Update();
    }

    #region method
    private void BaseSetting()
    {
        player = GameObject.Find("Player").transform;

        chunkParent = GameObject.Find("Grid").transform.Find("Type01_Background").gameObject;

        chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
            .Where(x => x != chunkParent.transform).ToArray();

        createdParent = GameObject.Find("CreatedParent");
    }

    private void LoadChunk()
    {
        createdPrefabs = createdParent.GetComponentsInChildren<Transform>()
            .Where(x => x != createdParent.transform).ToArray();
    }
    #endregion
}