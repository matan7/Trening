using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using TMPro;
using System.Xml.Linq;
using UnityEngine.Networking;
using System.IO;

public class ProizvodEdit : MonoBehaviour
{
    public string idProizvoda { get; set; }

    private XmlDocument xmlDoc;
    private XmlElement elem;
    private string xmlPath;

    private GameObject canvas;
    public GameObject imeInput;
    public GameObject cijenaInput;
    public GameObject tezinInput;
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
            if (elem.GetAttribute("id") == idProizvoda)
            {
                foreach (XmlAttribute atr in elem.Attributes)
                {
                    if (atr.Name == "Ime")
                    {
                        imeInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        imeInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                    if (atr.Name == "Cijena")
                    {
                        cijenaInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        cijenaInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                    if (atr.Name == "Tezina")
                    {
                        tezinInput.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>().text = atr.Value;
                        tezinInput.GetComponentInChildren<TextMeshProUGUI>().text = "Promjeni " + atr.Name;
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        imeInput.GetComponentInChildren<TMP_InputField>().text = "";
        cijenaInput.GetComponentInChildren<TMP_InputField>().text = "";
        tezinInput.GetComponentInChildren<TMP_InputField>().text = "";
    }
    public void onSave()
    {
        elem =  xmlDoc.GetElementById(idProizvoda);
        if (imeInput.GetComponentInChildren<TMP_InputField>().text.Length > 1)
        {
            elem.SetAttribute("id", imeInput.GetComponentInChildren<TMP_InputField>().text.Replace(" ", string.Empty));          //error
            elem.SetAttribute("Ime", imeInput.GetComponentInChildren<TMP_InputField>().text);
        }
        if (cijenaInput.GetComponentInChildren<TMP_InputField>().text.Length > 0)
        {
            elem.SetAttribute("Cijena", PriceConverter(cijenaInput.GetComponentInChildren<TMP_InputField>().text));
        }
        if (tezinInput.GetComponentInChildren<TMP_InputField>().text.Length > 0)
        {
            elem.SetAttribute("Tezina", tezinInput.GetComponentInChildren<TMP_InputField>().text + "g");
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
        xmlDoc.GetElementById("Proizvodi").RemoveChild(xmlDoc.GetElementById(idProizvoda));               //error
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
