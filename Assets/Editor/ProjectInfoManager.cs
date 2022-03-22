
using System;
using UnityEditor;
using Utilities;

public class ProjectEditorManager
{
    /// <summary>
    /// Get project info to update editor values
    /// </summary>
    [MenuItem("Project/Update Properties", false, 1)]
    protected static void UpdateProperties()
    {
        try
        {
            SerializedObject playerSettingsManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath(Properties.FilesNames.ProjectSettings)[0]);
            SerializedProperty productName = playerSettingsManager.FindProperty("productName");
            SerializedProperty bundleVersion = playerSettingsManager.FindProperty("bundleVersion");
            SerializedProperty androidBundleVersionCode = playerSettingsManager.FindProperty("AndroidBundleVersionCode");

            PlayerSettings.companyName = ProjectInfo.CompanyName;
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, ProjectInfo.Identifier);

            productName.stringValue = ProjectInfo.ProductName;
            bundleVersion.stringValue = ProjectInfo.BundleVersion;
            androidBundleVersionCode.intValue = ProjectInfo.BundleVersionCode;

            playerSettingsManager.ApplyModifiedProperties();
            playerSettingsManager.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}