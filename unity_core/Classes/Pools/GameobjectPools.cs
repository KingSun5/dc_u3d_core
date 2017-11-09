using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// gameobject对象池
/// </summary>
public class GameobjectPools
{
    private static Dictionary<string, List<Transform>> m_DicFile2Pool = new Dictionary<string, List<Transform>>();

    /// <summary>
    /// 产生对象
    /// </summary>
    public static Transform Spawn(string file)
    {
        if (file.Length == 0) return null;

        Transform spawnItem = null;
        List<Transform> list = null;
        ///1.查找pools
        if (m_DicFile2Pool.TryGetValue(file, out list))
        {
            if (list.Count > 0)
            {
                spawnItem = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
            }
        }
        ///2.创建新的
        if (spawnItem == null)
        {
            Object res = ResourceLoaderManager.Instance.Load(file);
            if (res == null) return null;
            spawnItem = (GameObject.Instantiate(res, Vector3.zero, Quaternion.identity) as GameObject).transform;
        }
        spawnItem.name = file;

        return spawnItem;
    }

    /// <summary>
    /// 回收
    /// </summary>
    public static void Despawn(Transform obj)
    {
        if (obj == null || string.IsNullOrEmpty(obj.gameObject.name)) return;

        //重置
        TrailRenderer[] list_render = obj.GetComponentsInChildren<TrailRenderer>();
        for (int i = 0; i < list_render.Length; ++i)
        {
            list_render[i].Clear();
        }
        obj.gameObject.SetActive(false);

        List<Transform> itemArray = null;
        if (!m_DicFile2Pool.TryGetValue(obj.gameObject.name, out itemArray))
        {
            itemArray = new List<Transform>();
            m_DicFile2Pool.Add(obj.gameObject.name, itemArray);
        }

        if (!itemArray.Contains(obj)) itemArray.Add(obj);
    }

    public static void Clear()
    {
        foreach(var item_list in m_DicFile2Pool)
        {
            foreach(var obj in item_list.Value)
            {
                if (obj != null) GameObject.Destroy(obj.gameObject);
            }
        }
        m_DicFile2Pool.Clear();
    }
}
