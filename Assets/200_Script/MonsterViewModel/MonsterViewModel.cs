using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterViewModel : MonoBehaviour
{
    public static MonsterViewModel I { get; set; }
    [SerializeField] private Transform MonsterSpawnPool;

    public List<GameObject> MonsterPool;

    private int TimeTriggerCount = 0;
    private int ScoreTriggerCount = 0;
    private int KillCountTriggerCount = 0;

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
        //오브젝트 풀 장전
        for (int i = 0; i < MonsterModel.I.MonsterSheet01.dataArray.Length; i++)
        {
            for (int j = 0; j < CommonValueData.I.PoolMonsterAmount; j++)
            {
                MonsterPoolLoad("Monster/" + MonsterModel.I.MonsterSheet01.dataArray[i].Prefabid);
            }
        }
    }

    private void Update()
    {
        if (TimeTriggerCount < StageModel.I.TimeTrigger.Count && StageModel.I.TimeTrigger[TimeTriggerCount].Condition <= CommonValueData.I.CurrentTime)
        {
            MonsterSpawn(MonsterModel.I.MonsterSheet01.dataArray.Where(x => x.ID == StageModel.I.TimeTrigger[TimeTriggerCount].Monsterid).FirstOrDefault().Prefabid
                , (Vector2)PlayerCharacterViewModel.I.transform.localPosition + Random.insideUnitCircle.normalized * CommonValueData.I.SpawnCircleDistance) ;

            TimeTriggerCount++;
        }

        if (ScoreTriggerCount < StageModel.I.ScoreTrigger.Count && StageModel.I.ScoreTrigger[ScoreTriggerCount].Condition <= CommonValueData.I.CurrentScore)
        {
            MonsterSpawn(MonsterModel.I.MonsterSheet01.dataArray.Where(x => x.ID == StageModel.I.ScoreTrigger[ScoreTriggerCount].Monsterid).FirstOrDefault().Prefabid
                , (Vector2)PlayerCharacterViewModel.I.transform.localPosition + Random.insideUnitCircle.normalized * CommonValueData.I.SpawnCircleDistance);

            ScoreTriggerCount++;
        }

        if (KillCountTriggerCount < StageModel.I.KillCountTrigger.Count && StageModel.I.KillCountTrigger[KillCountTriggerCount].Condition <= CommonValueData.I.CurrentKillCount)
        {
            MonsterSpawn(MonsterModel.I.MonsterSheet01.dataArray.Where(x => x.ID == StageModel.I.KillCountTrigger[KillCountTriggerCount].Monsterid).FirstOrDefault().Prefabid
                , (Vector2)PlayerCharacterViewModel.I.transform.localPosition + Random.insideUnitCircle.normalized * CommonValueData.I.SpawnCircleDistance);

            KillCountTriggerCount++;
        }
    }

    GameObject MonsterPoolLoad(string path)
    {
        GameObject spawnMonster = (GameObject)Instantiate(Resources.Load(path), MonsterSpawnPool);

        spawnMonster.SetActive(false);
        MonsterPool.Add(spawnMonster);

        return spawnMonster;
    }

    void MonsterSpawn(string MonsterID, Vector3 spawnPosition)
    {
        GameObject spawnMonster = MonsterPool.Find(x => x.gameObject.name == MonsterID + "(Clone)" && !x.activeSelf);

        if(spawnMonster == null)
        {
            spawnMonster = MonsterPoolLoad("Character/" + MonsterID);

            Debug.Log("Pool is Full. Make CommonValueData.PoolMonsterAmount More Bigger!");
        }

        spawnMonster.transform.position = spawnPosition;
        spawnMonster.SetActive(true);
    }
}
