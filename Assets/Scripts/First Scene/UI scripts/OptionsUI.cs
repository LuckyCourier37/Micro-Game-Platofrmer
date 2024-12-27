using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    [SerializeField] private GameObject PanelControls;
    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private GameObject Mcamera;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject buttonExit;
    
    void Start()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(ShowOptions);

        startPos = Mcamera.transform.position;
         
    }

    // Update is called once per frame
    
    private void ShowOptions()
    {
        Debug.Log(button.name + " Was clicked");
        
        PanelControls.SetActive(true);
        startButton.SetActive(false);
        title.SetActive(false);
        buttonExit.SetActive(false);
       gameObject.SetActive(false);
        Mcamera.transform.position += new Vector3(0f, 0f, 100f);
    }

    public void CloseOptions()
    {
        PanelControls.SetActive(false);
        startButton.SetActive(true);
        title.SetActive(true);  
        gameObject.SetActive(true );
        buttonExit.SetActive(true);
        Mcamera.transform.position = startPos;

    }
}
