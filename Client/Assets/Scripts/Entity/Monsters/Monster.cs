using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    private Transform[] paths;
    public void Move(List<GameObject> paths)
    {
        this.paths = paths.Select(x => x.transform).ToArray();
        iTween.PutOnPath(gameObject, this.paths, 0f);
        //iTween.PointOnPath(paths.Select(x => x.transform).ToArray(), 1f);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawLine 는 직선으로 선을 그립니다.
        // iTween.DrawPath 는 곡선형태로 선을 그립니다.
        // iTween.DrawPath 로 그리는 라인과 iTween.PutOnPath 로 이동하는 동선이 같습니다.
        iTween.DrawPath(paths, Color.magenta);
        Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position, lookTarget.transform.position);
    }
}
