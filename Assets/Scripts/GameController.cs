using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public List<GameObject> textParserPrefabs = new List<GameObject>();
    public GameObject startButton;
    public GameObject nextWordButton;
    public GameObject pauseButton;
    public Vector3 letterSpawn = new Vector3();
    public List<GameObject> vowelPrefabs = new List<GameObject>();
    public List<GameController> consonantPrefabs = new List<GameController>();
    public TextMeshProUGUI word;
    public TextMeshProUGUI type;
    public TextMeshProUGUI paragraph;

    private GameObject textParserInstance;
    private string currentWord = "";
    private string currentWordType = "";
    private bool paused = false;
    private bool playing = false;
    private List<GameObject> vowels = new List<GameObject>(); private float maxVowels = 6;
    private List<GameObject> consonants = new List<GameObject>(); private float maxConsonants = 16;
    private float nextSpawnCheck = 0f; private float spawnInterval = 0.75f;

    
    void OnDrawGizmos() {

        Gizmos.DrawSphere(letterSpawn, 1f);

    }

    public bool pressButton(char character_) {

        if (currentWord.Length <= 12) {

            currentWord += character_;
            return true;

        }
        else {

            Debug.Log("Too many characters!");
            return false;

        }

    }

    public void nextWord() {

        if (currentWord != "") {

            textParserInstance.GetComponent<TextParser>().setInput(currentWord);
            currentWordType = textParserInstance.GetComponent<TextParser>().getInputType();
            currentWord = "";
            if (currentWordType == null) {

                gameComplete();

            }
            else {

                type.SetText(currentWordType);
                word.SetText(currentWord);

            }

        }

    }

    public void gameComplete() {

        textParserInstance.GetComponent<TextParser>().parse();
        Debug.Log(textParserInstance.GetComponent<TextParser>().parsedParagraph);
        paragraph.SetText(textParserInstance.GetComponent<TextParser>().parsedParagraph);
        type.SetText("");
        word.SetText("");


    }

    public void pauseGame() {

        if (!paused) {

            Time.timeScale = 1;

        }
        else {

            Time.timeScale = 0;

        }
        paused = !paused;

    }

    public void startGame() {

        if (textParserInstance != null) {

            Destroy(textParserInstance);

        }
        textParserInstance = Instantiate(textParserPrefabs[Random.Range(0,textParserPrefabs.Count)]);
        currentWord = "";
        currentWordType = textParserInstance.GetComponent<TextParser>().getInputType();
        paragraph.SetText("");
        word.SetText("");
        type.SetText(currentWordType);
        for (int a = vowels.Count - 1; a >= 0; a--) {

            Destroy(vowels[a]);

        }
        for (int a = consonants.Count - 1; a >= 0; a--) {

            Destroy(consonants[a]);

        }
        playing = true;
        Debug.Log("Starting Game");

    }

    void Update() {

        if (playing && Time.time > nextSpawnCheck) {

            nextSpawnCheck = Time.time + spawnInterval;
            if (vowels.Count < maxVowels) {

                Debug.Log("Spawning vowel");
                Instantiate(vowels[Random.Range(0,vowels.Count)], letterSpawn, Quaternion.identity);

            }
            else if (consonants.Count < maxConsonants) {

                Debug.Log("Spawning consonant");
                Instantiate(consonants[Random.Range(0,consonants.Count)], letterSpawn, Quaternion.identity);

            }

        }

    }

}
