using roguelike.system.singleton;
using System.Threading.Tasks;
using Tweens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace roguelike.system.manager {
    public class LoadingManager : PersistentSingleton<LoadingManager> {
        private static VisualElement _root, _loadingHolder, _loadingProgressBar;
        private static Label _tips;

        public string[] _loadingScreenTips;

        protected override void Awake() {
            base.Awake();

            _root = GetComponent<UIDocument>().rootVisualElement;
            _loadingHolder = _root.Q<VisualElement>("LoadingScreenHolder");
            _loadingProgressBar = _root.Q<VisualElement>("ProgressBar");
            _tips = _root.Q<Label>("LoadingScreenTips");

            _loadingHolder.style.opacity = 0f;
            _loadingHolder.style.visibility = Visibility.Hidden;
        }

        public void LoadScene(int sceneIndex, GameState gameState) {
            BeginLoading(sceneIndex, gameState);
        }

        internal async void AsyncLoadGame(int sceneIndex, GameState gameState) {
            var scene = SceneManager.LoadSceneAsync(sceneIndex);
            scene.completed += GameManager.SpawnPlayer;
            GameManager.CurrentGameState = gameState;
            scene.allowSceneActivation = false;

            do {
                await Task.Delay(100);
                UpdateProgress(scene.progress);
            } while (scene.progress < 0.9f);

            await Task.Delay(1000);
            UpdateProgress(1);

            scene.allowSceneActivation = true;


            switch (gameState) {
                case GameState.MAINMENU:
                    DiscordManager.Instance?.ChangeActivity("Picking a save...", "In Main Menu");
                    break;

                case GameState.TOWN:
                    DiscordManager.Instance?.ChangeActivity("Gearing up...", "Pargenyl - Town");
                    break;

                case GameState.DUNGEON:
                    DiscordManager.Instance?.ChangeActivity("Floor X", "Surviving the Dungeon");
                    break;
            }

            FinishLoading();
        }

        internal void BeginLoading(int sceneIndex, GameState gameState) {

            int random = Random.Range(0, _loadingScreenTips.Length);

            _tips.text = $"Tip #{random + 1}: {_loadingScreenTips[random]}";

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

            Instance.gameObject.AddTween(tween);
        }

        public void UpdateProgress(float progress) {
            _loadingProgressBar.style.width = new StyleLength(new Length(progress * 100, LengthUnit.Percent));
        }

        public async void FinishLoading() {
            await Task.Delay(1000);

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