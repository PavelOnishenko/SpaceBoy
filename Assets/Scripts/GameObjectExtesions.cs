using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetComponentFromParentByName<T>(this GameObject child, string parentName) where T : Component
    {
        Transform currentTransform = child.transform;

        while (currentTransform != null)
        {
            if (currentTransform.name == parentName)
            {
                T component = currentTransform.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }

            currentTransform = currentTransform.parent;
        }

        Debug.LogError("Didn't find anything!");
        return null;
    }

    public static bool IsParentWithName(this GameObject obj, string parentName)
    {
        Transform currentParent = obj.transform.parent;
        while (currentParent != null)
        {
            if (currentParent.name == parentName)
            {
                return true;
            }
            currentParent = currentParent.parent;
        }
        return false;
    }
}
