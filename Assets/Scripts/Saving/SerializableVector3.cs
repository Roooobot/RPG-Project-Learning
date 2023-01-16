using UnityEngine;

namespace RPG.Saving
{
    /// <summary>
    /// `Vector3` 类的 `System.Serializable` 包装器。
    /// </summary>
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;
        /// <summary>
        /// 从现有 Vector3 复制状态。
        /// </summary>
        public SerializableVector3(Vector3 vector)
        {
            x=vector.x;
            y=vector.y;
            z=vector.z;
        }
        /// <summary>
        /// 从此类的状态创建一个 Vector3。
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }



    }

}