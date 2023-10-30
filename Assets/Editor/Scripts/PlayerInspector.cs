using roguelike.enviroment.entity.player;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor
{
    public override VisualElement CreateInspectorGUI() {
        VisualElement myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Resources/ui/editor/PlayerInspector.uxml");
        visualTree.CloneTree(myInspector);
        return myInspector;
    }
}
