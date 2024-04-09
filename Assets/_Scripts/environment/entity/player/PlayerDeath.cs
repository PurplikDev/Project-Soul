using Cinemachine;
using roguelike.core.utils;
using roguelike.system.manager;
using System.IO;
using Tweens;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity.player {
    public class PlayerDeath : MonoBehaviour {

        CinemachineVirtualCamera virtualCamera;
        [SerializeField] UIDocument document;

        VisualElement deathBG;
        Label deathLabel;
        Button deathButton;

        Player player;

        private void Awake() {
            player = GetComponentInParent<Player>();
            if (player != null) {
                player.DeathEvent += Death;
            }

            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>(); // i hate doing this

            deathBG = document.rootVisualElement.Q<VisualElement>("DeathScreenBackground");
            deathLabel = document.rootVisualElement.Q<Label>("DeathScreenHeader");
            deathButton = document.rootVisualElement.Q<Button>("DeathScreenButton");

            TranslationManager.TranslateHeader(deathLabel);

            deathButton.clicked += EndGame;
        }

        private void Death() {
            player.DeathEvent -= Death;
            CameraZoom();
        }

        private void CameraZoom() {

            float orthoSize =  virtualCamera.m_Lens.OrthographicSize;

            var tween = new FloatTween {
                duration = 2.5f,
                from = 1f,
                to = 0.5f,
                easeType = EaseType.ExpoInOut,
                onUpdate = (_, value) => {
                    Time.timeScale = value;
                    virtualCamera.m_Lens.OrthographicSize = orthoSize - (1 - value);
                },
                onFinally = (_) => {
                    BlackOut();
                }
            };
            gameObject.AddTween(tween);
        }

        private void BlackOut() { 
            var tween = new FloatTween {
                duration = 1.5f,
                from = 0f,
                to = 1f,
                onUpdate = (_, value) => {
                    deathBG.style.opacity = value;
                },
                onFinally = (_) => {
                    DisplayText();
                }
            };
            gameObject.AddTween(tween);
        }

        private void DisplayText() {
            var tween = new FloatTween {
                duration = 0.75f,
                from = 0f,
                to = 1f,
                onUpdate = (_, value) => {
                    deathLabel.style.opacity = value;
                    deathButton.style.opacity = value;
                }
            };
            gameObject.AddTween(tween);
        }


        // trigger on button press later :3
        private void EndGame() {
            Time.timeScale = 1f;
            if(GameManager.CurrentGameData.IsPermaDeath) {
                File.Delete($"{GlobalStaticValues.SAVE_PATH}/{GameManager.CurrentGameData.Name}.json");
                GameManager.Instance.GoToMainMenu();
            } else {
                GameManager.Instance.LoadSave(SaveFileUtils.GetDataFromFile($"{GlobalStaticValues.SAVE_PATH}/{GameManager.CurrentGameData.Name}.json"));
                LoadingManager.Instance.LoadScene(2, GameState.TOWN);
            }
        }
    }
}