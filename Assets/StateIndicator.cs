using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateIndicator : MonoBehaviour
{
    public FSMExecutor FSMExecutor;

    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = FSMExecutor.CurrentStateHandler.ActionState.ToString();
    }
}
