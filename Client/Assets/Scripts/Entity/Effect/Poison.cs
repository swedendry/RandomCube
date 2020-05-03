using System.Collections;
using UnityEngine;

public class Poison : Effect
{
    public override void Spawn(string key, Transform target, float durationTime, params object[] values)
    {
        Spawn(key, target);

        StartCoroutine(CoroutineTimer(durationTime));
        StartCoroutine(CoroutineDamage(target.GetComponent<Monster>()));
    }

    protected IEnumerator CoroutineDamage(Monster monster)
    {
        while (true)
        {
            monster.Skill(key.Key2Id());

            yield return new WaitForSeconds(0.2f);
        }
    }
}
