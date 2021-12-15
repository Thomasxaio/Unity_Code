using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sample : MonoBehaviour {

    public CardDeckInterface cardDeck;

    public Text selectedCardText;

    private string sampleDescription;
    	
    void Start()
    {

        sampleDescription = selectedCardText.text;

        /*
         * 您可以使用 OnCardSelected 回調
          * 如果您願意等待動畫完成
          * 或 Update() 函數中在用戶單擊卡片後立即獲取索引的方式
         */
        cardDeck.OnCardSelected = OnCardChanged;
    }

    void OnCardChanged(int cardIndex)
    {
        Card currentCard = cardDeck.GetCurrentCard();

        if (currentCard != null)
            selectedCardText.text = sampleDescription + currentCard.tooltipMessage;
    }

	// Update is called once per frame
	void Update () {

        /*
          *您可以使用 OnCardSelected 回調
          * 如果您願意等待動畫完成
          * 或下面的方式在用戶點擊卡片後立即獲取索引
         */
        //Card currentCard = cardDeck.GetCurrentCard();

        //if (currentCard != null)
        //    selectedCardText.text = sampleDescription + currentCard.tooltipMessage;

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            cardDeck.SetCurrentIndex(cardDeck.GetCurrentIndex() - 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            cardDeck.SetCurrentIndex(cardDeck.GetCurrentIndex() + 1);
        }
    }
}
