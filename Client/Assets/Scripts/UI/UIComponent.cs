using System;
using UnityEngine;

namespace UI
{
    public class Props<T>
    {
        public int index;
        public T data;
    }

    public class UIComponent : MonoBehaviour
    {
        public Action<string> OnEvent;

        public virtual void Empty()
        {
            gameObject.SetActive(false);
        }

        public virtual void Upsert()
        {

        }

        public virtual void Event(string param)
        {
            OnEvent?.Invoke(param);
        }
    }

    public class UIComponent<T> : UIComponent
    {
        public Action<bool, Props<T>> OnEventProps;

        [HideInInspector]
        public Props<T> props;

        public virtual void Upsert(T data)
        {
            Upsert(0, data);
        }

        public virtual void Upsert(int index, T data)
        {
            props = new Props<T>()
            {
                index = index,
                data = data,
            };

            if (data == null)
            {
                Empty();
                return;
            }

            gameObject.SetActive(true);
        }

        public virtual void Event()
        {
            var isSelected = true;

            OnEventProps?.Invoke(isSelected, props);
        }
    }
}