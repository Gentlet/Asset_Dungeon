using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxUI : MonoBehaviour
{
    public GameObject TextBox;
    GameObject NextBtn;
    public Image Illust_Image;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;
    public Sprite[] Illusts;

    public int currentLine;
    public int endAtLine; 

    void Start()
    {
        NextBtn = TextBox.transform.GetChild(2).GetChild(0).gameObject;
        Illust_Image = TextBox.transform.GetChild(4).GetComponent<Image>();

        if(textFile != null)
        {
            textLines = (textFile.text.Split('/'));
        }
        
        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
        theText.text = textLines[currentLine];  
    }

    public void ClickNextBtn()
    {
        currentLine += 1;

        if (currentLine > endAtLine - 1)
        {
            NextBtn.SetActive(false);
        }
    }

    public void ClickQuitBtn()
    {
        Destroy(gameObject);
    }
}


