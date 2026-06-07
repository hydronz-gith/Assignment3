using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

// Referred to as UIInventoryPage in the tutorials

    public class InventoryHandler : MonoBehaviour
    {
        InputManager inputmanager;

        [Header("Menu")]
        public GameObject inventoryMenu;

        [SerializeField]
        // Referred to as UIInventoryItem in the tutorials
        private ItemManager itemPrefab;
        
        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        // Referred to as UIInventoryDescription in the tutorials
        private InventoryDescription itemDescription;

        List<ItemManager> listofItems = new List<ItemManager>();

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested;

        [SerializeField]
        private ItemActionPanel actionPanel;

        private void Awake()
        {
            Hide();
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ItemManager uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = Vector3.one;
                listofItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in listofItems)
            {
                itemPrefab.ResetData();
                itemPrefab.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            listofItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listofItems.Count > itemIndex)
            {
                listofItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(ItemManager inventoryItemUI)
        {
            int index = listofItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleItemSelection(ItemManager inventoryItemUI)
        {
            int index = listofItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            inventoryMenu.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButon(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listofItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (ItemManager item in listofItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            inventoryMenu.SetActive(false);
    }
}
