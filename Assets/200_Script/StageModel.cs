using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageModel : MonoBehaviour
{
    public static StageModel I { get; set; }

    public Stage01 Stage01;

    public List<Stage01Data> TimeTrigger = new List<Stage01Data>();
    public List<Stage01Data> ScoreTrigger = new List<Stage01Data>();
    public List<Stage01Data> KillCountTrigger = new List<Stage01Data>();

    private void Awake()
    {
        if(I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

    private void Start()
    {
        TimeTrigger = Stage01.dataArray.Where(x => x.Trigger == "Time").ToList();
        ScoreTrigger = Stage01.dataArray.Where(x => x.Trigger == "Score").ToList();
        KillCountTrigger = Stage01.dataArray.Where(x => x.Trigger == "KillCount").ToList();
    }
}
