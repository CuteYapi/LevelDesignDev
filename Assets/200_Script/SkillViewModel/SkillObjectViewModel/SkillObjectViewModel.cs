using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillObjectViewModel : MonoBehaviour
{
    private string objectName;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private SkillDataData skillData;
    private PresetDataData presetData;

    private void OnEnable()
    {
        //(Clone) 이라는 String을 제거하기 위해 뒤에서부터 7글자 제거
        objectName = gameObject.name.Remove(gameObject.name.Length - 7, 7);

        skillData = SkillViewModel.I.UseSkillData.Find(x => x.Prefabid == objectName);
        presetData = SkillModel.I.PresetData.dataArray.Where(x => x.Presetid == skillData.Presetid).FirstOrDefault();

        startPosition = SetStartPosition(presetData);
        targetPosition = SetTargetPosition(presetData);
        transform.localPosition = startPosition;

        StartCoroutine(MissileSelfDestroy());
    }

    private void FixedUpdate()
    {
        if (presetData.Ballistictype == "Line")
        {
            transform.localPosition = Ballistic_Line(transform.localPosition, targetPosition);
        }
    }

    private Vector2 SetStartPosition(PresetDataData presetData)
    {
        if (presetData.Startposition == "Player")
        {
            return PlayerCharacterViewModel.I.transform.localPosition;
        }

        else
        {
            //추후 데이터 테이블에 따른 미사일 시작 지점 개발할 것
            return PlayerCharacterViewModel.I.transform.localPosition;
        }
    }

    private Vector2 SetTargetPosition(PresetDataData presetData)
    {
        if (!presetData.Targetting)
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        else
        {
            //추후 타케팅 시스템 개발할 것
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private Vector2 Ballistic_Line(Vector2 currentPosition, Vector2 targetPosition)
    {
        currentPosition = currentPosition + (targetPosition - startPosition).normalized
            * skillData.Missilespeed;

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
