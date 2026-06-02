using UnityEngine;


    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField]
        private InventoryHandler inventoryUI;

        public int inventorySize = 10;

        private void Start()
        {
            inventoryUI.InitializeInventoryUI(inventorySize);
        }
    }

