﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterCharacterViewModel : MonoBehaviour
{
    private string objectName;

    private void Start()
    {
        //(Clone) 이라는 String을 제거하기 위해 뒤에서부터 7글자 제거
        objectName = gameObject.name.Remove(gameObject.name.Length - 7, 7);
    }

    private void FixedUpdate()
    {
        transform.localPosition = transform.localPosition + 
            (PlayerCharacterViewModel.I.transform.localPosition
            - transform.localPosition).normalized * MonsterModel.I.MonsterSheet01.dataArray.Where(x => x.Prefabid == objectName).FirstOrDefault().Movespeed;
    }

    private void OnTriggerEnter()
    {
        gameObject.SetActive(false);
    }
}
