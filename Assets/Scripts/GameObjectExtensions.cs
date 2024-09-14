using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetComponentFromParentByName<T>(this GameObject child, string parentName) where T : Component
    {
        var currentTransform = child.transform;

        while (currentTransform != null)
        {
            if (currentTransform.name == parentName && currentTransform.TryGetComponent<T>(out var component)) 
                return component;
            currentTransform = currentTransform.parent;
        }
        Debug.LogError($"Didn't find parent with name [{parentName}].");
        return null;
    }

    public static bool IsParentWithName(this GameObject obj, string parentName)
    {
        Transform currentParent = obj.transform.parent;
        while (currentParent != null)
        {
            if (currentParent.name == parentName) 
                return true;
            currentParent = currentParent.parent;
        }
        return false;
    }
}