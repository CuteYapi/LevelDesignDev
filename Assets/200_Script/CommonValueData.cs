using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonValueData : MonoBehaviour
{
    public static CommonValueData I { get; private set; }

    public int PoolMonsterAmount; //오브젝트 풀에 최초 생성할 오브젝트의 개개 수량
    public int SpawnMonsterAmount;
    public float SpawnCircleDistance;

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
