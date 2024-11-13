using HarmonyLib;
using static ExtraSlotsAPI.API;

namespace ExtraSlotsAPI
{
    internal static class Extensions
    {
        internal static ExtraSlot ToExtraSlot(this object slot)
        {
            return new ExtraSlot()
            {
                _id = () => (string)AccessTools.Property(_typeSlot, "ID").GetValue(slot),
                _name = () => (string)AccessTools.Property(_typeSlot, "Name").GetValue(slot),
                _gridPosition = () => (Vector2i)AccessTools.Property(_typeSlot, "GridPosition").GetValue(slot),
                _item = () => (ItemDrop.ItemData)AccessTools.Property(_typeSlot, "Item").GetValue(slot),
                _itemFits = (item) => (bool)AccessTools.Method(_typeSlot, "ItemFits").Invoke(slot, new[] { (object)item }),
                _isActive = () => (bool)AccessTools.Property(_typeSlot, "IsActive").GetValue(slot),
                _isFree = () => (bool)AccessTools.Property(_typeSlot, "IsFree").GetValue(slot),
                _isHotkeySlot = () => (bool)AccessTools.Property(_typeSlot, "IsHotkeySlot").GetValue(slot),
                _isEquipmentSlot = () => (bool)AccessTools.Property(_typeSlot, "IsEquipmentSlot").GetValue(slot),
                _isQuickSlot = () => (bool)AccessTools.Property(_typeSlot, "IsQuickSlot").GetValue(slot),
                _isMiscSlot = () => (bool)AccessTools.Property(_typeSlot, "IsMiscSlot").GetValue(slot),
                _isAmmoSlot = () => (bool)AccessTools.Property(_typeSlot, "IsAmmoSlot").GetValue(slot),
                _isFoodSlot = () => (bool)AccessTools.Property(_typeSlot, "IsFoodSlot").GetValue(slot),
                _isCustomSlot = () => (bool)AccessTools.Property(_typeSlot, "IsCustomSlot").GetValue(slot),
                _isEmptySlot = () => (bool)AccessTools.Property(_typeSlot, "IsEmptySlot").GetValue(slot)
            };
        }
    }
}
