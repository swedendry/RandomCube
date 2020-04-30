using System;
using System.Collections.Generic;

namespace Extension
{
    public static class ListExtension
    {
        public static void Upsert<T>(this List<T> targets, T source, Predicate<T> match)
        {
            if (source == null)
                return;

            var index = targets.FindIndex(match);
            if (index == -1)
            {   //생성
                targets.Add(source);
            }
            else
            {
                targets[index] = source;
            }
        }

        public static void Delete<T>(this List<T> targets, Predicate<T> match)
        {
            var target = targets.Find(match);
            if (target != null)
                targets.Remove(target);
        }
    }
}