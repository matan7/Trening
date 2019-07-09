using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using System.Xml.Linq;
using UnityEngine.Networking;
using System.IO;

public class ProductEdit : MonoBehaviour
{
    public string idProduct { get; set; }

    private XmlDocument xmlDoc;
    private XmlElement elem;
    private string xmlPath;

    private GameObject canvas;
    public GameObject nameInput;
    public GameObject priceInput;
    public GameObject weightInput;
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
        xmlDoc.Load(xmlPath);
#endif

#if UNITY_ANDROID
        xmlDoc.Load(Application.persistentDataPath + "/Proizvodi.xml");   
#endif

        foreach (XmlElement elem in xmlDoc.DocumentElement)
        {
            if (elem.GetAttribute("id") == idProduct)
            {
                foreach (XmlAttribute atr in elem.Attributes)
                {
                    if (atr.Name == "Ime")
                    {
                        nameInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        nameInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                    if (atr.Name == "Cijena")
                    {
                        priceInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        priceInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                    if (atr.Name == "Tezina")
                    {
                        weightInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        weightInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        nameInput.GetComponentInChildren<TMP_InputField>().text = "";
        priceInput.GetComponentInChildren<TMP_InputField>().text = "";
        weightInput.GetComponentInChildren<TMP_InputField>().text = "";
    }
    public void onSave()
    {
        elem =  xmlDoc.GetElementById(idProduct);
        if (nameInput.GetComponentInChildren<TMP_InputField>().text.Length > 1)
        {
            elem.SetAttribute("id", nameInput.GetComponentInChildren<TMP_InputField>().text.Replace(" ", string.Empty));          //error
            elem.SetAttribute("Ime", nameInput.GetComponentInChildren<TMP_InputField>().text);
        }
        if (priceInput.GetComponentInChildren<TMP_InputField>().text.Length > 0)
        {
            elem.SetAttribute("Cijena", PriceConverter(priceInput.GetComponentInChildren<TMP_InputField>().text));
        }
        if (weightInput.GetComponentInChildren<TMP_InputField>().text.Length > 0)
        {
            elem.SetAttribute("Tezina", weightInput.GetComponentInChildren<TMP_InputField>().text + "g");
        }
#if UNITY_STANDALONE
        xmlDoc.Save(Application.dataPath + "/StreamingAssets/Proizvodi.xml");
#endif
#if UNITY_ANDROID
        xmlDoc.Save(Application.persistentDataPath + "/Proizvodi.xml");
#endif
    }
    public void onDeletePress()
    {
        xmlDoc.GetElementById("Proizvodi").RemoveChild(xmlDoc.GetElementById(idProduct));               //error
#if UNITY_STANDALONE
        xmlDoc.Save(Application.dataPath + "/StreamingAssets/Proizvodi.xml");
#endif
#if UNITY_ANDROID
        xmlDoc.Save(Application.persistentDataPath + "/Proizvodi.xml");
#endif
        canvas.GetComponent<Navigation>().getBack();
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
        return price = stringArr[0] + "." + stringArr[1];
    }
}
