using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace API
{
    /// <summary>
    /// Base API handler class
    /// </summary>
    public class BaseAPI
    {
        /// <summary>
        /// Get request data at path
        /// </summary>
        /// <param name="path"> Desired path </param>
        /// <param name="callback"> Callback with data bytes </param>
        public static IEnumerator Get(string path, Action<byte[]> callback)
        {
            Debug.LogFormat("Path: {0}", path);
            using (UnityWebRequest request = UnityWebRequest.Get(path))
            {
                yield return request.SendWebRequest();

                try
                {
                    Debug.LogError("request.error: " + request.error);
                    if (string.IsNullOrEmpty(request.error))
                    {
                        callback(request.downloadHandler.data);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                    callback(null);
                }
            }
        }
    }
}