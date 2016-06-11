using UnityEngine;
using System.Collections;

public class PlantModifier : MonoBehaviour {

    private float sizeModifier = 0.0F;
    private Transform plant;

    void Start()
    {
        plant = GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other) {
        sizeModifier += 0.01F;
        if (plant && other.gameObject.CompareTag("PlayerControlled") && sizeModifier < 2) {
            Debug.Log(sizeModifier);
            float scaleX = plant.transform.localScale.x + sizeModifier;
            float scaleY = plant.transform.localScale.y + sizeModifier;
            float scaleZ = plant.transform.localScale.z + sizeModifier;
            plant.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}
