using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingParameters : MonoBehaviour
{
    public static SortingParameters Instance {  get; private set; }

    public int speed { get; set; }

    public string sortingType { get; set; }

    public int count { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}