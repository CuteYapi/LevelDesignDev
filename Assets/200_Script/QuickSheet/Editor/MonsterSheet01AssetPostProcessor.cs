using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class MonsterSheet01AssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/300_Data/ExcelData/MonsterDataTable.xlsx";
    private static readonly string assetFilePath = "Assets/300_Data/ExcelData/MonsterSheet01.asset";
    private static readonly string sheetName = "MonsterSheet01";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            MonsterSheet01 data = (MonsterSheet01)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(MonsterSheet01));
            if (data == null) {
                data = ScriptableObject.CreateInstance<MonsterSheet01> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<MonsterSheet01Data>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<MonsterSheet01Data>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
