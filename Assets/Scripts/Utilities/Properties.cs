using System.IO;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// App Properties
    /// </summary>
    public class Properties
    {
        public class PlayerPrefsKeys
        {
            public static string QualitySetting => "QualitySetting";
        }

        /// <summary>
        /// Nome dos arquivos gerados pelo app
        /// </summary>
        public class FilesNames
        {
            /// <summary>
            /// Database file name
            /// </summary>
            public static string DatabaseName => "classroom.s3db";

            /// <summary>
            /// Project Settings path
            /// </summary>
            public static string ProjectSettings => "ProjectSettings/ProjectSettings.asset";
        }

        /// <summary>
        /// Streaming Assets database path
        /// </summary>
        public static string StreamingAssetsDatabasePath => Path.Combine(BetterStreamingAssets.Root, FilesNames.DatabaseName);

        /// <summary>
        /// Local database path for Others for reading
        /// </summary>
        public static string TargetDatabasePath => Path.Combine(Application.persistentDataPath, FilesNames.DatabaseName);

        /// <summary>
        /// Local database path for Others for reading
        /// </summary>
        public static string DatabasePath => Path.Combine("URI=file:", TargetDatabasePath);

        /// <summary>
        /// Save progress path
        /// </summary>
        public static string ProgressPath => Path.Combine(Application.persistentDataPath, "prog.dat");

        /// <summary>
        /// Database Password
        /// </summary>
        public static string DatabasePassword => "q3t6w9z$C&E)H@McQfTjWnZr4u7x!A%D";
    }
}