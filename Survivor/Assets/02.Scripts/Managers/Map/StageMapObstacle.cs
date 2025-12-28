using System.Linq;
using UnityEngine;

public class StageMapObstacle : Obstacles
{
    private enum ObstacleType
    {
        type01 = 1,
        type02,
        type03
    }

    private ObstacleType selectNum;

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
        selectNum = (ObstacleType)ChunkManager.Instance.typeNumb;

        switch(selectNum)
        {
            case ObstacleType.type01:
                {
                    obstacleParentA = GameObject.Find("Grid").transform.Find("Type01_ObstacleA").gameObject;
                    if (obstacleParentA != null) obstacleParentA.SetActive(true);
                    obstaclePrefabsA = obstacleParentA.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentA.transform).ToArray();

                    obstacleParentB = GameObject.Find("Grid").transform.Find("Type01_ObstacleB").gameObject;
                    if (obstacleParentB != null) obstacleParentB.SetActive(true);
                    obstaclePrefabsB = obstacleParentB.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentB.transform).ToArray();
                }
                break;
            case ObstacleType.type02:
                {
                    obstacleParentA = GameObject.Find("Grid").transform.Find("Type02_ObstacleA").gameObject;
                    if (obstacleParentA != null) obstacleParentA.SetActive(true);
                    obstaclePrefabsA = obstacleParentA.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentA.transform).ToArray();

                    obstacleParentB = GameObject.Find("Grid").transform.Find("Type02_ObstacleB").gameObject;
                    if (obstacleParentB != null) obstacleParentB.SetActive(true);
                    obstaclePrefabsB = obstacleParentB.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentB.transform).ToArray();
                }
                break;
            case ObstacleType.type03:
                {
                    obstacleParentA = GameObject.Find("Grid").transform.Find("Type03_ObstacleA").gameObject;
                    if (obstacleParentA != null) obstacleParentA.SetActive(true);
                    obstaclePrefabsA = obstacleParentA.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentA.transform).ToArray();

                    obstacleParentB = GameObject.Find("Grid").transform.Find("Type03_ObstacleB").gameObject;
                    if (obstacleParentB != null) obstacleParentB.SetActive(true);
                    obstaclePrefabsB = obstacleParentB.GetComponentsInChildren<Transform>()
                        .Where(x => x != obstacleParentB.transform).ToArray();
                }
                break;
        }
    }
    #endregion
}