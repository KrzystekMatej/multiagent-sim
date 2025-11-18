using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Hierarchy
{
    public static GameObject GetRoot(GameObject obj)
    {
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
        }
        return obj;
    }

    public static bool IsChild(GameObject child, GameObject parent)
    {
        while (child.transform.parent != null)
        {
            child = child.transform.parent.gameObject;
            if (child == parent)
            {
                return true;
            }
        }
        return false;
    }

    public static GameObject GetParentIf(GameObject obj, Func<GameObject, bool> condition)
    {
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            if (condition(obj)) return obj;
        }
        return null;
    }
}
