using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public Transform ProgressBarPlane;
    public float Progress = 0.5f;

    private float maxScale;
    private float currentProgress;
    private float boundsOnX;

    // Start is called before the first frame update
    void Start()
    {
        maxScale = ProgressBarPlane.localScale.x;
        currentProgress = 1;
        var mesh = ProgressBarPlane.GetComponent<MeshFilter>();
        boundsOnX = mesh.mesh.bounds.max.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Progress < 0)
            Progress = 0;
        else if (Progress > 1)
            Progress = 1;
        
        if (Progress == currentProgress)
            return;

        ProgressBarPlane.localScale = new Vector3(maxScale * Progress, ProgressBarPlane.localScale.y, ProgressBarPlane.localScale.z);
        var prevLokal = ProgressBarPlane.transform.localPosition;

        ProgressBarPlane.transform.localPosition = new Vector3(0 - (boundsOnX * (1 - Progress)), prevLokal.y, prevLokal.z);

        currentProgress = Progress;
    }
}
