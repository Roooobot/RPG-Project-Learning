using RPG.Core;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    /// <summary>
    /// 添加在任何具有需要保存的 ISaveable 组件的游戏对象上。
    /// 
    /// 这个类为场景文件中的 GameObject 提供了唯一的 ID。 
    /// ID 用于保存和恢复与此 GameObject 相关的状态。 
    /// 可以手动覆盖此 ID 以将不同场景之间的游戏对象链接（例如重复出现的角色、玩家或记分板）。 
    /// 注意不要在预制件中设置它，除非你想将不同场景之间的所有实例都链接。
    /// </summary>
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [Tooltip("如果留空，场景中的实例会自动生成唯一 ID。 "+
            "除非你希望所有实例都具有相同的ID，否则不要在预制件中设置。")]
        [SerializeField] string uniqueIdentifier = "";
        //一个静态字典集，用于存储不同组件的状态
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }
        /// <summary>
        /// 将捕获所有实现了“ISaveables”的组件的状态，并返回一个 `System.Serializable` object ，稍后可以恢复此状态。
        /// </summary>
        public object CaptureState()
        {
            Dictionary<string,object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()]=saveable.CaptureState();
            }
            return state;
        }
        /// <summary>
        /// 将恢复由 CaptureState() 捕获的状态。
        /// </summary>
        /// <param name="state">
        /// 由 CaptureState() 返回的同一个 object
        /// The same object that was returned by `CaptureState`.
        /// </param>
        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject))  return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");


            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue=System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }
#endif
        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;

            if (globalLookup[candidate] == this) return true;

            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
    }

}