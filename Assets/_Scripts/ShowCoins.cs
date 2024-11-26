using MoreMountains.InventoryEngine;
using UnityEngine;
using UnityEngine.UI;

public class ShowCoins : MonoBehaviour
{
    public float UpdateIntervalInSeconds = 0.5f;
    public Inventory _inventory;
    
    private float timeLeft;
    private Text text;
    private int lastNumberOfCoins = -1;

    protected void Start()
    {
        if (GetComponent<Text>() == null)
        {
            Debug.LogWarning("ShowCoins requires a GUIText component.");
            return;
        }

        text = GetComponent<Text>();
        
        timeLeft = UpdateIntervalInSeconds;
    }

    protected void Update()
    {
        timeLeft = timeLeft - Time.deltaTime;

        if (timeLeft <= 0.0)
        {
            timeLeft = UpdateIntervalInSeconds;
           
            var actualCount = _inventory.GetQuantity("Coin");
            if (lastNumberOfCoins != actualCount)
            {
                lastNumberOfCoins = actualCount;
                text.text = string.Format("{0:D4}", lastNumberOfCoins);
            }
        }
    }
}