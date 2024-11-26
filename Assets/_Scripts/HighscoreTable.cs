using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    public Transform highscoreContainer;
    public Transform entryTemplate;
    public Text loadingText;

    private HighscoreClient _highscoreClient = new HighscoreClient();
    private List<HighscoreEntryDto> _highscore;
    private long _currentRank = 0;
    public Inventory _inventory;

    private void OnDisable()
    {
        loadingText.text = "Loading...";
        foreach (Transform child in highscoreContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private async void OnEnable()
    {
        var characterHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        entryTemplate.gameObject.SetActive(false);
        var userName = GameManager.Instance.GetComponent<UsernameHandling>().GetUsername();

        var coins = 0;
        if (_inventory != null)
        {
            coins = _inventory.GetQuantity("Coin");
        }

        if (coins > 0 && characterHealth.CurrentHealth > 0)
        {   
            _currentRank = await _highscoreClient.SendHighscore(userName, coins);
        }
        
        _highscore = await _highscoreClient.GetHighScore();

        var templateHeight = 35f;

        if (_highscore == null)
        {
            loadingText.text += "failed!";
        }
        else
        {
            loadingText.gameObject.SetActive(false);
            for (int i = 0; i < _highscore.Count; i++)
            {
                var rank = i + 1;
                var entryTransform = Instantiate(entryTemplate, highscoreContainer);
                var entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
                entryTransform.gameObject.SetActive(true);

                if (rank == _currentRank)
                {
                    entryTransform.Find("Rank").GetComponent<Text>().color = new Color(255, 196, 0);
                    entryTransform.Find("Score").GetComponent<Text>().color = new Color(255, 196, 0); ;
                    entryTransform.Find("Name").GetComponent<Text>().color = new Color(255, 196, 0); ;
                }

                entryTransform.Find("Rank").GetComponent<Text>().text = rank.ToString() + ".";
                entryTransform.Find("Score").GetComponent<Text>().text = _highscore[i].Score.ToString();
                entryTransform.Find("Name").GetComponent<Text>().text = _highscore[i].Name;
            }
        }

        if (_currentRank > 10)
        {
            var entryTransform = Instantiate(entryTemplate, highscoreContainer);
            var entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * 12);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("Rank").GetComponent<Text>().color = new Color(255, 196, 0);
            entryTransform.Find("Score").GetComponent<Text>().color = new Color(255, 196, 0);
            entryTransform.Find("Name").GetComponent<Text>().color = new Color(255, 196, 0);
            entryTransform.Find("Rank").GetComponent<Text>().text = _currentRank.ToString() + ".";
            entryTransform.Find("Score").GetComponent<Text>().text = coins.ToString();
            entryTransform.Find("Name").GetComponent<Text>().text = userName;
        }
    }
}
