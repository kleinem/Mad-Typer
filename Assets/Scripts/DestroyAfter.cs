using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{

    public float whenToDestroy = 6f;

    void Start() {

        Destroy(this.gameObject, whenToDestroy);

    }

}
