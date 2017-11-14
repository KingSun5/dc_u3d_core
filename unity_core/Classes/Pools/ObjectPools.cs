using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;

/// <summary>
/// 对象池类接口
/// 1.需要重写Init
/// 2.使用对象池的类，集合成员变量，需要在Init里面清空
/// @author hannibal
/// @time 2016-9-11
/// </summary>
public interface IPoolsObject
{
    void Init();
}

/// <summary>
/// 对象池
/// 1.使用对象池，需要把原有对象的集合清空(list,dictionary等)
/// @author hannibal
/// @time 2016-7-24
/// </summary>
public sealed class ObjectPools
{
    private int m_total_new_count = 0;
    //一个无序的集合，程序可以向其中插入元素，或删除元素。在同一个线程中向集合插入，删除元素的效率很高。
    private List<object> m_obj_pools = new List<object>();
    private static Dictionary<string, long> m_new_count = new Dictionary<string, long>();
    private static Dictionary<string, long> m_remove_count = new Dictionary<string, long>();

    public T Spawn<T>() where T : new()
    {
        object obj = null;
        if (m_obj_pools.Count == 0)
        {
            ++m_total_new_count;
            obj = new T();

            //修改次数
            Type t = typeof(T);
            long count = 0;
            if (!m_new_count.TryGetValue(t.FullName, out count))
                m_new_count.Add(t.FullName, 1);
            else
                m_new_count[t.FullName] = ++count;
        }
        else
        {
            obj = m_obj_pools[m_obj_pools.Count - 1];
            m_obj_pools.RemoveAt(m_obj_pools.Count - 1);
        }
        //初始化
        if (obj is IPoolsObject)
        {
            ((IPoolsObject)obj).Init();
        }
        return (T)obj;
    }
    public void Despawn<T>(T obj) where T : new()
    {
        if (obj == null) return;
        m_obj_pools.Add(obj);

        //修改次数
        Type t = typeof(T);
        long count = 0;
        if (!m_remove_count.TryGetValue(t.FullName, out count))
            m_remove_count.Add(t.FullName, 1);
        else
            m_remove_count[t.FullName] = ++count;
    }
    public static string ToString(bool is_print)
    {
        StringBuilder st = new StringBuilder();
        st.AppendLine("ObjectPools使用情况:");
        foreach (var obj in m_new_count)
        {
            string class_name = obj.Key;
            string one_line = class_name + " New次数:" + obj.Value;
            long count;
            if (m_remove_count.TryGetValue(class_name, out count))
            {
                one_line += " 空闲数量:" + count;
            }
            st.AppendLine(one_line);
        }
        if (is_print) Console.WriteLine(st.ToString());
        return st.ToString();
    }
}

public sealed class CommonObjectPools
{
    private static Dictionary<string, List<object>> m_pools = new Dictionary<string, List<object>>();
    private static Dictionary<string, long> m_new_count = new Dictionary<string, long>();
    /// <summary>
    /// 分配对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">类名</param>
    /// <returns></returns>
    public static T Spawn<T>() where T : new()
    {
        object obj = null;
        Type t = typeof(T);
        List<object> list;
        if(m_pools.TryGetValue(t.FullName, out list) && list.Count > 0)
        {
            obj = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }
        //创建新对象
        if(obj == null)
        {
            obj = new T();
            //修改次数
            long count = 0;
            if (!m_new_count.TryGetValue(t.FullName, out count))
                m_new_count.Add(t.FullName, 1);
            else
                m_new_count[t.FullName] = ++count;
        }
        //初始化
        if (obj is IPoolsObject)
        {
            ((IPoolsObject)obj).Init();
        }
        return (T)obj;
    }
    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj"></param>
    public static void Despawn(object obj)
    {
        if (obj == null) return;
        Type t = obj.GetType();
        string name = t.FullName;
        List<object> list;
        if (!m_pools.TryGetValue(name, out list))
        {
            list = new List<object>();
            m_pools.Add(name, list);
        }
        list.Add(obj);
    }

    public static string ToString(bool is_print)
    {
        StringBuilder st = new StringBuilder();
        st.AppendLine("CommonObjectPools使用情况:");
        foreach(var obj in m_new_count)
        {
            string class_name = obj.Key;
            string one_line = class_name + " New次数:" + obj.Value;
            List<object> list;
            if (m_pools.TryGetValue(class_name, out list))
            {
                one_line += " 空闲数量:" + list.Count;
            }
            st.AppendLine(one_line);
        }
        if (is_print) Console.WriteLine(st.ToString());
        return st.ToString();
    }
}