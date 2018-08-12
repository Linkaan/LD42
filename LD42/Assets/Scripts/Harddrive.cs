using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum FileType {
    Image = 0,
    Video,
    Document,
    Music,
    Worm
}

public class Harddrive : MonoBehaviour {

    public Sprite[] fileSprites;
    public float[] fileSizes; // in MB
    public float totalSize; // in MB

    public SoundPlayer sfxPlayer;
    public Slider spaceUsedSlider;
    public TextMeshProUGUI spaceUsedText;
    public ScoreManager scoreMgr;
    public Selector selector;
    public Spawner spawner;
    public GameObject gameOverMenu;
    public bool gameOver;

    public List<File> files;

    public float spaceUsed;
    public float spaceFree;

    public void AddFile(File file) {
        if (file.type == FileType.Worm) {
            sfxPlayer.PlaySFX(sfxPlayer.wormSFX);
        }
        files.Add(file);
    }

    public void DeleteFile(File file) {
        sfxPlayer.PlaySFX(sfxPlayer.saveFileSFX);
        scoreMgr.AddPoints(file);
        files.Remove(file);
    }

    void Update() {
        if (gameOver) return;
        spaceUsed = 0;

        foreach (File file in files) {
            spaceUsed += fileSizes[(int)file.type];
        }

        spaceFree = totalSize - spaceUsed;

        spaceUsedSlider.value = spaceUsed / totalSize;
        spaceUsedText.text = string.Format("{0} MB / {1} MB", spaceUsed, totalSize);

        if (spaceFree <= 0) {
            gameOver = true;
            Destroy(selector);
            Destroy(spawner);
            gameOverMenu.SetActive(true);
            sfxPlayer.PlaySFX(sfxPlayer.loseSFX);
        }
    }
	
}
