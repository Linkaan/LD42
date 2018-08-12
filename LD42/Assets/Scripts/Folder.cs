using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder : MonoBehaviour {

    public FileType type;
    public Harddrive hdd;

    void Start () {
        hdd = FindObjectOfType<Harddrive>();
    }

    public bool AddFile(File file) {
        if (file.type == type) {
            hdd.DeleteFile(file);
            Destroy(file.gameObject);
            return true;
        }

        hdd.sfxPlayer.PlaySFX(hdd.sfxPlayer.failSFX);
        return false;
    }
}
