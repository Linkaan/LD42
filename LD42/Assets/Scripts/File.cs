using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class File : MonoBehaviour {

    public FileType type;
    public Harddrive hdd;

    public SpriteRenderer sr;

    public Vector3 desktopPosition;
    public Vector3 currentPosition;
    public float zPosition;

    public bool isSelected;

    void Start() {
        isSelected = false;
        hdd = FindObjectOfType<Harddrive>();
        desktopPosition = currentPosition = transform.position;
        zPosition = desktopPosition.z;
        sr.color = hdd.fileColours[(int)type];

        Vector3 newPosition;
        if (GetNearestAvailable(currentPosition, 10, out newPosition)) {
            currentPosition = newPosition;
        } else {
            currentPosition = desktopPosition;
        }
        transform.position = currentPosition;

        hdd.AddFile(this);
    }

    float Nearest(float num, float roundTo) {
        return Mathf.Round(num / roundTo) * roundTo;
    }

    Vector3 GetNearest(Vector3 position) {
        Vector3 nearest = new Vector3(Nearest(position.x, 0.5f), Nearest(position.y, 0.5f))
        {
            z = zPosition
        };
        return nearest;
    }

    bool IsBlocked(Vector3 position) {
        RaycastHit2D[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));

        hits = Physics2D.RaycastAll(ray.origin, ray.direction, 10f);

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("File")) {
                return true;
            }
        }

        return false;
    }

    bool GetNearestOffset(Vector3 position, Vector3 offset, out Vector3 nearest) {
        nearest = position;

        Vector3 currentNearest = GetNearest(position + offset);
        if (!IsBlocked(currentNearest)) {
            nearest = currentNearest;
            return true;
        }

        return false;
    }

    public bool GetNearestAvailable(Vector3 position, int depth, out Vector3 nearest) {
        nearest = position;
        for (int iter = 0; iter <= depth; iter++) {
            if (GetNearestOffset(position, Vector3.zero, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.up * iter * 0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.up * iter * -0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.right * iter * 0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.right * iter * -0.5f, out nearest)) return true;

            if (GetNearestOffset(position, Vector3.up * iter * 0.5f + Vector3.right * iter * 0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.up * iter * -0.5f + Vector3.right * iter * 0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.up * iter * 0.5f + Vector3.right * iter * -0.5f, out nearest)) return true;
            if (GetNearestOffset(position, Vector3.up * iter * -0.5f + Vector3.right * iter * -0.5f, out nearest)) return true;
        }
        return false;
    }
}
