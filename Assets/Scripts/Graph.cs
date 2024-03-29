using UnityEngine;

public class Graph : MonoBehaviour {

    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    public enum TransitionMode { Cycle, Random }

    [SerializeField]
    TransitionMode transitionMode;

    [SerializeField, Min(0f)]
    float functionDuration = 1f;


    Transform[] points;

    float duration;
    
    void Awake () {

        float step = 2f / resolution;
        Vector3 position = Vector3.zero;
        var scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform);
        }
    }

    void Update () {
        duration += Time.deltaTime;
        if (duration >= functionDuration) {
            duration -= functionDuration;
            PickNextFunction();
        }
        UpdateFunction();        
    }

    void PickNextFunction () {
        function = transitionMode == TransitionMode.Cycle ? 
            FunctionLibrary.GetNextFunctionName(function) :
            FunctionLibrary.GetRandomFunctionNameOtherThan(function);
    }

    void UpdateFunction () {
        float step = 2f / resolution;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++) {
            if (x == resolution) {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;
            Transform point = points[i];
            point.localPosition = f(u, v, time);
        }
    }
}