using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BackgroundViewModel : MonoBehaviour
{
    public static BackgroundViewModel I { get; set; }

    public List<GameObject> BackgroundView;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(I.gameObject);
        }
        I = this;
    }

#if UNITY_EDITOR
    private void Start()
    {
        StringBuilder apkName = new StringBuilder("Test_");
        apkName.Append($"{PlayerSettings.bundleVersion}_");
        apkName.Append($"{DateTime.Now.Year.ToString("0000")}.");
        apkName.Append($"{DateTime.Now.Month.ToString("00")}.");
        apkName.Append($"{DateTime.Now.Day.ToString("00")}.");
        apkName.Append($"{DateTime.Now.Hour.ToString("00")}.");
        apkName.Append($"{DateTime.Now.Minute.ToString("00")}.");
        apkName.Append($"{DateTime.Now.Second.ToString("00")}");
        apkName.Append(".apk");
        
        Debug.Log(apkName.ToString());
    }
#endif
}
