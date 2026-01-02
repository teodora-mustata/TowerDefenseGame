using UnityEngine;

public class EncyclopediaModelPreview : MonoBehaviour
{
    public Transform previewSpawnPoint;
    public Camera previewCamera; // Camera de preview
    private GameObject currentPreview;

    public void ShowModel(GameObject prefab)
    {
        Clear();

        currentPreview = Instantiate(prefab, previewSpawnPoint);

        currentPreview.transform.localPosition = Vector3.zero;
        currentPreview.transform.localRotation = Quaternion.Euler(0, 45, 0);
        currentPreview.transform.localScale = Vector3.one;

        SetLayerRecursively(currentPreview, LayerMask.NameToLayer("Preview"));
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform t in obj.GetComponentsInChildren<Transform>())
            t.gameObject.layer = layer;
    }

    void Clear()
    {
        if (currentPreview != null)
            Destroy(currentPreview);
    }

    void Update()
    {
        if (currentPreview != null)
            currentPreview.transform.Rotate(Vector3.up * 20f * Time.deltaTime);
    }

    void Start()
    {
        PositionCamera();
    }

    void PositionCamera()
    {
        if (previewCamera != null && previewSpawnPoint != null)
        {
            previewCamera.transform.position = previewSpawnPoint.position + new Vector3(0, 1, -5);
            previewCamera.transform.LookAt(previewSpawnPoint.position);
        }
    }
}
