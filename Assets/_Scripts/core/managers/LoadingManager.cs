using roguelike.system.singleton;
using UnityEngine.UIElements;

namespace roguelike.system.manager {
    public class LoadingManager : Singleton<LoadingManager> {
        private static VisualElement _root, _loadingProgressBar;

        protected override void Awake() {
            base.Awake();

            _root = GetComponent<UIDocument>().rootVisualElement;

        }

        public static void BeginLoading() {
            _root.style.visibility = Visibility.Visible;
        }

        public static void UpdateProgress(float progress) {
            _loadingProgressBar.style.width = progress * 100;
        }

        public static void FinishLoading() {
            _root.style.visibility = Visibility.Hidden;
        }
    }
}