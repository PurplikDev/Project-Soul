using roguelike.enviroment.entity.StatSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer {
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        var container = new VisualElement();

        var popup = new UnityEngine.UIElements.PopupWindow();
        popup.text = "Stat Details";
        popup.Add(new PropertyField(property.FindPropertyRelative("_baseValue"), "BaseValue"));
        container.Add(popup);

        return container;
    }
}