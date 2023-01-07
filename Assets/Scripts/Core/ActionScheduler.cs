using UnityEngine;

namespace RPG.Core
{
    //行为调度，用于不同行为之间
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        //执行新的行为,若相同则不变
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
        //将行为置空，即取消当前行为
        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
