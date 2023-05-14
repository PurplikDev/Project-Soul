using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace io.purplik.ProjectSoul.InventorySystem
{
    public class ItemTooltip : MonoBehaviour
    {
        [Header("Tooltip Texts")]
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemSlotType;
        [SerializeField] TextMeshProUGUI itemClassStats;
        [SerializeField] TextMeshProUGUI itemPlayerStats;
        [Space]
        [Header("Tooltip Item Icon")]
        [SerializeField] Image itemSprite;

        private StringBuilder stringBuilder = new StringBuilder();

        public void ShowTooltip(EquipmentItem item)
        {
            itemName.text = item.itemName;
            itemSlotType.text = item.equipmentType.ToString();
            itemSprite.sprite = item.icon;

            stringBuilder.Length = 0;

            // CLASS STATS
            AddStatText(item.rogueStat, "Rogue");
            AddStatText(item.thaumaturgeStat, "Thaumaturge");
            AddStatText(item.templarStat, "Templar");

            itemClassStats.text = stringBuilder.ToString();
            stringBuilder.Length = 0;

            // PLAYER STATS
            AddStatText(item.healthBonus, "Health");
            AddStatText(item.defenceBonus, "Defence");
            AddStatText(item.speedBonus, "Speed");

            // PLAYER STATS PERCENTAGE
            AddStatText(item.healthPercentBonus, "Health", true);
            AddStatText(item.defencePercentBonus, "Defence", true);
            AddStatText(item.speedPercentBonus, "Speed", true);

            itemPlayerStats.text = stringBuilder.ToString();

            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        private void AddStatText(float value, string statName, bool isPercent = false)
        {
            if(value != 0)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.AppendLine();

                if(value > 0) {
                    stringBuilder.Append("<color=green>+");
                } else if (value < 0) {
                    stringBuilder.Append("<color=red>"  );
                } else {
                    stringBuilder.Append("<color=white>");
                }

                if (isPercent) {
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%</color> | ");
                } else
                {
                    stringBuilder.Append(value);
                    stringBuilder.Append("</color> | ");
                }

                stringBuilder.Append(statName);
            }
        }
    }
}