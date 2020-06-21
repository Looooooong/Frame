using System;
using System.Collections.Generic;

public static class ArrayHelper 
{
    /// <summary>
    /// 满足条件的单个数据元素
    /// </summary>
    /// <typeparam name="T">数组的类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="condition">查找条件</param>
    /// <returns></returns>
    public static T Find<T> (this T[] array,Func<T,bool> condition)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if(condition(array[i]))
            {
                return array[i];
            }
        }

        return default(T);
    }

    /// <summary>
    /// 查找满足条件的所有元素
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="condition">条件</param>
    /// <returns></returns>
    public static T[] FindAll<T> (this T[] array, Func<T, bool> condition)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (condition(array[i]))
            {
                list.Add(array[i]);
            }
        }

        return list.ToArray();
    }


    public static T GetMax<T,Q>(this T[] array ,Func<T,Q> condition) where Q : IComparable
    {
        T max = array[0];
        for (int  i = 0;  i <array.Length;  i++)
        {
            if(condition(max).CompareTo(condition(array[i])) < 0)
            {
                max = array[i];
            }
        }

        return max;
    }


    public static T GetMin<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
    {
        T min = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (condition(min).CompareTo(condition(array[i])) > 0)
            {
                min = array[i];
            }
        }

        return min;
    }

    public static Q[] Select<T,Q>(this T[] array,Func<T,Q> condition)
    {
        Q[] res = new Q[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            res[i] = condition(array[i]);
        }

        return res;
    }
}
