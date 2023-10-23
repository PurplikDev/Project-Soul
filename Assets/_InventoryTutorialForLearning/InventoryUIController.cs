using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    private VisualElement _root;
    private VisualElement _slotContainer;

    private static VisualElement _ghostIcon;

    private static bool _isDragging;
    private static InventorySlot _originalSlot;

    private void Awake() {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _slotContainer = _root.Q<VisualElement>("InventorySlotContainer");

        for(int i = 0; i < 20; i++) {
            InventorySlot item = new InventorySlot();
            InventoryItems.Add(item);
            _slotContainer.Add(item);
        }

        GameController.OnInventoryChanged += GameController_OnInventoryChanged;

        _ghostIcon = _root.Q<VisualElement>("GhostIcon");
        _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        _ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerMove(PointerMoveEvent evt) {
        if (!_isDragging) {
            return;
        }

        _ghostIcon.style.top = evt.position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = evt.position.x - _ghostIcon.layout.width / 2;

    }

    private void OnPointerUp(PointerUpEvent evt) {
        if (!_isDragging) {
            return;
        }

        IEnumerable<InventorySlot> slots = InventoryItems.Where(x => x.worldBound.Overlaps(_ghostIcon.worldBound));

        if (slots.Count() != 0) {
            InventorySlot closestSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, _ghostIcon.worldBound.position)).First();

            closestSlot.HoldItem(GameController.GetItemByGUID(_originalSlot.ItemGUID));

            _originalSlot.DropItem();
        } else {
            _originalSlot.Icon.image =
                  GameController.GetItemByGUID(_originalSlot.ItemGUID).Icon.texture;
        }

        _isDragging = false;
        _originalSlot = null;
        _ghostIcon.style.visibility = Visibility.Hidden;

    }

    public static void StartDrag(Vector2 position, InventorySlot originalSlot) {
        _isDragging = true;
        _originalSlot = originalSlot;

        _ghostIcon.style.top = position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = position.x - _ghostIcon.layout.width / 2;

        _ghostIcon.style.backgroundImage = GameController.GetItemByGUID(_originalSlot.ItemGUID).Icon.texture;
        _ghostIcon.style.visibility = Visibility.Visible;
    }

    private void GameController_OnInventoryChanged(string[] itemGUID, InventoryChangeType change) {
        foreach(string item in itemGUID) {
            if(change == InventoryChangeType.PICKUP) {
                var emptySlot = InventoryItems.FirstOrDefault(x => x.ItemGUID.Equals(""));
                if(emptySlot != null) {
                    emptySlot.HoldItem(GameController.GetItemByGUID(item));
                }
            }
        }
    }
}
