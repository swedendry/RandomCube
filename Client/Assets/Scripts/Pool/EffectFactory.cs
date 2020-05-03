using System;
using UnityEngine;

public enum EffectId
{
    Damage,
    Ice,
    Poison,
}

public static class EffectFactory
{
    public static string Id2key(this EffectId id)
    {
        return "Effect_" + id.ToString();
    }

    public static EffectId Key2Id(this string key)
    {
        var splits = key.Split('_');
        return (EffectId)Enum.Parse(typeof(EffectId), splits[1]);
    }

    public static void Spawn(EffectId id, Transform target, float durationTime, params object[] values)
    {
        var key = id.Id2key();
        var entity = PoolFactory.Get<Effect>(key, Vector3.zero, Quaternion.identity);
        entity.OnReturn = Return;
        entity.Spawn(key, target, durationTime, values);
    }

    public static void Return(string key, GameObject obj)
    {
        PoolFactory.Return(key, obj);
    }
}
