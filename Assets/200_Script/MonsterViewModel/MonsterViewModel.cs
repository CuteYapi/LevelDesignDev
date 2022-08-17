using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterViewModel : MonoBehaviour
{
    public static MonsterViewModel I { get; set; }
    [SerializeField] private Transform MonsterSpawnPool;

    public List<GameObject> MonsterPool;

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

        //최초 몬스터 생성
        for (int i = 0; i < StageModel.I.TimeTrigger.Count; i++)
        {
            MonsterSpawn(MonsterModel.I.MonsterSheet01.dataArray.Where(x=> x.ID == StageModel.I.TimeTrigger[i].Monsterid).FirstOrDefault().Prefabid
                , Random.insideUnitCircle.normalized * CommonValueData.I.SpawnCircleDistance);
        }
    }

    GameObject MonsterPoolLoad(string path)
    {
        GameObject spawnMonster = (GameObject)Instantiate(Resources.Load(path),
                                                            MonsterSpawnPool);

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
