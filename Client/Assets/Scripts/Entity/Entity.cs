using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void Create(Transform parent)
    {
        transform.parent = parent;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }

    protected virtual void Move(Vector3 position, float speed, object completeparams = null)
    {
        Move(position, speed, "MoveComplete", completeparams);
    }

    protected virtual void Move(Vector3 position, float speed, string complete, object completeparams = null)
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", position, "speed", speed, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", complete, "oncompleteparams", completeparams ?? string.Empty));
    }

    protected virtual void MoveComplete(object cmpParams)
    {

    }
}
