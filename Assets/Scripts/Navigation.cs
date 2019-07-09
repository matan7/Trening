using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Navigation : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject administration;
    public GameObject products;
    public GameObject inputs;
    public GameObject productEdit;
    public GameObject addProduct;
    public GameObject inputExcel;
    public GameObject inputReview;

    private GameObject curentWindow;

    public bool mainMenuIsActive { get; set; }
    public bool administrationIsActive { get; set; }
    public bool productsIsActive { get; set; }
    public bool inputsIsActive { get; set; }
    public bool productEditIsActive { get; set; }
    public bool addProductIsActive { get; set; }
    public bool inputExcelIsActive { get; set; }
    public bool inputReviewIsActive { get; set; }
    
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
        windowsStatus.Add(administrationIsActive);
        windowsStatus.Add(productsIsActive);
        windowsStatus.Add(inputsIsActive);
        windowsStatus.Add(productEditIsActive);
        windowsStatus.Add(addProductIsActive);
        windowsStatus.Add(inputExcelIsActive);
        windowsStatus.Add(inputReviewIsActive);

        windowList.Add(mainMenu);
        windowList.Add(administration);
        windowList.Add(products);
        windowList.Add(inputs);
        windowList.Add(productEdit);
        windowList.Add(addProduct);
        windowList.Add(inputExcel);
        windowList.Add(inputReview);

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
            if (administrationIsActive && !administration.active)
            {
                administration.SetActive(true);
                curentWindow = administration;
            }
            else
            {
                administration.SetActive(false);
            }
            if (productsIsActive && !products.active)
            {
                products.SetActive(true);
                curentWindow = products;
            }
            else
            {
                products.SetActive(false);
            }
            if (inputsIsActive && !inputs.active)
            {
                inputs.SetActive(true);
                curentWindow = inputs;
            }
            else
            {
                inputs.SetActive(false);
            }
            if (productEditIsActive && !productEdit.active)
            {
                productEdit.SetActive(true);
                curentWindow = productEdit;
            }
            else
            {
                productEdit.SetActive(false);
            }
            if (addProductIsActive && !addProduct.active)
            {
                addProduct.SetActive(true);
                curentWindow = addProduct;
            }
            else
            {
                addProduct.SetActive(false);
            }
            if (inputExcelIsActive && !inputExcel.active)
            {
                inputExcel.SetActive(true);
                curentWindow = inputExcel;
            }
            else
            {
                inputExcel.SetActive(false);
            }
            if (inputReviewIsActive && !inputReview.active)
            {
                inputReview.SetActive(true);
                curentWindow = inputReview;
            }
            else
            {
                inputReview.SetActive(false);
            }
            setAllFalse();
            windowCanged = false;           
        }

        // Navigation back

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
        administrationIsActive = false;
        productsIsActive = false;
        inputsIsActive = false;
        productEditIsActive = false;
        addProductIsActive = false;
        inputExcelIsActive = false;
        inputReviewIsActive = false;
    }
}
