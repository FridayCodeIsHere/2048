using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Space(5)] 
    [SerializeField] private Color _pointsDarkColor;
    [SerializeField] private Color _pointsLightColor;
    [SerializeField] private Color[] _cellColors;

    public Color PointsDarkColor => _pointsDarkColor;
    public Color PointsLightColor => _pointsLightColor;

    public static ColorManager Instance;
    public Color[] CellColors => _cellColors;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
