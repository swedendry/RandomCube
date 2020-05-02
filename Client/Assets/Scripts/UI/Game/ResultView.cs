using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Game
{
    public class ResultView : UIView
    {
        private ResultComponent resultComponent;

        protected override void Awake()
        {
            base.Awake();

            resultComponent = GetUIComponent<ResultComponent>();
        }

        public override void Upsert()
        {
            resultComponent?.Upsert();

            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        public override void Event(string param)
        {
            switch (param)
            {
                case "lobby":
                    {
                        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                    }
                    break;
            }
        }
    }
}