using System;

[Serializable]
public class Item {
    public ItemStack itemStack;

    public Item(ItemStack item) {
        itemStack = item;
    }
}
