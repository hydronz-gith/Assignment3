using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEditor.Rendering;

// This has been 'quarantined'; old code. 

#if FALSE
public class ItemManager : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private TMP_Text itemName;

        InventoryDescription description;

        private string currentItemName = "";

        public event Action<ItemManager> OnItemClicked,
        OnRightMouseBtnClick;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        private void ResetData()
        {
            // This hides the image; may not be necessary
            itemImage.gameObject.SetActive(false);
            quantityTxt.text = "";
            currentItemName = "";
            empty = true;
        }

        private void Deselect()
        {
            if (!empty)
        {
            itemName.SetText(currentItemName);
        }
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            currentItemName = name;

            if(itemName != null)
        {
            itemName.SetText(name);
        }
            empty = false;
        }

        public void Select()
        {
            if (empty) return;

            if(itemName != null)
        {
            itemName.SetText($"> {currentItemName}");
        }
 
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {                
                Debug.Log($"[ItemManager] Current Stored name is {currentItemName}");
                Select();
                OnItemClicked?.Invoke(this);
            }
        }
    }
#endif
