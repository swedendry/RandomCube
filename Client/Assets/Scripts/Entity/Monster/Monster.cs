using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Entity
{
    private List<Transform> paths;
    private int targetIndex = 0;
    private float speed = 1f;

    public void Move(List<GameObject> paths)
    {
        this.paths = paths.Select(x => x.transform).ToList();

        Move(1);
    }

    public void Move(int targetIndex)
    {
        this.targetIndex = targetIndex;
        speed = 1f;

        var position = paths[targetIndex].position;
        var nextIndex = targetIndex + 1;

        base.Move(position, speed, nextIndex);
    }

    protected override void MoveComplete(object cmpParams)
    {
        var targetIndex = int.Parse(cmpParams.ToString());
        if (paths.Count <= targetIndex)
        {   //Finish
            Finish();
            return;
        }

        Move(targetIndex);
    }

    private void Finish()
    {
        Debug.Log("Finish");
    }

    public void Hit(Missile collider)
    {
        Debug.Log(collider.name);
    }
}
