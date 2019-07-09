using UnityEngine;
using TMPro;


public class ButtonUnoos : MonoBehaviour
{
    public GameObject unosExcel;
    public string proizvodName { get; set; }
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        unosExcel = canvas.GetComponent<Navigation>().unosExcel;
    }
   
    public void onClickedButton()
    {
        unosExcel.GetComponent<UnosiExcel>().IdProizvoda = proizvodName.Replace(" ",string.Empty);
        canvas.GetComponent<Navigation>().unosExcelIsActive = true;
        canvas.GetComponent<Navigation>().windowCanged = true;
    }
}
