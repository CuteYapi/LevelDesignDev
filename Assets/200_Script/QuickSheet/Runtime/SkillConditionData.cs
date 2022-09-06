using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class SkillConditionData
{
  [SerializeField]
  string key;
  public string Key { get {return key; } set { key = value;} }
  
  [SerializeField]
  float cooltime;
  public float Cooltime { get {return cooltime; } set { cooltime = value;} }
  
  [SerializeField]
  float ratio;
  public float Ratio { get {return ratio; } set { ratio = value;} }
  
  [SerializeField]
  bool projectilecheck;
  public bool Projectilecheck { get {return projectilecheck; } set { projectilecheck = value;} }
  
}