using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;

public class AddProduct : MonoBehaviour
{
    public GameObject inputName;
    public GameObject inputPrice;
    public GameObject inputWeight;
    public GameObject errorReport;
    private GameObject canvas;
    private XmlDocument xmlDoc;
    private string xmlPath;
    private string productName;
    private string weightString;
    private string priceString;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
    }
    private void OnEnable()
    {
        xmlDoc = new XmlDocument();
#if UNITY_STANDALONE
        xmlPath = Application.dataPath + "/StreamingAssets/Proizvodi.xml";
#endif
#if UNITY_ANDROID
        xmlPath = Application.persistentDataPath + "/Proizvodi.xml";
#endif
        if (File.Exists(xmlPath))
        {
            xmlDoc.Load(xmlPath);
        }
        else
        {
            errorReport.SetActive(true);
            errorReport.GetComponent<TMPro.TextMeshProUGUI>().text = "File Proizvodi.xml not found";         
        }
        
    }
    private void OnDisable()
    {
        inputName.GetComponent<TMP_InputField>().text = "";
        inputPrice.GetComponent<TMP_InputField>().text = "";
        inputWeight.GetComponent<TMP_InputField>().text = "";
        errorReport.SetActive(false);
    }
    public void AddNewProduct()
    {
        if (inputName.GetComponent<TMP_InputField>().text != "" && inputPrice.GetComponent<TMP_InputField>().text != "" && inputWeight.GetComponent<TMP_InputField>().text != "")
        {
            productName = inputName.GetComponent<TMP_InputField>().text;
            priceString = PriceConverter(inputPrice.GetComponent<TMP_InputField>().text);
            weightString = inputWeight.GetComponent<TMP_InputField>().text;
            XmlElement elem = xmlDoc.CreateElement("Proizvod");
            xmlDoc.DocumentElement.AppendChild(elem);
            elem.SetAttribute("id", productName.Replace(" ", string.Empty));
            elem.SetAttribute("Ime", productName);
            elem.SetAttribute("Cijena", priceString);
            elem.SetAttribute("Tezina", weightString + "g");
#if UNITY_STANDALONE
            xmlDoc.Save(Application.dataPath + "/StreamingAssets/Proizvodi.xml");
#endif
#if UNITY_ANDROID
            xmlDoc.Save(Application.persistentDataPath + "/Proizvodi.xml");
#endif
            errorReport.SetActive(false);
            canvas.GetComponent<Navigation>().getBack();
        }
        else
        {
            errorReport.SetActive(true);
            errorReport.GetComponent<TMPro.TextMeshProUGUI>().text = "Popuni sva polja!";
        }
    }
   
    private string PriceConverter(string price)
    {
        string[] stringArr = new string[2];
        if (price.Contains("."))
        {
            stringArr = price.Split(char.Parse("."));
        }
        else
        {
            stringArr[0] = price;
            stringArr[1] = "00";
        }
        if (stringArr[1].Length >= 2)
        {
            stringArr[1] = stringArr[1].Remove(2, stringArr[1].Length - 2);
        }
        else
        {
            stringArr[1] = stringArr[1] + "0";
        }
        return price = stringArr[0]+"." + stringArr[1];
    }
}
