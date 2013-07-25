using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AndrewScott.SettingsSerializer
{
    /// <summary>
    /// Represents a set of settings that will be stored and retrieved.
    /// </summary>
    [Serializable]
    public class SettingsData
    {
        /// <summary>
        /// A dictionary that contains objects that will be serialized and deserialized.
        /// </summary>
        public Dictionary<string, object> Settings = new Dictionary<string, object>();
    }

    /// <summary>
    /// Manages reading, writing, serialization, and deserialization of a set of settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Reference to the <c>SettingsData</c> object that this <c>Settings</c> is handling.
        /// </summary>
        public SettingsData Data;

        /// <summary>
        /// The name and path of the file that will be written to and read from.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// Retrieves a setting.
        /// </summary>
        /// <param name="key">The key of the requested setting</param>
        /// <returns>The requested object instance of the setting, or null if it was not found.</returns>
        public object GetSetting(string key)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Settings cannot be queried before the settings have been loaded");

            if (Data.Settings.ContainsKey(key))
                return Data.Settings[key];
            else
                return null;
        }

        /// <summary>
        /// Sets a setting.
        /// </summary>
        /// <param name="key">The key of the setting</param>
        /// <param name="setting">The value of the setting</param>
        public void SetSetting(string key, object setting)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Settings cannot be set before they have been loaded");

            Data.Settings[key] = setting;
        }

        /// <summary>
        /// A delegate that will be used for events that communicate data about read/write actions.
        /// </summary>
        /// <param name="Data"></param>
        public delegate void SettingsEventHandler(SettingsData Data);

        /// <summary>
        /// Event that will be fired when the managed file is read from and loaded into this <c>Settings</c>
        /// </summary>
        public event SettingsEventHandler ReadFile;

        /// <summary>
        /// Event that will be fired when the settings managed by this <c>Settings</c> are written to its file.
        /// </summary>
        public event SettingsEventHandler WriteFile;

        /// <summary>
        /// True if the settings for this <c>Settings</c> have been read from their file since it was created.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Creates a new <c>Settings</c> to manage a file and a set of data.
        /// </summary>
        /// <param name="fileName">The name (and path) of the file that will be read to and written from. </param>
        /// <param name="readNow"><c>True</c> if the file should be read immediately. 
        ///     <c>False</c> if the file will be read manually later using <c>Settings.ReadFromFile()</c></param>
        public Settings(string fileName, bool readNow)
        {
            FileName = fileName;

            if (readNow) ReadFromFile();
        }


        /// <summary>
        /// Writes the settings being managed by this <c>XMLSettings</c> to its file.
        /// </summary>
        public void WriteToFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Data);
            stream.Close();

            if (WriteFile != null) WriteFile(Data);
        }

        /// <summary>
        /// Reads the settings being managed by this <c>XMLSettings</c> from its file.
        /// </summary>
        public void ReadFromFile()
        {

            FileInfo fi = new FileInfo(FileName);
            if (!fi.Directory.Exists)
                System.IO.Directory.CreateDirectory(fi.DirectoryName);


            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            if (stream.Length == 0)
                Data = new SettingsData();
            else
                Data = (SettingsData)formatter.Deserialize(stream);

            stream.Close();

            IsLoaded = true;

            if (ReadFile != null) ReadFile(Data);
        }

        /// <summary>
        /// Declares a default setting
        /// </summary>
        /// <param name="key">The key of the setting as it exists in <c>SettingsData.Settings</c></param>
        /// <param name="initializer">A delegate that will initialize an instance of the setting class if it doesn't exist.</param>
        public void DeclareDefault(string key, Func<object> initializer)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Defaults cannot be declared until the settings have been loaded");

            if (!Data.Settings.ContainsKey(key))
                Data.Settings[key] = initializer();
        }

        /// <summary>
        /// Declares a default setting
        /// </summary>
        /// <param name="key">The key of the setting as it exists in <c>SettingsData.Settings</c></param>
        /// <param name="defaultValue">A value type that represents the default value for the setting in case it doesn't exist in the settings.</param>
        public void DeclareDefault(string key, ValueType defaultValue)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Defaults cannot be declared until the settings have been loaded");

            if (!Data.Settings.ContainsKey(key))
                Data.Settings[key] = defaultValue;
        }

        /// <summary>
        /// Declares a default setting
        /// </summary>
        /// <param name="key">The key of the setting as it exists in <c>SettingsData.Settings</c></param>
        /// <param name="defaultValue">A string that represents the default value for the setting in case it doesn't exist in the settings.</param>
        public void DeclareDefault(string key, string defaultValue)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Defaults cannot be declared until the settings have been loaded");

            if (!Data.Settings.ContainsKey(key))
                Data.Settings[key] = defaultValue;
        }

    }
}
