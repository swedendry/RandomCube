using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Renderer renderer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
        }
    }
}
