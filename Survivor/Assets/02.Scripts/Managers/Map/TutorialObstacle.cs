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
        obstacleParents = GameObject.Find("Grid").transform.Find("Type01_Object").gameObject;
        obstaclePrefabs = obstacleParents.GetComponentsInChildren<Transform>()
            .Where(x => x != obstacleParents.transform).ToArray();
    }

    protected override void HordingTime()
    {
        
    }

    protected override void ObjectHp()
    {
        
    }

    protected override void SpawnInteraction()
    {
        
    }
    #endregion
}