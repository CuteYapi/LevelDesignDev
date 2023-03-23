using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommonValueData : MonoBehaviour
{
    public static CommonValueData I { get; private set; }

    public int PoolMonsterAmount;
    public int SpawnMonsterAmount;
    public float SpawnCircleDistance;

    public int PoolSkillAmount;

    public float MapRespawnBorder;

    public float CurrentTime = 0;
    public float CurrentKillCount = 0;
    public float CurrentScore = 0;

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CurrentTime += 0.1f;
            UI_ViewModel.I.RenewalBoard("Time");
        }
    }

}
