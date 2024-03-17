using System.Linq;
using Godot;
using Godot.Collections;

public class ECS
{
    public static ECS? instance;

    public Dictionary<Node, Dictionary<string, Array<Node>>> childCache;

    public static ECS GetInstance()
    {
        return instance ?? new ECS();
    }

    public static Array<T> GetChildrenOfType<[MustBeVariant] T>(Node parent, bool useCache = false)
        where T : Node
    {
        var instance = GetInstance();
        var className = typeof(T).FullName ?? "null class name";

        if (useCache)
        {
            // First check the cache

            Dictionary<string, Array<Node>>? nodeDict;
            Array<Node>? cachedArray;
            if (instance.childCache.TryGetValue(parent, out nodeDict))
            {
                if (nodeDict.TryGetValue(className, out cachedArray))
                {
                    return cachedArray as Array<T> ?? new Array<T>();
                }
            }
        }

        var uncachedChildren = GetUncachedChildrenOfType<T>(parent);
        if (useCache)
        {
            // Add it to the cache
            if (!instance.childCache.ContainsKey(parent))
            {
                instance.childCache[parent] = new Dictionary<string, Array<Node>>();
            }

            if (!instance.childCache[parent].ContainsKey(className))
            {
                instance.childCache[parent][className] = new Array<Node>();
            }

            instance.childCache[parent][className].Concat(uncachedChildren);
            parent.ChildOrderChanged += () =>
            {
                instance.childCache.Remove(parent);
            };
        }
        return uncachedChildren;
    }

    private static Array<T> GetUncachedChildrenOfType<[MustBeVariant] T>(Node parent)
        where T : Node
    {
        Array<T> items = new Array<T>();
        foreach (Node child in parent.GetChildren())
        {
            var casted = child as T;
            if (casted != null)
            {
                items.Add(casted);
            }
        }

        return items;
    }

    public ECS()
    {
        instance = this;
        childCache = new Dictionary<Node, Dictionary<string, Array<Node>>>();
    }
}
