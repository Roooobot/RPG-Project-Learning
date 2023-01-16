using UnityEngine;

namespace RPG.Saving
{
    /// <summary>
    /// `Vector3` ��� `System.Serializable` ��װ����
    /// </summary>
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;
        /// <summary>
        /// ������ Vector3 ����״̬��
        /// </summary>
        public SerializableVector3(Vector3 vector)
        {
            x=vector.x;
            y=vector.y;
            z=vector.z;
        }
        /// <summary>
        /// �Ӵ����״̬����һ�� Vector3��
        /// </summary>
        /// <returns></returns>
        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }



    }

}