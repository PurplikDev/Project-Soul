using Discord;
using roguelike.system.singleton;

namespace roguelike.system.manager {
    public class DiscordManager : PersistentSingleton<DiscordManager> {

        Discord.Discord discord;

        protected override void Awake() {
            try {
                discord = new Discord.Discord(1216476050599575723, (ulong)Discord.CreateFlags.NoRequireDiscord);
                ChangeActivity("", "Loading the game...");
            } catch (ResultException) {
                gameObject.SetActive(false);
            }

            base.Awake();
        }

        private void OnDisable() {
            discord?.Dispose();
        }

        public void ChangeActivity(string state, string details) {
            var activityManager = discord?.GetActivityManager();
            var activity = new Discord.Activity {
                State = state,
                Details = details,

                Assets = {
                    LargeImage = "icon"
                }

        };

            activityManager?.UpdateActivity(activity, (res) => {});
        }

        private void Update() {
            discord?.RunCallbacks();
        }
    }
}