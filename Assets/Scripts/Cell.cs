using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _points;
    private Image _image;

    public int X { get; private set; }
    public int Y { get; private set; }

    public int Value { get; private set; }
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public const int MaxValue = 11;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetValue(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
        UpdateCell();
    }

    public void UpdateCell()
    {
        _points.text = IsEmpty ? string.Empty : Points.ToString();
        _points.color = Value <= 2 ? ColorManager.Instance.PointsDarkColor : ColorManager.Instance.PointsLightColor;
        _image.color = ColorManager.Instance.CellColors[Value];
    }
}
