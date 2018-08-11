using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public LayerMask layerMask;

    private bool dragging;
    private Collider2D hitCollider;

    private File currentFile;

	public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);

            if (hit.collider && hit.collider.CompareTag("File")) {
                currentFile = hit.collider.GetComponent<File>();
                currentFile.isSelected = true;
                dragging = true;
            }
        }

        if (dragging) {
            RaycastHit2D hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, layerMask);

            if (hit) {
                hitCollider = hit.collider;
                currentFile.currentPosition = hit.point;
                currentFile.currentPosition.z = currentFile.zPosition;
            } else {
                hitCollider = null;
            }

            currentFile.transform.position = currentFile.currentPosition;
        }

        if (Input.GetMouseButtonUp(0) && dragging) {
            dragging = false;
            currentFile.isSelected = false;
            if (hitCollider) {
                if (hitCollider.CompareTag("Folder")) {
                    bool succeeded = hitCollider.GetComponent<Folder>().AddFile(currentFile);
                    if (!succeeded) {
                        currentFile.transform.position = currentFile.currentPosition = currentFile.desktopPosition;
                    } else {
                        hitCollider = null;
                    }
                } else {
                    Vector3 newPosition;
                    if (currentFile.GetNearestAvailable(currentFile.currentPosition, 1, out newPosition)) {
                        currentFile.currentPosition = newPosition;
                    } else {
                        currentFile.currentPosition = currentFile.desktopPosition;
                    }
                    currentFile.transform.position = currentFile.currentPosition;
                }
            } else {
                currentFile.desktopPosition = currentFile.currentPosition;
            }
        }
    }
}
