using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildPlayer : MonoBehaviour
{
    [MenuItem("Build/Build AOS(Debug)")]
    public static void Build_AOS_Debug()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        buildPlayerOptions.scenes = FindEnabledEditorScenes();
        buildPlayerOptions.locationPathName = $"C:/Users/Client/Desktop/BuildTest/{SetApkName()}.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.Development;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build Succeeded : " + summary.totalSize + " bytes");
        }

        else if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build Failed");
        }
        
    }

    private static string SetApkName()
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
        
        return apkName.ToString();
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> editorScenes = new List<string>();

        // foreach (var scene in EditorBuildSettings.scenes)
        // {
        //     if (!scene.enabled)
        //     {
        //         continue;
        //     }
        //     editorScenes.Add(scene.path);
        // }

        editorScenes.Add("Assets/000_Scenes/SkillMakeTest.unity");
        return editorScenes.ToArray();
    }
}
