using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountryController : MonoBehaviour
{
    public string countryName;
    private GameManager gameManager;
    [SerializeField] Image RightWrongImage;
    [SerializeField] AudioSource RightAudio, WrongAudio;

    void Start()
    {
        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CheckAnswer()
    {
        CountryController[] contries = gameObject.transform.parent.GetComponentsInChildren<CountryController>();
        foreach (CountryController con in contries)
        {
            con.GetComponent<Button>().interactable = false;
            if (con.countryName == gameManager.etalonCountryName)
            {
                con.RightWrongImage.gameObject.SetActive(true);
                con.RightWrongImage.sprite = Resources.Load<Sprite>("Images/OkButton");
            }
        }

        int currentLevelPoint = 1 + CheckLevel(gameManager.score);
        if (countryName == gameManager.etalonCountryName)
        {
            gameManager.Damage(currentLevelPoint, gameObject);
            gameManager.score += currentLevelPoint;
            RightAudio.Play();
        }
        else
        {
            gameManager.Damage(0, gameObject);
            RightWrongImage.gameObject.SetActive(true);
            RightWrongImage.sprite = Resources.Load<Sprite>("Images/NoButton");
            WrongAudio.Play();
        }
        gameManager.level = CheckLevel(gameManager.score);
        StartCoroutine(WaitingforRight());

    }

    public int CheckLevel(int score)
    {
        int level = 0;
        int summ = 0;
        while (summ <= score)
        {
            level++;
            summ += level * 10;
        }
        return level - 1;

    }

    IEnumerator WaitingforRight()
    {
        yield return new WaitForSeconds(1f);
        gameManager.CreateNewQuestion();
    }
}