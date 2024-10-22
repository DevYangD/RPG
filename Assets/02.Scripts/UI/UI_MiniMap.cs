using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_MiniMap : MonoBehaviour
{
    [SerializeField]
    private Camera          minimapCamera;
    [SerializeField]
    private float           zoomMin = 1f;
    [SerializeField]
    private float           zoomMax = 30f;
    [SerializeField]
    private float           zoomOnestep = 1;
    [SerializeField]
    private TextMeshProUGUI textMapName;
    MiniMapController minimapctrl;

    private void Awake()
    {
        textMapName.text = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        minimapctrl = GetComponent<MiniMapController>();
    }
    public void ZoomIn()
    {
        minimapctrl.camSize = Mathf.Max(minimapCamera.orthographicSize - zoomOnestep, zoomMin);
    }

    public void ZoomOut()
    {
        minimapctrl.camSize = Mathf.Max(minimapCamera.orthographicSize + zoomOnestep, zoomMax);
    }

}
