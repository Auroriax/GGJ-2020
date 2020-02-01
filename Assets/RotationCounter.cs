using UnityEngine;

public class RotationCounter : MonoBehaviour
{
    public float SignalOnRotationDegrees = 180;

    public ProgressBar ProgressBar;
    public GameStateController GameStateController;

    public bool DetectPositiveMotion;
    public float DetectedRotation = 0;
    public float previousY;

    // Start is called before the first frame update
    void Start()
    {
        previousY = this.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        var currentY = this.transform.rotation.eulerAngles.y;
        if (currentY == previousY)
            return;

        var recalculatedPreviousY = previousY;
        if (currentY < 90 && previousY >= 270)
            recalculatedPreviousY -= 360;
        else if (previousY < 90 && currentY >= 270)
            recalculatedPreviousY += 360;

        DetectedRotation += currentY - recalculatedPreviousY;
        previousY = currentY;
        var positiveRotation = DetectedRotation > 0 ? DetectedRotation : DetectedRotation * -1;

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
