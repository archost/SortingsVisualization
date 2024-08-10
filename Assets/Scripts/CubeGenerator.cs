using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private List<GameObject> _squares = new List<GameObject>();

    private float cubeWidth;

    public List<GameObject> generateCubes(int count)
    {
        Camera camera = Camera.main;
        float _screenWidth = camera.orthographicSize * 2 * camera.aspect;

        cubeWidth = _screenWidth / count;

        for (int i = 0; i < count; i++)
        {
            GameObject instance = Instantiate(_prefab);
            Transform transform = instance.transform;
            transform.localScale = new Vector2(cubeWidth, UnityEngine.Random.Range(0f, 10f));
            transform.position = new Vector2(cubeWidth * i - 8.9f, -5f);
            _squares.Add(instance);
        }

        return _squares;
    }

    public void ResetArray(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _squares[i].transform.localScale = new Vector2(cubeWidth, UnityEngine.Random.Range(0f, 10f));
        }
    }
}