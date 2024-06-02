using Chinook.ClientServices.Interfaces;
using Microsoft.AspNetCore.Components;
using System;

namespace Chinook.ClientServices
{
    public class AppState : IAppState
    {
        /// <summary>
        /// This event will be triggered when the state of the application changes.
        /// </summary>
        public event Action OnChange;

        /// <summary>
        /// This method is responsible for notifying subscribers when the state of the application changes.
        /// </summary>
        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
