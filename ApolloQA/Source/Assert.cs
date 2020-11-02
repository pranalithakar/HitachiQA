using System;
using System.Collections.Generic;
using System.Text;
using ApolloQA.Source.Helpers;
namespace ApolloQA
{
    class Assert
    {

        public static bool SoftAreEqual(object A, object B)
        {
            return AreEqual(A, B, true);
        }
        public static bool AreEqual(object A, object B, bool optional = false)
        {
            if (A.Equals(B))
            {
                success($"Object {A} equals {B}");
                return true;
            }
            else
            {
                Functions.handleFailure(new Exception($"Assert - type:[{A?.GetType()?.FullName}] value:[${A}]   does not Equal  type: [{A?.GetType()?.FullName}] value: [{B}]"), optional);
                return false;
            }
        }
        public static bool AreNotEqual(object A, object B, bool optional = false)
        {
            if (!A.Equals(B))
            {
                success($"Object {A} does not equal {B}");
                return true;
            }
            else
            {
                Functions.handleFailure(new Exception($"Assert - type:[{A?.GetType()?.FullName}] value:[${A}]   Equals  type: [{A?.GetType()?.FullName}] value: [{B}]"), optional);
                return false;
            }
        }

        /// <summary>
        /// checks whether the given condition was false,<br></br>
        /// </summary>
        /// <returns> will return true if the condition was true</returns>
        public static bool IsTrue(bool? condition, bool optional = false)
        {
            
            if(condition.HasValue && condition.Value == true)
            {
                success($"Condition {condition}");
                return true;
            }
            else
            {
                Functions.handleFailure(new Exception($"Assert is True - for given condition: ${condition} was not true"), optional);
                return false;
            }
        }

        /// <summary>
        /// checks whether the given condition was false,<br></br>
        /// </summary>
        /// <returns> will return true if the condition equals false or null</returns>
        public static bool IsFalse(bool? condition, bool optional = false)
        {

            if (condition.HasValue && condition.Value == true)
            {
                Functions.handleFailure(new Exception($"Assert is False - the given condition: ${condition} was not false"), optional);
                return false;
            }
            else
            {
                success($"Condition {condition}");
                return true;
            }
        }

        public static bool IsNull(object _object, bool optional = false)
        {

            if (_object == null)
            {
                success("Object is null");
                return true;
            }
            else
            {
                
                Functions.handleFailure(new Exception($"Assert is Null - the given Object: ${_object} was not null"), optional);
                return false;
            }
        }
        public static bool IsNotNull(object _object, bool optional = false)
        {

            if (_object != null)
            {
                success("Object is not null");
                return true;
            }
            else
            {

                Functions.handleFailure(new Exception($"Assert is Not Null - the given Object: ${_object} was null"), optional);
                return false;
            }
        }

        private static void success(String message)
        {
            Log.Info("Success - Assert: " + message);
        }
    }
}
