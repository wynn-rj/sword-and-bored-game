namespace SwordAndBored.Utilities
{
    public interface IObserver<T>
    {
        /// <summary>
        /// Adds the subscriber to the observer's list of objects to be notified
        /// </summary>
        /// <param name="subscriber">The object that is subscribing to the observer</param>
        void Subscribe(T subscriber);

        /// <summary>
        /// Attempts to remove the subscriber from the list of subscribed objects
        /// </summary>
        /// <param name="subscriber">The object to remove</param>
        /// <returns>Whether the object was successfully removed</returns>
        bool Unsubscribe(T subscriber);
    }

    public interface IObserver
    {
        /// <summary>
        /// Adds the subscriber to the observer's list of objects to be notified
        /// </summary>
        /// <param name="subscriber">The object that is subscribing to the observer</param>
        void Subscribe(object subscriber);

        /// <summary>
        /// Attempts to remove the subscriber from the list of subscribed objects
        /// </summary>
        /// <param name="subscriber">The object to remove</param>
        /// <returns>Whether the object was successfully removed</returns>
        bool Unsubscribe(object subscriber);
    }
}
