using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonValueData : MonoBehaviour
{
    public static CommonValueData I { get; private set; }

    public int PoolMonsterAmount;
    public int SpawnMonsterAmount;
    public float SpawnCircleDistance;

    public int PoolSkillAmount;

    public float MapRespawnBorder;

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
