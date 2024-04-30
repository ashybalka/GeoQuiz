using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject CountryPrefab, PointTextPrefab;

    [SerializeField] GameObject GamePanel, StartPanel, WinPanel;

    [SerializeField] TMP_Text countryName;
    [SerializeField] TMP_Text scoreText, levelText, healtText, pointsTotalText;
    [SerializeField] GameObject CountriesObjectsParent;

    [SerializeField] TMP_Text WinPoints;

    public int etalonCountryId;
    public int level;
    public int score;
    private int health = 3;

    public int pointsTotal;

    private CountryStats countryStats;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        countryStats = GameObject.Find("Loader").GetComponent<CountryStats>();

        pointsTotalText.text = "0";

        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    public void CreateNewQuestion()
    {
        CleanGameScreen();

        int[] countryArray;
        if (level <= 8)
        {
            countryArray = new int[level + 2];
        }
        else 
        {
            countryArray = new int[10];
        }

        //Etalon country
        int i = Random.Range(0, countryStats.Country._countriesList.Count);

        countryName.text = TranslateCountryName(i);

        scoreText.text = score.ToString();
        levelText.text = level.ToString();

        countryArray[0] = countryStats.Country._countriesList.Where(c => c.Id == i).First().Id;
        etalonCountryId = countryStats.Country._countriesList.Where(c => c.Id == i).First().Id;
 
        int k = 1;

        while ( k < countryArray.Length)
        {
            int z = Random.Range(0, countryStats.Country._countriesList.Count);
            int tempId = countryStats.Country._countriesList.Where(c => c.Id == z).First().Id;
            if (!countryArray.Contains(tempId))
            {
                countryArray[k] = tempId;
                k++;
            }
        }

        countryArray = RandomizeWithFisherYates(countryArray);

        for (int j = 0; j < countryArray.Length; j++)
        { 
            var generatedCountry = Instantiate(CountryPrefab);

            string spriteName = "Images/Flags/" + countryStats.Country._countriesList.Where(c => c.Id == countryArray[j]).First().Name.EnName;
            generatedCountry.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteName);

            generatedCountry.transform.SetParent(CountriesObjectsParent.transform, false);
            generatedCountry.GetComponent<CountryController>().countryId = countryArray[j];
        }
    }

    public void FakeChangeScene()
    {
        if (StartPanel.activeSelf)
        {
            StartPanel.SetActive(false);
        }
        if (!GamePanel.activeSelf)
        {
            GamePanel.SetActive(true);
        }
    }

    public string TranslateCountryName(int id)
    {
        return YandexGame.savesData.language switch
        {
            "en" => countryStats.Country._countriesList.Where(c => c.Id == id).First().Name.EnName,
            "ru" => countryStats.Country._countriesList.Where(c => c.Id == id).First().Name.RuName,
            "tr" => countryStats.Country._countriesList.Where(c => c.Id == id).First().Name.TrName,
            _ => countryStats.Country._countriesList.Where(c => c.Id == id).First().Name.RuName,
        };
    }
    public static int[] RandomizeWithFisherYates(int[] array)
    {
        int count = array.Length;
        while (count > 1)
        {
            int i = Random.Range(0, count);
            count--;
            (array[i], array[count]) = (array[count], array[i]);
        }
        return array;
    }

    public void CleanGameScreen()
    {
        Image[] countries = CountriesObjectsParent.GetComponentsInChildren<Image>();
        foreach (Image co in countries)
        {
            Destroy(co.gameObject);
        }
    }

    public void Damage(int dmg, GameObject parent)
    {
        ShowPoints(dmg, parent);
        if (dmg == 0)
        {
            health--;
            healtText.text = health.ToString();
        }

        if (health <= 0)
        {
            StartCoroutine(WaitingforWinpanel());
        }
    }

    public void ShowPoints(int dmg, GameObject parent)
    {
        var PointText = Instantiate(PointTextPrefab, transform.parent);
        PointText.GetComponent<TMP_Text>().text = "+" + dmg;
        PointText.transform.SetParent(parent.transform.parent.transform.parent.transform, false);
    }

    public void RestartGame()
    {
        pointsTotal += score;
        MySave();
        SceneManager.LoadScene(0);    
    }

    public void AddHealth()
    { 
        health++;
        healtText.text = health.ToString();
        WinPanel.SetActive(false);
    }
    IEnumerator WaitingforWinpanel()
    {
        yield return new WaitForSeconds(1f);
        WinPanel.SetActive(true);
        WinPoints.text = score.ToString();
    }

    public void GetLoad()
    {
        pointsTotal = YandexGame.savesData.pointsTotal;
        pointsTotalText.text = pointsTotal.ToString();
    }

    public void MySave()
    {
        YandexGame.savesData.pointsTotal = pointsTotal;
        YandexGame.SaveProgress();
        YandexGame.NewLeaderboardScores("TotalPoints", pointsTotal);
    }
}
