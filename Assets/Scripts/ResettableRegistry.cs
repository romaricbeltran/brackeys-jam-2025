using System.Collections.Generic;


public static class ResettableRegistry
{
private static List<IResettable> resettableObjects = new List<IResettable>();

public static void Register(IResettable obj)
{
    resettableObjects.Add(obj);
}

public static void Unregister(IResettable obj)
{
    resettableObjects.Remove(obj);
}

public static List<IResettable> GetAll() => resettableObjects;
}
