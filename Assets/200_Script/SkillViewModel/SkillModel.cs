using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel : MonoBehaviour
{
    public static SkillModel I { get; set; }

    public SkillInformation skillInformation;
    public SkillFX skillFX;
    public SkillCondition skillCondition;
    public SkillShoot skillShoot;
    public SkillEffect skillEffect;
    public SkillFollow skillFollow;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }
}
