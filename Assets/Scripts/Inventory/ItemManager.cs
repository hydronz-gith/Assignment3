using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEditor.Rendering;

public class ItemManager : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityTxt;

        [SerializeField]
        private TMP_Text itemName;

        public event Action<ItemManager> OnItemClicked, OnRightMouseBtnClick;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            // This hides the image; may not be necessary
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            quantityTxt.text = "";
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";

            empty = false;
        }

        public void Select()
        {
            if (empty) return;

            if(itemName != null)
        {
            itemName.SetText($"> {itemName}");
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
                Debug.Log($"[ItemManager] Current Stored name is {itemName}");
                Select();
                OnItemClicked?.Invoke(this);
            }
        }
    }

