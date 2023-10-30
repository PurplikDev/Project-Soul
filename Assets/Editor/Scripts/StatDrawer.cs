using roguelike.enviroment.entity.StatSystem;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer {
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        var container = new VisualElement();

        var popup = new UnityEngine.UIElements.PopupWindow();
        popup.text = fieldInfo.Name;
        popup.Add(new PropertyField(property.FindPropertyRelative("_baseValue"), "Base Value"));
        var label = new Label();
        //label.text = 
        popup.Add(label);
        container.Add(popup);

        return container;
    }
}