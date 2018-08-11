using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scoreLabel;
    public Harddrive hdd;

    public GameObject scorePrefab;
    public float score;

    public void AddPoints(File file) {
        ScorePopup popup = Instantiate(scorePrefab, file.transform.position, Quaternion.identity).GetComponent<ScorePopup>();
        popup.score = hdd.fileSizes[(int)file.type];
        score += hdd.fileSizes[(int)file.type];
    }

    void Update() {
        scoreLabel.text = string.Format("{0} points", score);
    }

}
