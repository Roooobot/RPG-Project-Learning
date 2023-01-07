using UnityEngine;

namespace RPG.Core
{
    //��Ϊ���ȣ����ڲ�ͬ��Ϊ֮��
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        //ִ���µ���Ϊ,����ͬ�򲻱�
        public void StartAction(IAction action)
        {
            if (currentAction == action)
                return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }
        //����Ϊ�ÿգ���ȡ����ǰ��Ϊ
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
