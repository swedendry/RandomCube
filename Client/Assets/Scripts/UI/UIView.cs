using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIView : MonoBehaviour
    {
        public Action<string> OnEvent;

        private List<UIComponent> components = new List<UIComponent>();

        protected virtual void Awake()
        {
            components = GetComponentsInChildren<UIComponent>(true).ToList();
            components.ForEach(x => { x.OnEvent = Event; });
        }

        protected virtual void Create()
        {

        }

        protected virtual void Empty()
        {
            components.ForEach(x => x.Empty());
        }

        public virtual void Upsert()
        {

        }

        public virtual void Event(string param)
        {
            OnEvent?.Invoke(param);
        }

        protected virtual T GetUIComponent<T>() where T : UIComponent
        {
            return components.Find(x => x.GetType() == typeof(T)) as T;
        }

        protected virtual List<T> GetUIComponents<T>() where T : UIComponent
        {
            return components.FindAll(x => x.GetType() == typeof(T)).Cast<T>().ToList();
        }
    }

    public class UIView<T> : UIView
    {
        public Action<bool, Props<T>> OnEventProps;

        [HideInInspector]
        public List<Props<T>> selectProps = new List<Props<T>>();

        protected override void Awake()
        {
            base.Awake();

            var components = GetUIComponents<UIComponent<T>>();
            components.ForEach(x => { x.OnEventProps = Event; });
        }

        protected override void Empty()
        {
            base.Empty();

            selectProps.Clear();
        }

        public virtual void Event(bool isSelected, Props<T> props)
        {
            OnEventProps?.Invoke(isSelected, props);
        }
    }
}
