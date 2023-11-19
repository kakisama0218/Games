using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject sceneLoader;
    private Animator anim;

    public GameObject settings;
    [SerializeField] private Text version;
    // Start is called before the first frame update
    void Start()
    {
        anim = sceneLoader.GetComponent<Animator>();
        version.text = "Ver" + Application.version + "BETA";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterGame()
    {
        anim.SetBool("switch", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SettingBtn()
    {
        settings.gameObject.SetActive(true);
    }
    public void QuitSettingBtn()
    {
        settings.gameObject.SetActive(false);
    }
}
