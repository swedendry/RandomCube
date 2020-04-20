using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void Spawn()
    {

    }

    protected virtual void Move(Vector3 position, float speed, object completeparams = null)
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", position, "speed", speed, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "MoveComplete", "oncompleteparams", completeparams ?? string.Empty));
    }

    protected virtual void MoveComplete(object cmpParams)
    {

    }
}
