using Controllers.Core;
using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Controllers.Scenes
{
    public class LogoSceneController : MonoBehaviour
    {
        // State

        private bool isDatabaseOk = false;

        private void Awake()
        {
            UnityUtilities.DisableAnalytics();
            BetterStreamingAssets.Initialize();
        }

        private IEnumerator Start()
        {
            yield return ExtractDatabase();
            yield return new WaitUntil(() => isDatabaseOk);
            SceneManagerController.CallSceneAsync(SceneManagerController.SceneNames.Classroom);
        }

        /// <summary>
        /// Extract database file
        /// </summary>
        private IEnumerator ExtractDatabase()
        {
            if (!FileManager.Exists(Properties.TargetDatabasePath))
            {
                if (Application.isEditor)
                {
                    FileManager.Copy(Properties.StreamingAssetsDatabasePath, Properties.TargetDatabasePath);
                }
                else
                {
                    yield return CopyDatabase();
                }
            }

            isDatabaseOk = true;
            yield return null;
        }

        /// <summary>
        /// Copy database from streaming assets to app path
        /// </summary>
        private IEnumerator CopyDatabase()
        {
            try
            {
                byte[] bytes = FileManager.ReadAllBytesFromStreamingAssets(Properties.FilesNames.DatabaseName);
                if (bytes != null && bytes.Length != 0)
                {
                    FileManager.WriteAllBytes(Properties.TargetDatabasePath, bytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            yield return null;
        }
    }
}