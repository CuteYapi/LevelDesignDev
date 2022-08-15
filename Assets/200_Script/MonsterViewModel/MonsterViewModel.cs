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
        for (int i = 0; i < MonsterModel.I.MonsterSheet01.dataArray.Length; i++)
        {
            for (int j = 0; j < CommonValueData.I.PoolMonsterAmount; j++)
            {
                ObjectPoolLoad("Character/" + MonsterModel.I.MonsterSheet01.dataArray[i].Prefabid);
            }
        }

        for (int i = 0; i < StageModel.I.TimeTrigger.Count; i++)
        {
            ObjectSpawn(MonsterModel.I.MonsterSheet01.dataArray.Where(x=> x.ID == StageModel.I.TimeTrigger[i].Monsterid).FirstOrDefault().Prefabid
                , Random.insideUnitCircle.normalized * CommonValueData.I.SpawnCircleDistance);
        }
    }

    GameObject ObjectPoolLoad(string path)
    {
        GameObject spawnMonster = (GameObject)Instantiate(Resources.Load(path),
                                                            MonsterSpawnPool);

        spawnMonster.SetActive(false);
        MonsterPool.Add(spawnMonster);

        return spawnMonster;
    }

    void ObjectSpawn(string objectID, Vector3 spawnPosition)
    {
        GameObject spawnObject = MonsterPool.Find(x => x.gameObject.name == objectID + "(Clone)" && !x.activeSelf);

        if(spawnObject == null)
        {
            spawnObject = ObjectPoolLoad("Character/" + objectID);

            Debug.Log("Pool is Full. Make CommonValueData.PoolMonsterAmount More Bigger!");
        }

        spawnObject.transform.position = spawnPosition;
        spawnObject.SetActive(true);
    }
}
