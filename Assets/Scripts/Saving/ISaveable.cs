namespace RPG.Saving
{
    /// <summary>
    /// �����κξ���Ҫ����/�ָ�״̬�������
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// ����ʱ�����Բ��������״̬��
        /// </summary>
        /// <returns>
        /// ����һ����ʾ���״̬�� `System.Serializable` object��
        /// </returns>
        object CaptureState();
        /// <summary>
        /// �ָ�����״̬ʱ���á�
        /// </summary>
        /// <param name="state">
        /// �뱣��ʱ���� CaptureState() ���ص� `System.Serializable` object Ϊͬһ��
        /// </param>
        void RestoreState(object state);
    }

}