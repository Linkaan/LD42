using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject filePrefab;
    public float[] spawnRates;
    public Harddrive hdd;

    public float spawnInterval;
    public float spawnIntervalFactor;
    public float spawnIntervalFactorDelta;
    public float spawnChanceRandom;
    public int initialSpawn;

    public int minWormSpread;
    public int maxWormSpread;

    public float zPosition;
    public float spawnX;
    public float spawnY;
    public GameObject plane;

    private float lastSpawnTick;

    void Start () {
        for (int i = 0; i < initialSpawn; i++) SpawnFile();
    }

    public Vector3 RandomPosition() {
        Vector3 newVec = plane.transform.position + 
                         new Vector3(Random.Range(-spawnX, spawnX),
                                     Random.Range(-spawnY, spawnY),
                                     zPosition);
        return newVec;
    }

    void SpawnFile() {
        File newFile = Instantiate(filePrefab, RandomPosition(), Quaternion.identity).GetComponent<File>();
        float sum = 0, random = Random.value;
        for (int type = 0; type < spawnRates.Length; type++) {
            sum += spawnRates[type];
            if (random <= sum) {
                newFile.type = (FileType)type;
                break;
            }
        }
    }
	
	void Update () {
        if (Time.time - lastSpawnTick > spawnInterval * spawnIntervalFactor) {
            lastSpawnTick = Time.time;
            SpawnFile();

            foreach (File file in hdd.files) {
                if (file.type == FileType.Worm && !file.isSelected) {
                    int wormSpread = Random.Range(minWormSpread, maxWormSpread + 1);
                    for (int i = 0; i < wormSpread; i++) {
                        File newFile = Instantiate(filePrefab, file.transform.position, Quaternion.identity).GetComponent<File>();
                        newFile.type = Random.value < 0.1 ? FileType.Worm : FileType.Document;
                    }
                }
            }
        }

        lastSpawnTick -= Time.deltaTime * (Random.value * spawnChanceRandom / spawnIntervalFactorDelta);

        if (spawnIntervalFactor > 2.0f) {
            spawnIntervalFactor -= spawnIntervalFactorDelta * Time.deltaTime;
        } else {
            spawnIntervalFactor = 2.0f;
        }
	}
}
