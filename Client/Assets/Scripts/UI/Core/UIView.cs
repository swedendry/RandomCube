using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIView : MonoBehaviour
    {
        public Action<string> OnEvent;

        private List<UIComponent> components = new List<UIComponent>();
        private List<UIContainer> containers = new List<UIContainer>();

        protected virtual void Awake()
        {
            components = GetComponentsInChildren<UIComponent>(true).Where(x => x.transform.parent.GetComponentInParent<UIContainer>() == null).ToList();
            components.ForEach(x => { x.OnEvent = Event; });

            containers = GetComponentsInChildren<UIContainer>(true).Where(x => x.transform.parent.GetComponentInParent<UIContainer>() == null).ToList();
            containers.ForEach(x => { x.OnEvent = Event; });
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(Enable());
        }

        protected virtual IEnumerator Enable()
        {
            yield return null;

            Upsert();
        }

        protected virtual void Empty()
        {
            components.ForEach(x => x.Empty());
            containers.ForEach(x => x.Empty());
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

        protected virtual T GetUIContainer<T>() where T : UIContainer
        {
            return containers.Find(x => x.GetType() == typeof(T)) as T;
        }

        protected virtual T GetUIContainer<T>(int index) where T : UIContainer
        {
            return GetUIContainers<T>().Skip(index).FirstOrDefault();
        }

        protected virtual List<T> GetUIContainers<T>() where T : UIContainer
        {
            return containers.FindAll(x => x.GetType() == typeof(T)).Cast<T>().ToList();
        }
    }
}
