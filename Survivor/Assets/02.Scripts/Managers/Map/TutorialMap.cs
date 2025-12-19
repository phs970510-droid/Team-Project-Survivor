using System.Linq;
using UnityEngine;

public class TutorialMap : Chunk
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        player = GameObject.Find("Player").transform;

        chunkParent = GameObject.Find("Grid").transform.Find("Type01_Background").gameObject;

        chunkPrefabs = chunkParent.GetComponentsInChildren<Transform>()
            .Where(x => x != chunkParent.transform).ToArray();

        createdParent = GameObject.Find("CreatedParent");
    }
#endif

    protected override void Awake()
    {
        base.Awake();
        LoadChunk();
    }

    protected override void Update()
    {
        base.Update();
    }

    #region method
    private void LoadChunk()
    {
        createdPrefabs = createdParent.GetComponentsInChildren<Transform>()
            .Where(x => x != createdParent.transform).ToArray();
    }
    #endregion
}