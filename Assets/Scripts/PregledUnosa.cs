using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using System;

public class PregledUnosa : MonoBehaviour
{
    public GameObject line;
    public GameObject dateTxt;
    private string[] pekaraDocument;
    string pekaraDocPath = "";
    private List<GameObject> lineList;
    private bool isFocus = false;
    private bool isProcessing = false;
    void Start()
    {
        
        dateTxt.GetComponent<TextMeshProUGUI>().text = DateTime.Today.ToShortDateString();
    }
    void OnEnable()
    {
        lineList = new List<GameObject>();
#if UNITY_STANDALONE
        pekaraDocPath = Application.dataPath + "/StreamingAssets/Pekara.csv";
#endif
#if UNITY_ANDROID
        pekaraDocPath = Application.persistentDataPath + "/Pekara.csv";
#endif
        pekaraDocument = File.ReadAllText(pekaraDocPath).Split(new char[] { '\n' });

        foreach(string nline in pekaraDocument){
            if(nline.Length > 0)
            {
                string[] lineArr = nline.Split(',');
                if(lineArr[0] == DateTime.Today.ToShortDateString())
                {
                    GameObject lineInstance = GameObject.Instantiate(line, this.transform) as GameObject;

                    lineInstance.GetComponent<LineScript>()._time.text = lineArr[1];
                    lineInstance.GetComponent<LineScript>()._name.text = lineArr[2];
                    lineInstance.GetComponent<LineScript>()._amout.text = lineArr[3];

                    lineList.Add(lineInstance);
                }
                
            }
        }
    }
    void OnDisable()
    {
        saveFile();
    }
    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
    public void sendFile()
    {
        string allData = "";
        foreach (GameObject nline in lineList)
        {
            if (nline != null)
            {
                if (nline.tag == "Line")
                {
                    LineScript script = nline.GetComponent<LineScript>();
                    allData += "\n" + DateTime.Today.ToShortDateString() + "," + script._time.text + "," + script._name.text + "," + script._amout.text;
                }
            }
        }
        File.WriteAllText(pekaraDocPath, "");
        File.AppendAllText(pekaraDocPath, allData);
#if UNITY_ANDROID
        if (!isProcessing)
        {
            StartCoroutine(ShareFIleOutside());
        }
#else
		Debug.Log("No sharing set up for this platform.");
#endif

    }
    void saveFile()
    {
        string allData = "";
        foreach (GameObject nline in lineList)
        {
            if (nline != null)
            {
                if (nline.tag == "Line")
                {
                    LineScript script = nline.GetComponent<LineScript>();
                    allData += "\n" + DateTime.Today.ToShortDateString() + "," + script._time.text + "," + script._name.text + "," + script._amout.text;
                    GameObject.Destroy(nline.gameObject);
                }
            }
        }
        lineList.RemoveRange(0, lineList.Count);
        File.WriteAllText(pekaraDocPath, "");
        File.AppendAllText(pekaraDocPath, allData);
    }
#if UNITY_ANDROID
    public IEnumerator ShareFIleOutside()
    {

        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        string FilePath = Application.persistentDataPath + "/Pekara.csv";
        yield return new WaitForSeconds(0.5f);

        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //create file URI to add it to the intent
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + FilePath);

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "text/csv");

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Posalji sve unose");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
#endif
}
