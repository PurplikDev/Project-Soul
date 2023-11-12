using roguelike.core.item;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class ResultSlot : ItemSlot
{
    public ResultSlot() : base() {} // todo: add background images for equipment slot to show what equipment type it is

    public override bool SetStack(ItemStack stack) {
        if(stack.IsEmpty())
        {
            return base.SetStack(stack);
        }
        return false;
    }

    // These two methods need to be separate in order to prevent the mouse item putting shit in the result slot
    public void ForceStack(ItemStack stack) {
        base.SetStack(stack);
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<ResultSlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
