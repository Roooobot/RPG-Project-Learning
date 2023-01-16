using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    /// <summary>
    /// 该组件提供与保存系统的接口。 
    /// 提供了保存和恢复场景的方法。
    /// 
    /// 该组件应只创建一次并在所有后续场景之间共享。
    /// </summary>
    public class SavingSystem : MonoBehaviour
    {
        /// <summary>
        /// 将加载最后一次保存的场景并恢复状态。 
        /// 这必须作为协程运行。
        /// </summary>
        /// <param name="saveFile">
        /// 用于加载的保存文件。
        /// </param>
        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (state.ContainsKey("lastSceneBuildIndex"))
            {
                buildIndex = (int)state["lastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(state);
        }
        /// <summary>
        /// 将当前场景保存到提供的保存文件中。
        /// </summary>
        /// <param name="saveFile"></param>
        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }
        /// <summary>
        /// 删除给定保存文件中的状态。
        /// </summary>
        /// <param name="saveFile"></param>
        public void Delete(string saveFile)
        {
            File.Delete(GetPathFromSaveFile(saveFile));
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print(path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new();
                formatter.Serialize(stream, state);
            }

        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }

        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state["lastSceneBuildIndex"]=SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath , saveFile + ".sav");
        }

    }

}