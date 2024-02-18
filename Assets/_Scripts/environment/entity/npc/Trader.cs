using roguelike.core.item;
using roguelike.environment.entity.player;
using roguelike.rendering.ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.environment.entity.npc
{
    public class Trader : NPC
    {
        public int Gold { get; private set; }

        public List<ItemStack> Stock {  get; private set; }

        public TradeRenderer GetRenderer(Player interactor)
        {
            return new TradeRenderer(interactor.Inventory, this, InteractionScreenHolder.GetComponent<UIDocument>());
        }
    }
}   