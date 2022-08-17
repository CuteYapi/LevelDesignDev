﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillViewModel : MonoBehaviour
{
    public static SkillViewModel I { get; set; }
    [SerializeField] private Transform SkillSpawnPool;

    public List<SkillDataData> UseSkillData = new List<SkillDataData>();
    public List<GameObject> SkillPool = new List<GameObject>();

    private void Awake()
    {
        if( I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void Start()
    {
        SetSkill("FireBall");
    }

    public void SetSkill(string skillID)
    {
        UseSkillData.Add(SkillModel.I.SkillData.dataArray.Where(x => x.Skillid == skillID).FirstOrDefault());

        for (int i = 0; i < CommonValueData.I.PoolSkillAmount; i++)
        {
            SkillPoolLoad("Skill/" + UseSkillData.Find(x => x.Skillid == skillID).Prefabid);
        }
        StartCoroutine(FireSkill(skillID));
    }

    private GameObject SkillPoolLoad(string path)
    {
        GameObject spawnSkill = (GameObject)Instantiate(Resources.Load(path),
                                                            SkillSpawnPool);
        spawnSkill.SetActive(false);
        SkillPool.Add(spawnSkill);

        return spawnSkill;
    }

    private void SkillSpawn(string skillID)
    {
        string prefabID = UseSkillData.Find(x => x.Skillid == skillID).Prefabid;
        GameObject spawnSkill = SkillPool.Find(x => x.gameObject.name == prefabID + "(Clone)" && !x.activeSelf);

        if(spawnSkill == null)
        {
            spawnSkill = SkillPoolLoad("Skill/" + prefabID);

            Debug.Log("Pool is Full. Make CommonValueData.PoolSkillAmount More Bigger!");
        }

        spawnSkill.SetActive(true);
    }

    IEnumerator FireSkill(string skillID)
    {
        float intervalTime = UseSkillData.Find(x => x.Skillid == skillID).Attackinterval;
        while (true)
        {
            SkillSpawn(skillID);
            yield return new WaitForSeconds(intervalTime);
        }
    }
}