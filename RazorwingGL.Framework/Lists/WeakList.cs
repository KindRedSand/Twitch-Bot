﻿using System;
using System.Collections.Generic;

namespace RazorwingGL.Framework.Lists
{
    /// <summary>
    /// A list maintaining weak reference of objects.
    /// </summary>
    /// <typeparam name="T">Type of items tracked by weak reference.</typeparam>
    public class WeakList<T> : List<WeakReference<T>>
        where T : class
    {
        public void Add(T obj) => Add(new WeakReference<T>(obj));

        /// <summary>
        /// Iterate on alive items, and remove non-alive references.
        /// </summary>
        public void ForEachAlive(Action<T> action)
        {
            int index = 0;
            while (index < Count)
            {
                T obj;
                if (this[index].TryGetTarget(out obj))
                {
                    action(obj);
                    index++;
                }
                else
                    RemoveAt(index);
            }
        }
    }
}
