using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private TimeController timeController;
    private int secondsRemaining;

    public Canvas canvas_en_jeu;
    public TMP_Text mode;
    public TMP_Text time;

    public Canvas fin_de_jeu;
    public Button restart;
    public TMP_Text message;

    void Start()
    {
        timeController = FindObjectOfType<TimeController>();

        secondsRemaining = timeController.seconds;
        time.text = Utilitaires.FormatTime(secondsRemaining);

        fin_de_jeu.enabled = false;

        restart.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (timeController != null)
        {
            secondsRemaining = timeController.seconds;
            time.text = Utilitaires.FormatTime(secondsRemaining);
        }
    }

    public void GameOver(bool winner)
    {
        if (winner)
        {
            message.text = "Vous etes libres !!!";
        } else
        {
            message.text = "Vous avez perdu...";
        }

        fin_de_jeu.enabled = true;
        canvas_en_jeu.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
