using TMPro;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public string proizvodName { get; set; }
    public GameObject proizvodEdit;

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        proizvodEdit = canvas.GetComponent<Navigation>().proizvodEdit;  
    }

    public void ButtonCLicked()
    {
        proizvodEdit.GetComponent<ProizvodEdit>().idProizvoda = proizvodName.Replace(" ", string.Empty);
        canvas.GetComponent<Navigation>().proizvodEditIsActive = true;
        canvas.GetComponent<Navigation>().windowCanged = true;
    }
}
