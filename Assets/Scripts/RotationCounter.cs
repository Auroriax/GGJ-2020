using UnityEngine;

public class RotationCounter : MonoBehaviour
{
    public float SignalOnRotationDegrees = 180;
    public DetectRotationAx DetectRotationAx = DetectRotationAx.Y;
    public bool UseLocalRotation = false;

    public ProgressBar ProgressBar;
    public GameStateController GameStateController;

    private bool detectPositiveMotion;
    private float detectedRotation = 0;
    private Vector3 previous;

    // Start is called before the first frame update
    void Start()
    {
        previous = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current;
        if (!UseLocalRotation)
            current = this.transform.rotation.eulerAngles;
        else
            current = this.transform.localEulerAngles;
        if (current == previous)
            return;

        float currentAx;
        float previousAx;
        if (DetectRotationAx == DetectRotationAx.X)
        {
            currentAx = current.x;
            previousAx = previous.x;
        }
        else if (DetectRotationAx == DetectRotationAx.Y)
        {
            currentAx = current.y;
            previousAx = previous.y;
        }
        else
        {
            currentAx = current.z;
            previousAx = previous.z;
        }

        var recalculatedPrevious = previousAx;
        if (currentAx < 90 && previousAx >= 270)
            recalculatedPrevious -= 360;
        else if (previousAx < 90 && currentAx >= 270)
            recalculatedPrevious += 360;

        detectedRotation += currentAx - recalculatedPrevious;
        var positiveRotation = detectedRotation > 0 ? detectedRotation : detectedRotation * -1;

        previous = current;

        if (ProgressBar != null)
        {
            ProgressBar.Progress = positiveRotation / SignalOnRotationDegrees;
        }
        if (GameStateController != null)
        {
            if (positiveRotation > SignalOnRotationDegrees)
                GameStateController.LevelCompleted();
        }
    }
}

public enum DetectRotationAx
{
    X,
    Y,
    Z
}
