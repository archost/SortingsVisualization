using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sorting : MonoBehaviour
{
    [SerializeField]
    private GenerateTone generateTone;

    private List<GameObject> _array;

    [SerializeField]
    private CubeGenerator generator;

    private float _speed;

    private int _count;

    [SerializeField]
    private TextMeshProUGUI _iterationText;

    private Dictionary<string, IEnumerator> _sortings;

    private int _iterations = 0;

    string _sortingAlgorithm;

    private Sorting()
    {
        _count = SortingParameters.Instance.count;
        _speed = 1f / SortingParameters.Instance.speed;
        _sortingAlgorithm = SortingParameters.Instance.sortingType;
        _sortings = new Dictionary<string, IEnumerator>()
        {
            { "Bubble Sort", bubbleSort() },
            { "Selection Sort", selectionSort() },
            { "Insertion Sort", insertionSort() },
            { "Quick Sort", quickSort(0, _count - 1) },
            { "Shell Sort", shellSort() },
            { "Heap Sort", heapSort() },
        };
    }

    private void Start()
    {
        _array = generator.generateCubes(_count);
        
        foreach (var item in _sortings)
        {
            if (_sortingAlgorithm == item.Key)
                StartCoroutine(item.Value);
        }
    }

    private IEnumerator bubbleSort()
    {
        for (int i = 0; i < _count - 1; i++)
        {
            for (int j = 0; j < _count - i - 1; j++)
            {
                _iterations++;
                _iterationText.text = "Iterations: " + _iterations;
                if (_array[j].transform.localScale.y > _array[j + 1].transform.localScale.y)
                {

                    /*
                    _squares[j].transform.Find("Square").GetComponent<SpriteRenderer>().color = Color.green;
                    yield return new WaitForSeconds(speed);
                    _squares[j + 1].transform.Find("Square").GetComponent<SpriteRenderer>().color = Color.red;
                    yield return new WaitForSeconds(speed);
                    */

                    Swap(j, j + 1);

                    yield return new WaitForSeconds(_speed);
                }
            }
        }
    }

    private IEnumerator selectionSort()
    {
        int minId;
        for (int i = 0; i < _count - 1; i++)
        {
            minId = i;
            for (int j = i + 1; j < _count; j++)
            {
                _iterations++;
                _iterationText.text = "Iterations: " + _iterations;
                if (_array[j].transform.localScale.y < _array[minId].transform.localScale.y)
                {
                    minId = j;
                }
                yield return new WaitForSeconds(_speed);
            }
            if (minId != i)
                Swap(i, minId);
        }
    }

    private IEnumerator insertionSort()
    {
        float buf;
        int j;
        for (int i = 1; i < _count; i++)
        {
            buf = _array[i].transform.localScale.y;
            for (j = i - 1; j >= 0 && buf < _array[j].transform.localScale.y; j--)
            {
                _iterations++;
                _iterationText.text = "Iterations: " + _iterations;

                _array[j + 1].transform.localScale = new Vector3(_array[j + 1].transform.localScale.x, _array[j].transform.localScale.y);

                yield return new WaitForSeconds(_speed);
            }
            _array[j + 1].transform.localScale = new Vector3(_array[j + 1].transform.localScale.x, buf);
        }
    }

    private IEnumerator quickSort(int l, int r)
    {
        int left = l, right = r;
        int pivotId = l + (r - l) / 2;
        float pivot = _array[pivotId].transform.localScale.y;
        Swap(r, pivotId);  
        right--;

        while (left <= right)
        {
            while (left < _count && _array[left].transform.localScale.y < pivot) left++;

            while (right >= 0 && _array[right].transform.localScale.y > pivot) right--;

            if (left <= right)
            {
                Swap(left, right);
                left++;
                right--;
            }

            yield return new WaitForSeconds(_speed);

            _iterations++;
            _iterationText.text = "Iterations: " + _iterations;
        }

        Swap(left, r);

        if (l < right)
            StartCoroutine(quickSort(l, right));
        if (left + 1 < r)
            StartCoroutine(quickSort(left + 1, r));
    }

    private IEnumerator shellSort()
    {
        int[] step = { 1750, 701, 301, 132, 57, 23, 10, 4, 1 };
        float buf;
        int gap, j;

        for (int k = 0; k < 9; k++)
        {
            gap = step[k];
            for (int i = gap; i < _count; i++)
            {
                buf = _array[i].transform.localScale.y;
                for (j = i - gap; j >= 0 && buf < _array[j].transform.localScale.y; j -= gap)
                {
                    _iterations++;
                    _iterationText.text = "Iterations: " + _iterations;

                    _array[j + gap].transform.localScale = new Vector3(_array[j + gap].transform.localScale.x, _array[j].transform.localScale.y);

                    yield return new WaitForSeconds(_speed);
                }
                _array[j + gap].transform.localScale = new Vector3(_array[j + gap].transform.localScale.x, buf);
            }
        }
    }

    private IEnumerator heapify(int size, int i)
    {
        _iterations++;
        _iterationText.text = "Iterations: " + _iterations;
        yield return new WaitForSeconds(_speed);

        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < size && _array[l].transform.localScale.y > _array[largest].transform.localScale.y)
            largest = l;
        if (r < size && _array[r].transform.localScale.y > _array[largest].transform.localScale.y)
            largest = r;
        if (largest != i)
        {
            Swap(i, largest);
            yield return StartCoroutine(heapify(size, largest));
        }
    }

    private IEnumerator heapSort()
    {
        for (int i = _count / 2 - 1; i >= 0; i--)
        {
            yield return StartCoroutine(heapify(_count, i));
        }
        for (int i = _count - 1; i >= 0; i--)
        {
            Swap(0, i);
            yield return StartCoroutine(heapify(i, 0));
        }
    }

    private void Swap(int x, int y)
    {
        generateTone.GenerateSound(_array[y].transform.localScale.y * 1000, _speed);

        float temp = _array[x].transform.position.x;
        _array[x].transform.position = new Vector2(_array[y].transform.position.x, _array[x].transform.position.y);
        _array[y].transform.position = new Vector2(temp, _array[x].transform.position.y);

        GameObject tempObj = _array[x];
        _array[x] = _array[y];
        _array[y] = tempObj;
    }
}
