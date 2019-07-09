using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System.Xml;
using TMPro;

public class ProductEditor : MonoBehaviour
{
    private XmlDocument xmlDoc;
    public GameObject productButton;
    public GameObject containerButtons;

    private void OnEnable()
    {
        xmlDoc = new XmlDocument();
#if UNITY_STANDALONE
        xmlDoc.Load(Application.dataPath + "/StreamingAssets/Proizvodi.xml");
#endif
#if UNITY_ANDROID
        xmlDoc.Load(Application.persistentDataPath + "/Proizvodi.xml");
#endif
        productLister(xmlDoc);

    }
    private void OnDisable()
    {
        foreach (Transform child in containerButtons.transform)
        {
            if(child.tag == "ButtonEdit")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    private void productLister(XmlDocument xmlDoc)
    {
        foreach(XmlElement proizvod in xmlDoc.DocumentElement)
        {
            GameObject butt = Instantiate(productButton, containerButtons.transform) as GameObject;
            butt.GetComponentInChildren<TextMeshProUGUI>().SetText(proizvod.GetAttribute("Ime"));
            butt.GetComponent<ButtonPress>().productName = proizvod.GetAttribute("Ime");
        }
        
     }
}
