using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataController : MonoBehaviour
{
    [SerializeField] private RoundData[] allRoundData;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this);
        SceneManager.LoadScene("Menu");
        
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
