using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{

    public List<string> items;
    public GameObject buttonPrefab;

    public RectTransform scrollViewContent;

    public float buttonHeight = 50f;
    public float spacing = 10f;


    void Start()
    {
        float contentHeight = items.Count * (buttonHeight + spacing) - spacing;
        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, contentHeight);

        for (int i = 0; i < items.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, scrollViewContent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = items[i];

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = items[i];


            RectTransform buttonRectTransform = newButton.GetComponent<RectTransform>();
            buttonRectTransform.anchoredPosition = new Vector2(0, -i * (buttonHeight + spacing));

            string currentItem = items[i];
            newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonText));

        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnButtonClick(TextMeshProUGUI buttonText)
    {
        if (!buttonText.text.Contains("<s>"))
        {
            buttonText.text = $"<s>{buttonText.text}</s>";


        }

    }
}