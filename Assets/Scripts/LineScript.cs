using TMPro;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public TMP_InputField _time;
    public TMP_InputField _name;
    public TMP_InputField _amout;

    public string timeStr { get; set; }
    public string nameStr { get; set; }
    public string amoutStr { get; set; }

    public void deleteThis()
    {
        GameObject.Destroy(this.gameObject);
    }
}
