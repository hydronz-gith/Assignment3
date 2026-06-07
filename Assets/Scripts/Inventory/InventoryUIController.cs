using UnityEngine;


    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField]
            private InventoryHandler inventoryUI;

        [SerializeField]
            private InventorySO inventoryData;

            //InputManager inputManager;

        private void Start()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryData.Initialize();
        }
        //public void Update()
        //{
            //if (inventoryMenu.activeSelf == true)

            //foreach (var item in inventoryData.GetCurrentInventoryState())
            //{
            //    inventoryUI.UpdateData(item.Key,
            //        item.Value.item.ItemImage,
            //        item.Value.quantity);
            //}
        //}
    }

