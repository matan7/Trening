using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnosiExcel : MonoBehaviour
{
    public string IdProizvoda { get; set; }
    public GameObject canvas;

    public TMP_InputField inputNr;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject buttonDel;

    public GameObject textDate;
    public GameObject textTrenutno;
    public GameObject textVlastito;
    public GameObject inputDate;

    public GameObject toggle1;
    public GameObject toggle2;

    private string currentTime;
    private string selectedTime;
    private string nameProizvod;
    private string xmlPath;
    public TextAsset pekaraDoc;
    private XmlDocument xmlDoc;
    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
    }
    private void OnEnable()
    {
        TimeSellect();

        xmlDoc = new XmlDocument();
#if UNITY_STANDALONE
        xmlPath = Application.dataPath + "/StreamingAssets/Proizvodi.xml";
        xmlDoc.Load(xmlPath);
#endif
#if UNITY_ANDROID
        xmlDoc.Load(Application.persistentDataPath + "/Proizvodi.xml");
#endif

        foreach(XmlElement elem in xmlDoc.DocumentElement)
        {
            if(elem.GetAttribute("id") == IdProizvoda)
            {
                nameProizvod = elem.GetAttribute("Ime");   // nesta brljavi ovdje
                break;    
            }
        }

        toggle1.GetComponent<Toggle>().isOn = true;
    }
    void OnDisable()
    {
        inputNr.text = "";
        inputDate.GetComponent<TMP_InputField>().text = "";
    }
    void Update()
    {
        currentTime = DateTime.Now.Hour.ToString() + ":" + TimeFixer(DateTime.Now.Minute.ToString());
        textDate.GetComponent<TextMeshProUGUI>().text = currentTime;
    }
    public void OnSaveClicked()
    {
        
        if (toggle1.GetComponent<Toggle>().isOn && inputNr.text.Length > 0)
        {
            selectedTime = currentTime; //->save
            SaveUnos(selectedTime, inputNr.text);
            canvas.GetComponent<Navigation>().getBack();
        }
        else
        {
            if (inputDate.GetComponent<TMP_InputField>().text.Length >= 4 && inputNr.text.Length > 0)
            {
                selectedTime = inputDate.GetComponent<TMP_InputField>().text;  //->save
                SaveUnos(selectedTime, inputNr.text);
                canvas.GetComponent<Navigation>().getBack();
            }
            else
            {
                inputDate.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Neispravan unos!";
                inputDate.GetComponent<TMP_InputField>().text = "";
            }
        }
        
    }
    private string TimeFixer(string time)
    {
        if (time.Length == 1)
        {
            time = "0" + time;
        }
        return time;
    }
    public void TimeSellect()
    {

        if (toggle1.GetComponent<Toggle>().isOn)
        {
            textDate.GetComponent<TextMeshProUGUI>().color = Color.white;
            textTrenutno.GetComponent<TextMeshProUGUI>().color = Color.white;
            textVlastito.GetComponent<TextMeshProUGUI>().color = Color.gray;
            inputDate.SetActive(false);
        }
        else
        {
            textDate.GetComponent<TextMeshProUGUI>().color = Color.gray;
            textTrenutno.GetComponent<TextMeshProUGUI>().color = Color.gray;
            textVlastito.GetComponent<TextMeshProUGUI>().color = Color.white;
            inputDate.SetActive(true);
        }
    }
    public void Add1()
    {
        int amoutNum = 0;
        string currentAmout = inputNr.text;
        if (currentAmout.Length > 0)
        {
            amoutNum = int.Parse(currentAmout) + 1;
        }
        else
        {
            amoutNum = 1;
        }
        inputNr.text = amoutNum.ToString();
    }
    public void Add5()
    {
        int amoutNum = 0;
        string currentAmout = inputNr.text;
        if (currentAmout.Length > 0)
        {
            amoutNum = int.Parse(currentAmout) + 5;
        }
        else
        {
            amoutNum = 5;
        }
        inputNr.text = amoutNum.ToString();
    }
    public void Add10()
    {
        int amoutNum = 0;
        string currentAmout = inputNr.text;
        if (currentAmout.Length > 0)
        {
            amoutNum = int.Parse(currentAmout) + 10;
        }
        else
        {
            amoutNum = 10;
        }
        inputNr.text = amoutNum.ToString();
    }
    public void Add20()
    {
        int amoutNum = 0;
        string currentAmout = inputNr.text;
        if (currentAmout.Length > 0)
        {
            amoutNum = int.Parse(currentAmout) + 20;
        }
        else
        {
            amoutNum = 20;
        }
        inputNr.text = amoutNum.ToString();
    }
    public void Add50()
    {
        int amoutNum = 0;
        string currentAmout = inputNr.text;
        if (currentAmout.Length > 0)
        {
            amoutNum = int.Parse(currentAmout) + 50;
        }
        else
        {
            amoutNum = 50;
        }
        inputNr.text = amoutNum.ToString();
    }
    public void DelButton()
    {
        inputNr.text = string.Empty;
    }
    public void SaveUnos(string time, string amout)
    {
        string pekaraDocPath = "";
#if UNITY_STANDALONE
        pekaraDocPath = Application.dataPath + "/StreamingAssets/Pekara.csv";
#endif
#if UNITY_ANDROID
        pekaraDocPath = Application.persistentDataPath + "/Pekara.csv";
#endif

        string saveDoc = Environment.NewLine + DateTime.Today.ToShortDateString() + "," + time + "," + nameProizvod + "," + amout ;
        File.AppendAllText(pekaraDocPath, saveDoc);
        
    }
}
