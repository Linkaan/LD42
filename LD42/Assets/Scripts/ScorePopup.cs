using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour {

    public float score;

    public float alphaDelta;
    public float yDelta;
    public float delay;
    public TextMeshPro scoreLabel;

    void Start () {
        scoreLabel.text = string.Format("+{0}", score);
        Destroy(gameObject, delay);
    }

    void Update () {
        scoreLabel.alpha -= alphaDelta * Time.deltaTime;
        transform.Translate(Vector3.up * yDelta);
    }
}
