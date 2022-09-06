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

        StartCoroutine(MissileSelfDestroy());
    }

    private void FixedUpdate()
    {
        if (!(skillData.Headlocation == "float_circle"))
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

        else if (skillData.Startlocation == "me_circle")
        {
            Debug.LogAssertion("Startlocation 'me_circle' 구현 필요");
            return PlayerCharacterViewModel.I.transform.localPosition;
        }

        else
        {
            Debug.LogError("Startlocation Error : " + skillData.Startlocation);
            return PlayerCharacterViewModel.I.transform.localPosition;
        }
    }

    private Vector2 SetTargetPosition(SkillShootData skillData)
    {

        if (skillData.Headlocation == "direction_random")
        {
            List<GameObject> monsterList = MonsterViewModel.I.MonsterPool.Where(x => x.activeSelf).ToList();
            return monsterList[Random.Range(0, monsterList.Count - 1)].transform.position;
        }

        else
        {
            Debug.LogError("Startlocation Error : " + skillData.Startlocation);
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private Vector2 Ballistic_Line(Vector2 currentPosition, Vector2 targetPosition)
    {
        currentPosition = currentPosition + (targetPosition - startPosition).normalized
            * skillData.Shootspeed;

        return currentPosition;
    }

    private IEnumerator MissileSelfDestroy()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter()
    {
        gameObject.SetActive(false);
    }
}
