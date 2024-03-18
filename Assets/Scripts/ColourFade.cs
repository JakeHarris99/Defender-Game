using UnityEngine;
using UnityEngine.UI;

public class ColourFade : MonoBehaviour
{
    [SerializeField]
    Gradient gradient;
    [SerializeField]
    float duration;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        float value = Mathf.Repeat(Time.time / duration, 1);
        Color color = gradient.Evaluate(value);
        text.color = color;
    }
}
