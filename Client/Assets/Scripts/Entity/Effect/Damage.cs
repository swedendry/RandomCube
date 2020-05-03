using UnityEngine;

public class Damage : Effect
{
    private Animation anim;
    public TextMesh damage_text;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void Spawn(string key, Transform target, float durationTime, params object[] values)
    {
        Spawn(key, target);

        var clipName = "Damage_Spwan";
        var animTime = anim.GetClip(clipName);
        StartCoroutine(CoroutineTimer(animTime.length));

        damage_text.text = values[0].ToString();
        anim.Play(clipName);
    }
}
