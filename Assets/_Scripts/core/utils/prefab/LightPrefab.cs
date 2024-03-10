
using UnityEngine;

namespace roguelike.core.utils.prefab {
    public class LightPrefab : MonoBehaviour {

        public Light PrefabLight;
        public ParticleSystem LightParticle;

        public void SetState(bool state) {
            PrefabLight?.gameObject.SetActive(state);
            if(state) {
                LightParticle?.Play();
            } else {
                LightParticle?.Stop();
            }
            
        }
    }
}
