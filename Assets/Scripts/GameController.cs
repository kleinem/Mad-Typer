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
    public List<GameObject> consonantPrefabs = new List<GameObject>();
    public TextMeshProUGUI word;
    public TextMeshProUGUI type;
    public TextMeshProUGUI paragraph;
    public AudioSource click;
    public AudioSource tap;
    public GameObject popPrefab;

    private GameObject textParserInstance;
    private string currentWord = "";
    private string currentWordType = "";
    private bool paused = false;
    private bool playing = false;
    private List<GameObject> vowels = new List<GameObject>(); private float maxVowels = 6;
    private List<GameObject> consonants = new List<GameObject>(); private float maxConsonants = 16;
    private float nextSpawnCheck = 0f; private float spawnInterval = 0.75f;

    void Start() {

        paragraph.SetText("");
        word.SetText("");
        type.SetText("");
        Camera.main.GetComponent<AudioSource>().Play();

    }

    void OnDrawGizmos() {

        Gizmos.DrawSphere(letterSpawn, 1f);

    }

    public bool pressButton(char character_) {

        if (currentWord.Length <= 12) {

            currentWord += character_;
            word.SetText(currentWord);
            tap.Play();
            return true;

        }
        else {

            Debug.Log("Too many characters!");
            return false;

        }

    }

    public void nextWord() {

        click.Play();
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
        playing = false;

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

        click.Play();
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

            vowels[a].GetComponent<LetterController>().explode();

        }
        vowels.Clear();
        for (int a = consonants.Count - 1; a >= 0; a--) {

            consonants[a].GetComponent<LetterController>().explode();

        }
        consonants.Clear();
        playing = true;
        nextSpawnCheck = Time.time;
        Debug.Log("Starting Game");

    }

    public void removeSelf(GameObject objectToRemove_) {

        if (vowels.Contains(objectToRemove_)) {

            vowels.Remove(objectToRemove_);

        }
        else if (consonants.Contains(objectToRemove_)) {

            consonants.Remove(objectToRemove_);

        }

    }

    void Update() {

        if (playing && Time.time > nextSpawnCheck) {

            nextSpawnCheck = Time.time + spawnInterval;
            if (vowels.Count < maxVowels) {

                Debug.Log("Spawning vowel");
                vowels.Add(Instantiate(vowelPrefabs[Random.Range(0,vowelPrefabs.Count)], letterSpawn, Quaternion.identity));

            }
            else if (consonants.Count < maxConsonants) {

                Debug.Log("Spawning consonant");
                consonants.Add(Instantiate(consonantPrefabs[Random.Range(0,consonantPrefabs.Count)], letterSpawn, Quaternion.identity));

            }

        }

    }

}
