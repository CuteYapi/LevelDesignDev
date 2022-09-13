using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillObjectViewModel : MonoBehaviour
{
    private string objectName;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private SkillShootData skillData;

    private void OnEnable()
    {
        //(Clone) 이라는 String을 제거하기 위해 뒤에서부터 7글자 제거
        objectName = gameObject.name.Remove(gameObject.name.Length - 7, 7);

        skillData = SkillModel.I.skillShoot.dataArray.Where
            (x => x.Key == SkillViewModel.I.UseSkillData.Find(y => y.Prefabid == objectName).Shootkey).FirstOrDefault();

        startPosition = SetStartPosition(skillData);
        targetPosition = SetTargetPosition(skillData);
        transform.localPosition = startPosition;

        if (skillData.Headlocation == "direction_head" || skillData.Headlocation == "direction_tail")
        {
            startPosition = new Vector3(0, 0, 0);
        }

        StartCoroutine(SkillSelfDestroy());
    }

    private void FixedUpdate()
    {
        if (skillData.Headlocation == "float_circle")
        {
            transform.RotateAround(PlayerCharacterViewModel.I.transform.localPosition, Vector3.forward, skillData.Shootspeed);
        }

        else
        {
            transform.localPosition = Ballistic_Line(transform.localPosition, targetPosition);
        }
    }

    private Vector2 SetStartPosition(SkillShootData skillData)
    {
        if (skillData.Startlocation == "me")
        {
            return PlayerCharacterViewModel.I.transform.localPosition;
        }

        else if (skillData.Startlocation == "target_near")
        {
            GameObject targetMonster = MonsterViewModel.I.MonsterPool.
                Where(x => x.activeSelf).
                OrderBy(x => (x.transform.localPosition - PlayerCharacterViewModel.I.transform.localPosition).sqrMagnitude).FirstOrDefault();

            return targetMonster.transform.position;
        }

        else if (skillData.Startlocation == "target_random")
        {
            Debug.Log("target_random과 direction_random 구분 필요");

            List<GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else if (skillData.Startlocation == "me_circle")
        {
            transform.parent = PlayerCharacterViewModel.I.transform;
            return Random.insideUnitCircle.normalized * skillData.Shootrange;
        }

        else if (skillData.Startlocation == "me_random")
        {
            return Random.insideUnitCircle.normalized * skillData.Shootrange;
        }

        else
        {
            Debug.LogError("Startlocation Error : " + skillData.Startlocation);
            return PlayerCharacterViewModel.I.transform.localPosition;
        }
    }

    private Vector2 SetTargetPosition(SkillShootData skillData)
    {
        if (skillData.Headlocation == "me")
        {
            return PlayerCharacterViewModel.I.transform.localPosition;
        }

        else if (skillData.Headlocation == "target_near")
        {
            GameObject targetMonster = MonsterViewModel.I.MonsterPool.
                Where(x => x.activeSelf).
                OrderBy(x => (x.transform.localPosition - PlayerCharacterViewModel.I.transform.localPosition).sqrMagnitude).FirstOrDefault();

            return targetMonster.transform.position;
        }

        else if (skillData.Headlocation == "target_random")
        {
            Debug.Log("target_random과 direction_random 구분 필요");

            List <GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else if (skillData.Headlocation == "direction_random")
        {
            List<GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else if (skillData.Headlocation == "direction_head")
        {
            return PlayerCharacterViewModel.I.playerDirection * -1;
        }

        else if (skillData.Headlocation == "direction_tail")
        {
            return PlayerCharacterViewModel.I.playerDirection;
        }

        else if (skillData.Headlocation == "direction_cross")
        {
            Debug.Log(skillData.Headlocation + " 구현 필요");

            List<GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else if (skillData.Headlocation == "direction_x")
        {
            Debug.Log(skillData.Headlocation + " 구현 필요");

            List<GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else if (skillData.Headlocation == "float_circle")
        {
            return PlayerCharacterViewModel.I.transform.localPosition;
        }

        else
        {
            Debug.LogError("Startlocation Error : " + skillData.Startlocation);
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private Vector2 Ballistic_Line(Vector2 currentPosition, Vector2 targetPosition)
    {
        currentPosition = currentPosition + (targetPosition - startPosition).normalized * skillData.Shootspeed;

        return currentPosition;
    }

    private IEnumerator SkillSelfDestroy()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter()
    {
        if (transform.parent != SkillViewModel.I.SkillSpawnPool)
        {
            transform.parent = SkillViewModel.I.SkillSpawnPool;
        }
        gameObject.SetActive(false);
    }
}
