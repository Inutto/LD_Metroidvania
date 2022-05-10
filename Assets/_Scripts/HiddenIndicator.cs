using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenIndicator : MonoBehaviour
{

    public Text numberText;
    public float currentNum;
    public float totalNum;


    #region SINGLETON
    public static HiddenIndicator _Instance;

    public static HiddenIndicator Instance
    {
        get
        {
            return _Instance;
        }
    }

    protected virtual void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this as HiddenIndicator;
        }
        else
        {
            if (_Instance != this as HiddenIndicator)
            {
                
                Destroy(gameObject);
            }
        }

        // DontDestroyOnLoad(gameObject);
    }

    #endregion SINGLETON



    // Start is called before the first frame update
    void Start()
    {
        numberText = GetComponent<Text>();
        InitOneWayGateTriggerNum();
    }

    void InitOneWayGateTriggerNum()
    {
        var gates = GameObject.FindObjectsOfType<OneWayGate>();
        var num = gates.Length;
        totalNum = num;
        UpdateTextContent();
        
    }

    public void UpdateTextContent()
    {
        numberText.text = string.Format("{0} / {1}", currentNum, totalNum);
    }

    public void UpdateCurrentNum()
    {
        ++currentNum;
        UpdateTextContent();
    }
}
