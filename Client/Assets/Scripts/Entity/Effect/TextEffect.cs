//using UnityEngine;

//public class TextEffect : Effect
//{
//    private Animation anim;
//    public TextMesh text;

//    private void Awake()
//    {
//        anim = GetComponent<Animation>();
//    }

//    public override void Spawn(string key, Transform target, float durationTime, params object[] values)
//    {
//        Spawn(key, target);

//        var clipName = values[1];
//        var animTime = anim.GetClip(clipName);
//        StartCoroutine(CoroutineTimer(animTime.length));

//        text.text = values[0].ToString();
//        anim.Play(clipName);
//    }
//}
