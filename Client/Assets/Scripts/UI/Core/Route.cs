﻿using Extension;
using UnityEngine;

namespace UI
{
    public class Route : MonoBehaviour
    {
        public string path;

        //private void Awake()
        //{
        //    //var parents = GetComponentsInParent<Route>().ToList();
        //    //parents.Reverse();
        //    //path = string.Join("/", parents.Select(c => c.name));

        //    //Router.Register(this);

        //    Close();
        //}

        //private void OnDestroy()
        //{
        //    Router.UnRegister(this);
        //}

        public void Close()
        {
            gameObject?.SetVisible(false);
        }

        public void Open()
        {
            gameObject?.SetVisible(true);
        }

        public void Refresh()
        {
            if (gameObject.activeSelf)
                gameObject?.GetComponent<UIView>()?.Upsert();
        }
    }
}