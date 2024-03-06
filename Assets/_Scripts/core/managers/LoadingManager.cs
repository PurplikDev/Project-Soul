using roguelike.system.singleton;
using System.Threading.Tasks;
using Tweens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace roguelike.system.manager {
    public class LoadingManager : PersistentSingleton<LoadingManager> {
        private static VisualElement _root, _loadingHolder, _loadingProgressBar;

        protected override void Awake() {
            base.Awake();

            _root = GetComponent<UIDocument>().rootVisualElement;
            _loadingHolder = _root.Q<VisualElement>("LoadingScreenHolder");
            _loadingProgressBar = _root.Q<VisualElement>("ProgressBar");

            _loadingHolder.style.opacity = 0f;
            _loadingHolder.style.visibility = Visibility.Hidden;
        }

        public void LoadScene(int sceneIndex, GameState gameState) {
            BeginLoading(sceneIndex, gameState);
        }

        internal async void AsyncLoadGame(int sceneIndex, GameState gameState) {
            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            GameManager.CurrentGameState = gameState;
            scene.allowSceneActivation = false;

            do {
                await Task.Delay(100);
                UpdateProgress(scene.progress);
            } while (scene.progress < 0.9f);

            await Task.Delay(1000);
            UpdateProgress(1);

            scene.allowSceneActivation = true;
            FinishLoading();
        }

        internal void BeginLoading(int sceneIndex, GameState gameState) {
            _loadingHolder.style.opacity = 0f;
            _loadingProgressBar.style.width = 0f;
            _loadingHolder.style.visibility = Visibility.Visible;

            var tween = new FloatTween {
                from = 0f, to = 1f, duration = 0.5f,
                onUpdate = (_, value) => {
                    _loadingHolder.style.opacity = value;
                },
                onFinally = (_) => {
                    AsyncLoadGame(sceneIndex, gameState);
                }
            };

            gameObject.AddTween(tween);
        }

        public void UpdateProgress(float progress) {
            _loadingProgressBar.style.width = new StyleLength(new Length(progress * 100, LengthUnit.Percent));
        }

        public void FinishLoading() {
            var tween = new FloatTween {
                from = 1f,
                to = 0f,
                duration = 1.5f,
                onUpdate = (_, value) => {
                    _loadingHolder.style.opacity = value;
                },
                onFinally = (_) => {
                    _loadingHolder.style.visibility = Visibility.Hidden;
                }
            };

            gameObject.AddTween(tween);
        }
    }
}