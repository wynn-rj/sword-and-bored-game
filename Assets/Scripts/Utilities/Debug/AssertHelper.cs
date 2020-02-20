using System;
using System.Diagnostics;

namespace SwordAndBored.Utilities.Debug
{
    static class AssertHelper
    {
        /// <summary>
        /// Checks to make sure an object has been set in the editor on debug builds
        /// </summary>
        /// <typeparam name="T">The type of object that is being checked</typeparam>
        /// <param name="obj">The object that is being checked</param>
        /// <param name="context">The unity object where the object being checked should have been set</param>
        [Conditional("DEBUG")]
        public static void IsSetInEditor<T>(T obj, UnityEngine.Object context) where T : class
        {
            if (obj is null)
            {
                UnityEngine.Debug.LogError(typeof(T).Name + " is not set for " + context.GetType().Name, context);
            }
        }
    }
}
