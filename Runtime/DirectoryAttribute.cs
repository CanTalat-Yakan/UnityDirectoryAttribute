using System;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Specifies that a string field should be displayed or processed as a directory path in the editor or UI.
    /// </summary>
    /// <remarks>This attribute can be applied to fields to indicate that they represent directory paths.  It
    /// is typically used in tools to provide specialized handling or validation  for directory path
    /// inputs.</remarks>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DirectoryAttribute : PropertyAttribute { }
}
