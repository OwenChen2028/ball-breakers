using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowText : MonoBehaviour
{
    public GameObject Streak;
    public GameObject Phrase;
    public GameObject Time;
    public GameObject Money;

    public string Phrase1;
    public string Phrase2;
    public string Phrase3;
    public string Phrase4;
    public string Phrase5;
    public string Phrase6;

    TextMeshProUGUI PhraseText;
    TextMeshProUGUI StreakText;
    TextMeshProUGUI TimeText;
    TextMeshProUGUI MoneyText;

    public GameObject Player;
    public GameObject GameManager;

    void Start()
    {
        PhraseText = Phrase.GetComponent<TextMeshProUGUI>();
        StreakText = Streak.GetComponent<TextMeshProUGUI>();
        TimeText = Time.GetComponent<TextMeshProUGUI>();
        MoneyText = Money.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Player != null)
        {
            if (Player.GetComponent<PlayerController>().streak == 0)
            {
                PhraseText.text = Phrase1;
            }
            else if (Player.GetComponent<PlayerController>().streak == 10)
            {
                PhraseText.text = Phrase2;
            }
            else if (Player.GetComponent<PlayerController>().streak == 20)
            {
                PhraseText.text = Phrase3;
            }
            else if (Player.GetComponent<PlayerController>().streak == 30)
            {
                PhraseText.text = Phrase4;
            }
            else if (Player.GetComponent<PlayerController>().streak == 40)
            {
                PhraseText.text = Phrase5;
            }
            else if (Player.GetComponent<PlayerController>().streak == 50)
            {
                PhraseText.text = Phrase6;
            }

            StreakText.text = "Score: " + ((int)Player.GetComponent<PlayerController>().streak).ToString();
            TimeText.text = "Time: " + ((int) GameManager.GetComponent<GameManager>().time).ToString();
            MoneyText.text = "Coins: " + ((int)Player.GetComponent<PlayerController>().money).ToString();
        }
    }
}