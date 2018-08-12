using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    public TextMeshProUGUI scoreLabel;
    public Harddrive hdd;

    public GameObject scorePrefab;
    public float score;

    public float timeAlive;
    public int filesDeleted;

    private float startTime;

    void Start() {
        startTime = Time.time;
        filesDeleted = 0;
    }

    public void AddPoints(File file) {
        filesDeleted++;
        ScorePopup popup = Instantiate(scorePrefab, file.transform.position, Quaternion.identity).GetComponent<ScorePopup>();
        popup.score = hdd.fileSizes[(int)file.type];
        score += hdd.fileSizes[(int)file.type];
    }

    void Update() {
        if (hdd.gameOver) return;

        timeAlive = Time.time - startTime;
        scoreLabel.text = string.Format("{0} points", score);
    }

}
