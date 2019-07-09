using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using System.Xml;
using TMPro;

public class UnosElements : MonoBehaviour
{
    public XmlDocument xmlDoc;
    public GameObject proizvodButton;
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
        proizvodLister(xmlDoc);

    }
    private void OnDisable()
    {
        foreach (Transform child in containerButtons.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    } 

    private void proizvodLister(XmlDocument xmlDoc)
    {
        foreach(XmlElement proizvod in xmlDoc.DocumentElement)
        {
            GameObject butt = Instantiate(proizvodButton, containerButtons.transform) as GameObject;
            butt.GetComponentInChildren<TextMeshProUGUI>().SetText(proizvod.GetAttribute("Ime"));
            butt.GetComponent<ButtonUnoos>().proizvodName = proizvod.GetAttribute("Ime");
        }
        
     }
}
