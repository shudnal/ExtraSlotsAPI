using BepInEx.Bootstrap;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExtraSlotsAPI
{
    public static class API
    {
        private static bool _isNotReady;
        private static List<ItemDrop.ItemData> emptyItemList = new List<ItemDrop.ItemData>();
        private static List<ExtraSlot> emptySlotList = new List<ExtraSlot>();

        internal static Type _typeAPI;
        internal static Type _typeSlot;
        internal static Type _typeCustomSlot;
        internal static Type _typeSlots;
        internal static FieldInfo _slots;

        private static IEnumerable<ExtraSlot> slots => ((object[])_slots.GetValue(null)).Select(slot => slot.ToExtraSlot());

        public static bool IsReady()
        {
            if (_isNotReady)
                return false;

            _isNotReady = !Chainloader.PluginInfos.ContainsKey("shudnal.ExtraSlots");
            if (_isNotReady)
                return false;

            if (_typeAPI == null)
                _typeAPI = AccessTools.TypeByName("ExtraSlots.API");

            if (_typeSlots == null)
                _typeSlots = AccessTools.TypeByName("ExtraSlots.Slots");

            if (_typeSlot == null)
                _typeSlot = AccessTools.TypeByName("ExtraSlots.Slots+Slot");

            if (_typeCustomSlot == null)
                _typeCustomSlot = AccessTools.TypeByName("ExtraSlots.Slots+CustomSlot");

            if (_slots == null)
                _slots = AccessTools.Field(_typeSlots, "slots");

            return _typeAPI != null;
        }

        /// <summary>
        /// Returns list of all slots
        /// </summary>
        public static List<ExtraSlot> GetExtraSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetEquipmentSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.Where(slot => slot.IsEquipmentSlot).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetQuickSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.Where(slot => slot.IsQuickSlot).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetFoodSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.Where(slot => slot.IsFoodSlot).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetAmmoSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.Where(slot => slot.IsAmmoSlot).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetMiscSlots()
        {
            if (!IsReady())
                return emptySlotList;

            return slots.Where(slot => slot.IsMiscSlot).ToList();
        }

        /// <summary>
        /// Returns slot with given ID
        /// </summary>
        /// <param name="slotID"></param>
        public static ExtraSlot FindSlot(string slotID)
        {
            if (!IsReady())
                return null;

            return slots.Where(slot => slot.ID == slotID || slot.ID == (string)AccessTools.Method(_typeCustomSlot, "GetSlotID").Invoke(null, new object[] { slotID })).FirstOrDefault();
        }

        /// <summary>
        /// Returns list of items located in extra slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetAllExtraSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return slots.Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetEquipmentSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return GetEquipmentSlots().Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetQuickSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return GetQuickSlots().Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetFoodSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return GetFoodSlots().Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetAmmoSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return GetAmmoSlots().Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetMiscSlotsItems()
        {
            if (!IsReady())
                return emptyItemList;

            return GetMiscSlots().Select(slot => slot.Item).Where(item => item != null).ToList();
        }

        /// <summary>
        /// Returns amount of extra rows added to player available inventory
        /// </summary>
        public static int GetExtraRows()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Property(_typeSlots, "ExtraRowsPlayer").GetValue(_typeSlots);
        }

        /// <summary>
        /// Returns full height of inventory
        /// </summary>
        public static int GetInventoryHeightFull()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Property(_typeSlots, "InventoryHeightFull").GetValue(_typeSlots);
        }

        /// <summary>
        /// Returns full height of inventory
        /// </summary>
        public static int GetInventoryHeightPlayer()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Property(_typeSlots, "InventoryHeightPlayer").GetValue(_typeSlots);
        }

        /// <summary>
        /// Returns if given position is extra slot
        /// </summary>
        /// <param name="gridPos">Position in inventory grid</param>
        public static bool IsGridPositionASlot(Vector2i gridPos)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeSlots, "IsGridPositionASlot").Invoke(_typeSlots, new object[] { gridPos });
        }

        /// <summary>
        /// Returns if given item is in extra slot
        /// </summary>
        /// <param name="item"></param>
        public static bool IsItemInSlot(ItemDrop.ItemData item)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeSlots, "IsItemInSlot").Invoke(_typeSlots, new object[] { item });
        }

        /// <summary>
        /// Returns if given item is in equipment slot
        /// </summary>
        /// <param name="item"></param>
        public static bool IsItemInEquipmentSlot(ItemDrop.ItemData item)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeSlots, "IsItemInEquipmentSlot").Invoke(_typeSlots, new object[] { item });
        }

        /// <summary>
        /// Adds new custom equipment slot at first available position
        /// </summary>
        /// <param name="slotID">To add new slot ID should be unique. If given ID is not unique returns true if slot is already created</param>
        /// <param name="getName">function that return slot name how it should be seen in the UI. Localization is recommended.</param>
        /// <param name="itemIsValid">function to check of item fits the slot</param>
        /// <param name="isActive">function to check if slot should be available in equipment panel. If you need live update - call UpdateSlots.</param>
        /// <returns></returns>
        public static bool AddSlot(string slotID, Func<string> getName, Func<ItemDrop.ItemData, bool> itemIsValid, Func<bool> isActive)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeCustomSlot, "TryAddNewSlotWithIndex").Invoke(_typeCustomSlot, new object[] { slotID, -1, getName, itemIsValid, isActive });
        }

        /// <summary>
        /// Adds new custom equipment slot with set position
        /// </summary>
        /// <param name="slotID">To add new slot ID should be unique. If given ID is not unique returns true if slot is already created</param>
        /// <param name="slotIndex">-1 to take first available empty slot. Otherwise shift other slots to the right and insert into position after vanilla equipment slots</param>
        /// <param name="getName">function that return slot name how it should be seen in the UI. Localization is recommended.</param>
        /// <param name="itemIsValid">function to check of item fits the slot</param>
        /// <param name="isActive">function to check if slot should be available in equipment panel. If you need live update - call UpdateSlots.</param>
        /// <returns></returns>
        public static bool AddSlotWithIndex(string slotID, int slotIndex, Func<string> getName, Func<ItemDrop.ItemData, bool> itemIsValid, Func<bool> isActive)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeCustomSlot, "TryAddNewSlotWithIndex").Invoke(_typeCustomSlot, new object[] { slotID, slotIndex, getName, itemIsValid, isActive });
        }

        /// <summary>
        /// Adds new custom equipment slot with set position
        /// </summary>
        /// <param name="slotID">To add new slot ID should be unique. If given ID is not unique returns true if slot is already created</param>
        /// <param name="getName">function that return slot name how it should be seen in the UI. Localization is recommended.</param>
        /// <param name="itemIsValid">function to check of item fits the slot</param>
        /// <param name="isActive">function to check if slot should be available in equipment panel. If you need live update - call UpdateSlots.</param>
        /// <param name="slotIDs">slot IDs to add the slot before</param>
        /// <returns></returns>
        public static bool AddSlotBefore(string slotID, Func<string> getName, Func<ItemDrop.ItemData, bool> itemIsValid, Func<bool> isActive, params string[] slotIDs)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeCustomSlot, "TryAddNewSlotBefore").Invoke(_typeCustomSlot, new object[] { slotIDs, slotID, getName, itemIsValid, isActive });
        }

        /// <summary>
        /// Adds new custom equipment slot with set position
        /// </summary>
        /// <param name="slotID">To add new slot ID should be unique. If given ID is not unique returns true if slot is already created</param>
        /// <param name="getName">function that return slot name how it should be seen in the UI. Localization is recommended.</param>
        /// <param name="itemIsValid">function to check of item fits the slot</param>
        /// <param name="isActive">function to check if slot should be available in equipment panel. If you need live update - call UpdateSlots.</param>
        /// <param name="slotIDs">slot IDs after which the slot should be added</param>
        /// <returns></returns>
        public static bool AddSlotAfter(string slotID, Func<string> getName, Func<ItemDrop.ItemData, bool> itemIsValid, Func<bool> isActive, params string[] slotIDs)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeCustomSlot, "TryAddNewSlotAfter").Invoke(_typeCustomSlot, new object[] { slotIDs, slotID, getName, itemIsValid, isActive });
        }

        /// <summary>
        /// Tries to remove custom slot with given ID
        /// </summary>
        /// <param name="slotID"></param>
        public static bool RemoveSlot(string slotID)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeCustomSlot, "TryRemoveSlot").Invoke(_typeCustomSlot, new object[] { slotID });
        }

        /// <summary>
        /// Calls an update to slots layout and equipment panel
        /// Should be called if slot active state was changed to update panel
        /// </summary>
        public static void UpdateSlots()
        {
            if (!IsReady())
                return;

            AccessTools.Method(_typeSlots, "UpdateSlotsGridPosition").Invoke(_typeSlots, null);
            AccessTools.Method(AccessTools.TypeByName("ExtraSlots.EquipmentPanel"), "UpdatePanel").Invoke(null, null);
        }
    }
}
