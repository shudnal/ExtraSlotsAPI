﻿using BepInEx.Bootstrap;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraSlotsAPI
{
    public static class API
    {
        private static bool _isNotReady;
        private static readonly List<ItemDrop.ItemData> _emptyItemList = new List<ItemDrop.ItemData>();
        private static readonly List<ExtraSlot> _emptySlotList = new List<ExtraSlot>();

        internal static Type _typeAPI;
        internal static Type _typeSlot;

        public static bool IsReady()
        {
            if (_isNotReady)
                return false;

            if (_typeAPI != null)
                return true;

            _isNotReady = !Chainloader.PluginInfos.ContainsKey("shudnal.ExtraSlots");
            if (_isNotReady)
                return false;

            if (_typeAPI == null)
                _typeAPI = AccessTools.TypeByName("ExtraSlots.API");

            if (_typeSlot == null)
                _typeSlot = AccessTools.TypeByName("ExtraSlots.Slots+Slot");

            return _typeAPI != null;
        }

        /// <summary>
        /// Returns list of all slots
        /// </summary>
        public static List<ExtraSlot> GetExtraSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetExtraSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetEquipmentSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetEquipmentSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetQuickSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetQuickSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetFoodSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetFoodSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetAmmoSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetAmmoSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns list of corresponding slots
        /// </summary>
        public static List<ExtraSlot> GetMiscSlots()
        {
            if (!IsReady())
                return _emptySlotList;

            return ((IEnumerable<object>)AccessTools.Method(_typeAPI, "GetMiscSlots").Invoke(null, null)).Select(slot => slot.ToExtraSlot()).ToList();
        }

        /// <summary>
        /// Returns slot with given ID
        /// </summary>
        /// <param name="slotID"></param>
        public static ExtraSlot FindSlot(string slotID)
        {
            if (!IsReady())
                return null;

            return AccessTools.Method(_typeAPI, "FindSlot").Invoke(null, new object[] { slotID }).ToExtraSlot();
        }

        /// <summary>
        /// Returns list of items located in extra slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetAllExtraSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetAllExtraSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetEquipmentSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetEquipmentSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetQuickSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetQuickSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetFoodSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetFoodSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetAmmoSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetAmmoSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns list of items located in corresponding slots
        /// </summary>
        /// <returns>List of not null ItemDrop.ItemData</returns>
        public static List<ItemDrop.ItemData> GetMiscSlotsItems()
        {
            if (!IsReady())
                return _emptyItemList;

            return (List<ItemDrop.ItemData>)AccessTools.Method(_typeAPI, "GetMiscSlotsItems").Invoke(null, null);
        }

        /// <summary>
        /// Returns amount of extra rows added to player available inventory
        /// </summary>
        public static int GetExtraRows()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Method(_typeAPI, "GetExtraRows").Invoke(null, null);
        }

        /// <summary>
        /// Returns full height of inventory
        /// </summary>
        public static int GetInventoryHeightFull()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Method(_typeAPI, "GetInventoryHeightFull").Invoke(null, null);
        }

        /// <summary>
        /// Returns full height of inventory
        /// </summary>
        public static int GetInventoryHeightPlayer()
        {
            if (!IsReady())
                return -1;

            return (int)AccessTools.Method(_typeAPI, "GetInventoryHeightPlayer").Invoke(null, null);
        }

        /// <summary>
        /// Returns if given position is extra slot
        /// </summary>
        /// <param name="gridPos">Position in inventory grid</param>
        public static bool IsGridPositionASlot(Vector2i gridPos)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeAPI, "IsGridPositionASlot").Invoke(null, new object[] { gridPos });
        }

        /// <summary>
        /// Returns if given item is in extra slot
        /// </summary>
        /// <param name="item"></param>
        public static bool IsItemInSlot(ItemDrop.ItemData item)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeAPI, "IsItemInSlot").Invoke(null, new object[] { item });
        }

        /// <summary>
        /// Returns if given item is in equipment slot
        /// </summary>
        /// <param name="item"></param>
        public static bool IsItemInEquipmentSlot(ItemDrop.ItemData item)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeAPI, "IsItemInEquipmentSlot").Invoke(null, new object[] { item });
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

            return (bool)AccessTools.Method(_typeAPI, "AddSlot").Invoke(null, new object[] { slotID, -1, getName, itemIsValid, isActive });
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

            return (bool)AccessTools.Method(_typeAPI, "AddSlotWithIndex").Invoke(null, new object[] { slotID, slotIndex, getName, itemIsValid, isActive });
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

            return (bool)AccessTools.Method(_typeAPI, "AddSlotBefore").Invoke(null, new object[] { slotIDs, slotID, getName, itemIsValid, isActive });
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

            return (bool)AccessTools.Method(_typeAPI, "AddSlotAfter").Invoke(null, new object[] { slotIDs, slotID, getName, itemIsValid, isActive });
        }

        /// <summary>
        /// Tries to remove custom slot with given ID
        /// </summary>
        /// <param name="slotID"></param>
        public static bool RemoveSlot(string slotID)
        {
            if (!IsReady())
                return false;

            return (bool)AccessTools.Method(_typeAPI, "RemoveSlot").Invoke(null, new object[] { slotID });
        }

        /// <summary>
        /// Calls an update to slots layout and equipment panel
        /// Should be called if slot active state was changed to update panel
        /// </summary>
        public static void UpdateSlots()
        {
            if (!IsReady())
                return;

            AccessTools.Method(_typeAPI, "UpdateSlots").Invoke(null, null);
        }
    }
}
