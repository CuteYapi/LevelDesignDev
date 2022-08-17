using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel : MonoBehaviour
{
    public static SkillModel I { get; set; }

    public SkillData SkillData;
    public PresetData PresetData;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
