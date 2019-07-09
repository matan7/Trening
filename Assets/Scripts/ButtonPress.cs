using TMPro;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public string productName { get; set; }
    public GameObject productdEdit;

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        productdEdit = canvas.GetComponent<Navigation>().productEdit;  
    }

    public void ButtonCLicked()
    {
        productdEdit.GetComponent<ProductEdit>().idProduct = productName.Replace(" ", string.Empty);
        canvas.GetComponent<Navigation>().productEditIsActive = true;
        canvas.GetComponent<Navigation>().windowCanged = true;
    }
}
