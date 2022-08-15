using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    public static MonsterModel I { get; set; }

    public MonsterSheet01 MonsterSheet01;

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
