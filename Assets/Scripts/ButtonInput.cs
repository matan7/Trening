using UnityEngine;
using TMPro;


public class ButtonInput : MonoBehaviour
{
    public GameObject unosExcel;
    public string proizvodName { get; set; }
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        unosExcel = canvas.GetComponent<Navigation>().inputExcel;
    }
   
    public void onClickedButton()
    {
        unosExcel.GetComponent<InputsExcel>().IdProduct = proizvodName.Replace(" ",string.Empty);
        canvas.GetComponent<Navigation>().inputExcelIsActive = true;
        canvas.GetComponent<Navigation>().windowCanged = true;
    }
}
