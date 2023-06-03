using io.purplik.ProjectSoul.Entity.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class StatDisplayInventory : MonoBehaviour
    {
        [SerializeField] StatDisplay[] statDisplays;
        [SerializeField] string[] statDisplayNames;

        [SerializeField] TextMeshProUGUI title;

        private EntityStat[] stats;

        private void OnValidate()
        {
            statDisplays = GetComponentsInChildren<StatDisplay>();
            UpdateStatNames();
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void SetStats(params EntityStat[] charStats)
        {
            stats = charStats;
            
            if(stats.Length > statDisplays.Length)
            {
                Debug.LogError("Not Enought Stat Display!");
                return;
            }

            for(int i = 0; i < statDisplays.Length; i++)
            {
                statDisplays[i].gameObject.SetActive(i < stats.Length);
            }
        }

        public void UpdateStatValues()
        {
            float highestValue = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                statDisplays[i].valueText.text = stats[i].Value.ToString();
                if (highestValue < stats[i].Value && i < 3)
                {
                    highestValue = stats[i].Value;
                    title.text = "The Legendary " + statDisplayNames[i];
                }
            }

            if (highestValue == 0)
            {
                title.text = "The Nomad";
            }
        }

        public void UpdateStatNames()
        {
            for (int i = 0; i < statDisplayNames.Length; i++)
            {
                statDisplays[i].nameText.text = statDisplayNames[i];
            }
        }
    }
}
