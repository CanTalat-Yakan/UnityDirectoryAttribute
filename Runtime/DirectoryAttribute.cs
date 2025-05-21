using System;
using UnityEngine;

namespace UnityEssentials
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DirectoryAttribute : PropertyAttribute { }
}
