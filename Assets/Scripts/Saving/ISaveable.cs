namespace RPG.Saving
{
    /// <summary>
    /// 用于任何具有要保存/恢复状态的组件中
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// 保存时调用以捕获组件的状态。
        /// </summary>
        /// <returns>
        /// 返回一个表示组件状态的 `System.Serializable` object。
        /// </returns>
        object CaptureState();
        /// <summary>
        /// 恢复场景状态时调用。
        /// </summary>
        /// <param name="state">
        /// 与保存时调用 CaptureState() 返回的 `System.Serializable` object 为同一个
        /// </param>
        void RestoreState(object state);
    }

}