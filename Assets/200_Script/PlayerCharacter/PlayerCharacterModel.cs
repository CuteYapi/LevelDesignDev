using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCharacterModel : MonoBehaviour
{
    public static PlayerCharacterModel I { get; private set; }

    public float characterMoveSpeed;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
