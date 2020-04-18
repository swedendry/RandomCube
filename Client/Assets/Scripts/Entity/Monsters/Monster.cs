using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    private Transform[] path;

    public void Move(List<GameObject> paths)
    {
        this.path = paths.Select(x => x.transform).ToArray();
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 10, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop, "movetopath", false));
        //iTween.PutOnPath(gameObject, this.paths, 0f);
        //iTween.PointOnPath(paths.Select(x => x.transform).ToArray(), 1f);
    }
}
