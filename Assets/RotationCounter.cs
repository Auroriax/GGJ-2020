using UnityEngine;

public class RotationCounter : MonoBehaviour
{
    public float SignalOnRotationDegrees = 180;
    public bool DetectPositiveMotion = true;

    public ProgressBar ProgressBar;
    public GameStateController GameStateController;

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

        if (DetectPositiveMotion)
            DetectedRotation += currentY - recalculatedPreviousY;
        else
            DetectedRotation += recalculatedPreviousY - currentY;

        previousY = currentY;
        if (DetectedRotation <= 0)
            return;

        if (ProgressBar != null)
            ProgressBar.Progress = DetectedRotation / SignalOnRotationDegrees;
        if (GameStateController != null)
        {
            if (DetectedRotation > SignalOnRotationDegrees)
                GameStateController.LevelCompleted();
        }
    }
}
