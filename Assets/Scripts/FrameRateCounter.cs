using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI display;

    void Update () {

        float frameDuration = Time.unscaledDeltaTime;
        display.SetText("FP\n{0:0}\n000\n000", 1f / frameDuration);
    }
}