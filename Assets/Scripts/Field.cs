using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Field : MonoBehaviour
{
    [Header("Field Properties")]
    [SerializeField] private float _cellSize;
    [SerializeField] private float _spacing;
    [SerializeField] private int _fieldSize;
    [SerializeField] private int _initCellsCount = 2;

    [Space(10)]
    [SerializeField] private Cell _cellPrefab;

    private RectTransform _rectTransform;
    private Cell[,] _field;

    #region MonoBehaviour 
    private void OnValidate()
    {
        if (_cellSize < 0f) _cellSize = 0f;
        if (_spacing < 0f) _spacing = 0f;
        if (_fieldSize < 0) _fieldSize = 0;

        if (_initCellsCount < 0 || _initCellsCount > (_fieldSize * _fieldSize))
        {
            _initCellsCount = 0;
        }
    }
    #endregion

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        GenerateField();
    }

    private void CreateField()
    {
        _field = new Cell[_fieldSize, _fieldSize];
        float fieldWidth = _fieldSize * (_cellSize + _spacing) + _spacing;
        _rectTransform.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (_cellSize / 2) + _spacing;
        float startY = (fieldWidth / 2) - (_cellSize / 2) - _spacing;

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                Cell cell = Instantiate(_cellPrefab, transform, false);
                Vector2 position = new Vector2(startX + (x * (_cellSize + _spacing)), startY - (y * (_cellSize + _spacing)));
                cell.transform.localPosition = position;

                _field[x, y] = cell;
                cell.SetValue(x, y, 0);
            }
        }
    }

    public void GenerateField()
    {
        if (_field == null)
        {
            CreateField();
        }

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                _field[x, y].SetValue(x, y, 0);
            }
        }

        for (int i = 0; i < _initCellsCount; i++)
        {
            GenerateRandomCell();
        }
    }

    private void GenerateRandomCell()
    {
        List<Cell> cells = new List<Cell>();

        for (int x = 0; x < _fieldSize; x++)
        {
            for (int y = 0; y < _fieldSize; y++)
            {
                if (_field[x, y].IsEmpty)
                {
                    cells.Add(_field[x, y]);
                }
            }
        }

        if (cells.Count == 0)
        {
            throw new System.Exception("There is no any empty cell!");
        }

        int value = Random.Range(0, 10) == 0 ? 2 : 1;
        Cell cell = cells[Random.Range(0, cells.Count)];
        cell.SetValue(cell.X, cell.Y, value);
    }
}
