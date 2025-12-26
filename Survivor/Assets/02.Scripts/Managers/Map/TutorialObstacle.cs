using System.Linq;
using UnityEngine;

public class TutorialObstacle : Obstacles
{
    protected override void Awake()
    {
        BaseSetting();
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    #region method
    private void BaseSetting()
    {
        obstacleParentA = GameObject.Find("Grid").transform.Find("Type01_ObjectA").gameObject;
        obstaclePrefabsA = obstacleParentA.GetComponentsInChildren<Transform>()
            .Where(x => x != obstacleParentA.transform).ToArray();
        obstacleParentB = GameObject.Find("Grid").transform.Find("Type01_ObjectB").gameObject;
        obstaclePrefabsB = obstacleParentB.GetComponentsInChildren<Transform>()
            .Where(x => x != obstacleParentB.transform).ToArray();
    }

    protected override void SpawnInteraction()
    {
        
    }
    #endregion
}