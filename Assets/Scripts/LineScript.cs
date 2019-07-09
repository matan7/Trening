using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineScript : MonoBehaviour
{
    public TMP_InputField _time;
    public TMP_InputField _name;
    public TMP_InputField _amout;

    public string timeStr { get; set; }
    public string nameStr { get; set; }
    public string amoutStr { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteThis()
    {
        GameObject.Destroy(this.gameObject);
    }
}
