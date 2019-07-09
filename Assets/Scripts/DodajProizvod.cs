using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class DodajProizvod : MonoBehaviour
{
    public GameObject inputName;
    public GameObject inputPrice;
    public GameObject inputWeight;
    public GameObject errorReport;
    private GameObject canvas;
    private XmlDocument xmlDoc;
    private string xmlPath;
    private string imeProizvoda;
    private string tezinaString;
    private string cijenaString;


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
    public void DodajNoviProizvod()
    {
        if (inputName.GetComponent<TMP_InputField>().text != "" && inputPrice.GetComponent<TMP_InputField>().text != "" && inputWeight.GetComponent<TMP_InputField>().text != "")
        {
            imeProizvoda = inputName.GetComponent<TMP_InputField>().text;
            cijenaString = priceConverter(inputPrice.GetComponent<TMP_InputField>().text);
            tezinaString = inputWeight.GetComponent<TMP_InputField>().text;
            XmlElement elem = xmlDoc.CreateElement("Proizvod");
            xmlDoc.DocumentElement.AppendChild(elem);
            elem.SetAttribute("id", imeProizvoda.Replace(" ", string.Empty));
            elem.SetAttribute("Ime", imeProizvoda);
            elem.SetAttribute("Cijena", cijenaString);
            elem.SetAttribute("Tezina", tezinaString + "g");
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
   
    private string priceConverter(string price)
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
