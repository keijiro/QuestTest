using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

sealed class StatusView : MonoBehaviour
{
    const int SampleCount = 30;

    Queue<float> _samples = new Queue<float>();

    void Update()
    {
        var current = Time.time;
        _samples.Enqueue(current);

        var start = _samples.Count <= SampleCount ?
          _samples.Peek() : _samples.Dequeue();

        var fps = SampleCount / (current - start);
        GetComponent<Text>().text = $"FPS: {fps:.00}";
    }
}
