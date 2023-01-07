using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        //跟随的玩家的位置
        [SerializeField] Transform target;

        void LateUpdate()
        {
            //跟随玩家
            transform.position = target.position;
        }
    }

}