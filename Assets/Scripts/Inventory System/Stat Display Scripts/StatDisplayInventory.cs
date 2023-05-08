using io.purplik.ProjectSoul.Entity.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class StatDisplayInventory : MonoBehaviour
    {
        [SerializeField] StatDisplay[] statDisplays;
        [SerializeField] string[] statDisplayNames;

        private EntityStat[] stats;

        private void OnValidate()
        {
            statDisplays = GetComponentsInChildren<StatDisplay>();
            UpdateStatNames();
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
            for (int i = 0; i < stats.Length; i++)
            {
                statDisplays[i].valueText.text = stats[i].Value.ToString();
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
