using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folder : MonoBehaviour {

    public FileType type;
    public Harddrive hdd;

    public SpriteRenderer srName;

    void Start () {
        hdd = FindObjectOfType<Harddrive>();
        srName.color = hdd.fileColours[(int)type];
    }

    public bool AddFile(File file) {
        if (file.type == type) {
            hdd.DeleteFile(file);
            Destroy(file.gameObject);
            return true;
        }

        return false;
    }
}
