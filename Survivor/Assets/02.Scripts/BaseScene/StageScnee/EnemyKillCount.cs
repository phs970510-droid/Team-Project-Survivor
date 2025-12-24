using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyKillCount : MonoBehaviour
{
    public int killcount;

    public void  AddKill()
    {
        killcount++;
    }
}
