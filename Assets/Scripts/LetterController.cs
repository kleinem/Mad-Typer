using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{

    public char character;

    private GameController gc;
    private Rigidbody2D rb;
    private float nextJolt = 0f; private Vector2 joltRange = new Vector2(0.5f, 1.5f);
    private Vector2 joltStrength = new Vector2(100f, 1000f);
    private Vector2 destroyAfter = new Vector2(20f, 30f);
    private float killAt = 0f;

    void Start() {

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        character = gameObject.name[0];
        killAt = Time.time + Random.Range(destroyAfter[0], destroyAfter[1]);

    }

    void Update() {

        if (Time.time >= nextJolt) {

            nextJolt = Time.time + Random.Range(joltRange[0], joltRange[1]);
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(joltStrength[0], joltStrength[1]));

        }
        if (Time.time >= killAt) {

            explode();

        }

    }

    public void click() {

        if (gc.pressButton(character)) {

            explode();

        }

    }

    public void explode() {

        Instantiate(gc.popPrefab, transform.position, Quaternion.identity);
        gc.removeSelf(this.gameObject);
        Destroy(this.gameObject);

    }

}
