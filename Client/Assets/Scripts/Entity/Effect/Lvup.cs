using UnityEngine;

public class Lvup : Effect
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void Spawn(string key, Transform target, float durationTime, params object[] values)
    {
        Spawn(key, target);

        var clipName = "Lvup_Spwan";
        var animTime = anim.GetClip(clipName);
        StartCoroutine(CoroutineTimer(animTime.length));

        anim.Play(clipName);
    }
}
