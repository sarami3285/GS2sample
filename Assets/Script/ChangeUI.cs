using UnityEngine;
using UnityEngine.UI;

public class ChangeUIButton : MonoBehaviour
{
    public GameObject BeforeScreen;
    public GameObject AfterScreen;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (AfterScreen == null)
            {
                BeforeScreen.SetActive(false);
            }
            else
            {
                BeforeScreen.SetActive(false);
                AfterScreen.SetActive(true);
            }
        });
    }
}