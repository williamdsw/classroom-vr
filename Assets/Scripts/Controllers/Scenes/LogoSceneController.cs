using Controllers.Core;
using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace Controllers.Scenes
{
    public class LogoSceneController : MonoBehaviour
    {
        [Header("Required Elements")]
        [SerializeField] private GameObject fadeInObject;
        [SerializeField] private GameObject fadeOutObject;

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
            yield return new WaitForSecondsRealtime(1f);
            fadeInObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            SceneManagerController.CallSceneAsync(SceneManagerController.SceneNames.Classroom);
        }

        /// <summary>
        /// Extract database file
        /// </summary>
        private IEnumerator ExtractDatabase()
        {
            if (!FileManager.Exists(Properties.TargetDatabasePath))
            {
                if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
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