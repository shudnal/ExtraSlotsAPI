using System;

namespace ExtraSlotsAPI
{
    public class ExtraSlot
    {
        internal Func<string> _id;
        internal Func<string> _name;
        internal Func<Vector2i> _gridPosition;
        internal Func<ItemDrop.ItemData> _item;
        internal Func<ItemDrop.ItemData, bool> _itemFits;
        internal Func<bool> _isActive;
        internal Func<bool> _isFree;
        internal Func<bool> _isHotkeySlot;
        internal Func<bool> _isEquipmentSlot;
        internal Func<bool> _isQuickSlot;
        internal Func<bool> _isMiscSlot;
        internal Func<bool> _isAmmoSlot;
        internal Func<bool> _isFoodSlot;
        internal Func<bool> _isCustomSlot;
        internal Func<bool> _isEmptySlot;

        public static readonly Vector2i emptyPosition = new Vector2i(-1, -1);

        public string ID => _id == null ? "" : _id();
        public string Name => _name == null ? "" : _name();
        public Vector2i GridPosition => _gridPosition == null ? emptyPosition : _gridPosition();
        public ItemDrop.ItemData Item => _item == null ? null : _item();
        public bool ItemFits(ItemDrop.ItemData item) => _itemFits != null && _itemFits(item);
        public bool IsActive => _isActive != null && _isActive();
        public bool IsFree => _isFree != null && _isFree();
        public bool IsHotkeySlot => _isHotkeySlot != null && _isHotkeySlot();
        public bool IsEquipmentSlot => _isEquipmentSlot != null && _isEquipmentSlot();
        public bool IsQuickSlot => _isQuickSlot != null && _isQuickSlot();
        public bool IsMiscSlot => _isMiscSlot != null && _isMiscSlot();
        public bool IsAmmoSlot => _isAmmoSlot != null && _isAmmoSlot();
        public bool IsFoodSlot => _isFoodSlot != null && _isFoodSlot();
        public bool IsCustomSlot => _isCustomSlot != null && _isCustomSlot();
        public bool IsEmptySlot => _isEmptySlot != null && _isEmptySlot();
    }
}