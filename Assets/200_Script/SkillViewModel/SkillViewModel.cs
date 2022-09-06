using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillViewModel : MonoBehaviour
{
    public static SkillViewModel I { get; set; }
    [SerializeField] private Transform SkillSpawnPool;

    public List<SkillInformationData> UseSkillData = new List<SkillInformationData>();
    public List<GameObject> SkillPool = new List<GameObject>();

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void Start()
    {
        SetSkill("skill_fireball01");
    }

    public void SetSkill(string skillID)
    {
        UseSkillData.Add(SkillModel.I.skillInformation.dataArray.Where(x => x.Key == skillID).FirstOrDefault());

        for (int i = 0; i < CommonValueData.I.PoolSkillAmount; i++)
        {
            SkillPoolLoad("Skill/" + UseSkillData.Find(x => x.Key == skillID).Prefabid);
        }
        StartCoroutine(FireSkill(skillID));
    }

    private GameObject SkillPoolLoad(string path)
    {
        GameObject spawnSkill = (GameObject)Instantiate(Resources.Load(path), SkillSpawnPool);
        spawnSkill.SetActive(false);
        SkillPool.Add(spawnSkill);

        return spawnSkill;
    }

    private void SkillSpawn(string skillID)
    {
        string prefabID = UseSkillData.Find(x => x.Key == skillID).Prefabid;

        GameObject spawnSkill = SkillPool.Find(x => x.gameObject.name == prefabID + "(Clone)" && !x.activeSelf);

        if (spawnSkill == null)
        {
            spawnSkill = SkillPoolLoad("Skill/" + prefabID);

            Debug.Log("Pool is Full. Make CommonValueData.PoolSkillAmount More Bigger!");
        }

        spawnSkill.SetActive(true);
    }

    IEnumerator FireSkill(string skillID)
    {
        SkillConditionData currentSkillConditionData = SkillModel.I.skillCondition.dataArray.Where
                (x => x.Key == UseSkillData.Find(y => y.Key == skillID).Conditionkey).FirstOrDefault();

        float cooltime = currentSkillConditionData.Cooltime;
        float ratio = currentSkillConditionData.Ratio;
        bool projectileCheck = currentSkillConditionData.Projectilecheck;

        while (true)
        {
            if(!(Random.Range(0,1) < ratio)) { continue; }
            if(projectileCheck && SkillPool.Where(x => x.activeSelf).Any()) { continue; }

            if(MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).Any())
            { 
                SkillSpawn(skillID);
            }

            yield return new WaitForSeconds(cooltime);
        }
    }
}
