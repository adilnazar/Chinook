namespace Chinook.ClientServices.Interfaces
{
    public interface IAppState
    {
        /// <summary>
        /// This event will be triggered when the state of the application changes.
        /// </summary>
        event Action OnChange;

        /// <summary>
        /// This method is responsible for notifying subscribers when the state of the application changes.
        /// </summary>
        void NotifyStateChanged();
    }
}
