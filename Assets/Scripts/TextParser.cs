using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParser : MonoBehaviour
{

    public bool autoParse = false;
    public string paragraph = "";
    public List<textInput> words = new List<textInput>();
    [TextArea(100,100)]
    public string parsedParagraph = "";
    int currentInput = 0;

    string defaultColor = "black";
    string filledColor = "green";
    string emptyColor = "red";

    public void parse() {

        parsedParagraph = paragraph;
        words.Sort(sortByIndex);
        for (int a = words.Count - 1; a >= 0; a--) {

            if (words[a].text == "") {

                parsedParagraph = parsedParagraph.Insert(words[a].index, "<color=" + emptyColor + ">[" + words[a].wordType + "]</color>");

            }
            else {

                parsedParagraph = parsedParagraph.Insert(words[a].index, "<color=" + filledColor + ">" + words[a].text + "</color>");

            }

        }
        parsedParagraph = "<color=" + defaultColor + ">" + parsedParagraph;

    }

    public string getInputType() {

        if (words.Count > 0 && currentInput < words.Count) {

            return words[currentInput].wordType;

        }
        else {

            return null;

        }

    }

    public void setInput(string input_) {

        if (words.Count > 0 && currentInput < words.Count) {

            words[currentInput].text = input_;
            currentInput++;

        }

    }

    public string getParagraph() {

        parse();
        return parsedParagraph;

    }

    static int sortByIndex(textInput first_, textInput second_) {

        return (first_.index.CompareTo(second_.index));

    } 

}

[System.Serializable]
public class textInput {

    public string wordType = "";
    public int index = 0;
    public string text = "";

}
