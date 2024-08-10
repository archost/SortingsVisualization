using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField elementsCount;

    [SerializeField]
    private TMP_InputField speed;

    [SerializeField]
    private TMP_Dropdown sortingAlgorithm;

    private void SetAllParameters()
    {
        SortingParameters.Instance.sortingType = sortingAlgorithm.options[sortingAlgorithm.value].text;
        SortingParameters.Instance.count = int.Parse(elementsCount.text);
        SortingParameters.Instance.speed = float.Parse(speed.text, CultureInfo.InvariantCulture.NumberFormat);
    }
    public void LoadMainScene()
    {
        SetAllParameters();
        SceneManager.LoadScene(0);
    }
}