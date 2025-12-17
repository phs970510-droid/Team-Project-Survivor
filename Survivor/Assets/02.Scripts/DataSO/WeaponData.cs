using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("무기 설정")]
    public GameObject weaponPrefab;
    public string weaponName;
    public bool isUnlocked;
    public float damage = 10f;
    public float speed = 10f;
    public float fireCoolTime = 1.0f;
    public int bulletCount = 1;
    public float attackRange = 10f;
    public int price;
    public Sprite Sprite;


    //무기 번호
    //무기 종류 리스트 or 배열
    //무기 레벨에 따른 별 개수
    //무기 레벨에 따른 설명
    //무기 레벨에 따른 공격력 리스트
    //무기 레벨에 따른 불릿 개수
    //무기 레벨에 따른 쿨타임 감소 리스트
    //무기 레벨에 따른 공격 거리 감소 리스트
}
