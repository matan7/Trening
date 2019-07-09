using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Navigation : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject administracija;
    public GameObject proizvodi;
    public GameObject unosi;
    public GameObject proizvodEdit;
    public GameObject dodajProizvod;
    public GameObject unosExcel;
    public GameObject pregledUnosa;

    private GameObject curentWindow;

    public bool mainMenuIsActive { get; set; }
    public bool administracijaIsActive { get; set; }
    public bool proizvodiIsActive { get; set; }
    public bool unosiIsActive { get; set; }
    public bool proizvodEditIsActive { get; set; }
    public bool dodajProizvodIsActive { get; set; }
    public bool unosExcelIsActive { get; set; }
    public bool pregledUnosaIsActive { get; set; }
    
    public bool windowCanged { get; set; }

    private List<bool> windowsStatus = new List<bool>();
    public List<GameObject> windowDepth = new List<GameObject>();
    private List<GameObject> windowList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        windowCanged = false;
#if UNITY_ANDROID
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("1 XML exist:" + File.Exists(Application.persistentDataPath + "/Proizvodi.xml"));
        Debug.Log(Application.dataPath);
#endif


        setAllFalse();

        windowsStatus.Add(mainMenuIsActive);
        windowsStatus.Add(administracijaIsActive);
        windowsStatus.Add(proizvodiIsActive);
        windowsStatus.Add(unosiIsActive);
        windowsStatus.Add(proizvodEditIsActive);
        windowsStatus.Add(dodajProizvodIsActive);
        windowsStatus.Add(unosExcelIsActive);
        windowsStatus.Add(pregledUnosaIsActive);

        windowList.Add(mainMenu);
        windowList.Add(administracija);
        windowList.Add(proizvodi);
        windowList.Add(unosi);
        windowList.Add(proizvodEdit);
        windowList.Add(dodajProizvod);
        windowList.Add(unosExcel);
        windowList.Add(pregledUnosa);

        curentWindow = mainMenu;
        
    }

    // Update is called once per frame
    void Update()
    {
        windowControler();
    }

    void windowControler()
    {
        if (windowCanged)
        {

            windowDepth.Add(curentWindow);
            curentWindow.SetActive(false);
            short itemIndex = 0;
            foreach(GameObject window in windowList)
            {
                itemIndex++;
                if (window == curentWindow)
                {
                    break;
                }
                    
            }
            windowsStatus[itemIndex] = false;

            if (mainMenuIsActive && !mainMenu.active)
            {
                mainMenu.SetActive(true);
                curentWindow = mainMenu;
                
            }
            else
            {
                mainMenu.SetActive(false);
            }
            if (administracijaIsActive && !administracija.active)
            {
                administracija.SetActive(true);
                curentWindow = administracija;
            }
            else
            {
                administracija.SetActive(false);
            }
            if (proizvodiIsActive && !administracija.active)
            {
                proizvodi.SetActive(true);
                curentWindow = proizvodi;
            }
            else
            {
                proizvodi.SetActive(false);
            }
            if (unosiIsActive && !unosi.active)
            {
                unosi.SetActive(true);
                curentWindow = unosi;
            }
            else
            {
                unosi.SetActive(false);
            }
            if (proizvodEditIsActive && !proizvodEdit.active)
            {
                proizvodEdit.SetActive(true);
                curentWindow = proizvodEdit;
            }
            else
            {
                proizvodEdit.SetActive(false);
            }
            if (dodajProizvodIsActive && !dodajProizvod.active)
            {
                dodajProizvod.SetActive(true);
                curentWindow = dodajProizvod;
            }
            else
            {
                dodajProizvod.SetActive(false);
            }
            if (unosExcelIsActive && !unosExcel.active)
            {
                unosExcel.SetActive(true);
                curentWindow = unosExcel;
            }
            else
            {
                unosExcel.SetActive(false);
            }
            if (pregledUnosaIsActive && !pregledUnosa.active)
            {
                pregledUnosa.SetActive(true);
                curentWindow = pregledUnosa;
            }
            else
            {
                pregledUnosa.SetActive(false);
            }
            setAllFalse();
            windowCanged = false;

            
        }
        if (Input.GetKeyDown("escape"))
        {
            getBack();
        }
    }
    public void getBack()
    {
        if (windowDepth.Count > 0)
        {
            try
            {
                foreach (GameObject window in windowList)
                {
                    window.SetActive(false);
                }
                windowDepth.Last<GameObject>().SetActive(true);
                curentWindow = windowDepth.Last<GameObject>();
                windowDepth.RemoveAt(windowDepth.Count - 1);
            }
            catch
            {
                return;
            }
        }
    }
    void setAllFalse()
    {
        mainMenuIsActive = false;
        administracijaIsActive = false;
        proizvodiIsActive = false;
        unosiIsActive = false;
        proizvodEditIsActive = false;
        dodajProizvodIsActive = false;
        unosExcelIsActive = false;
        pregledUnosaIsActive = false;
    }
}
