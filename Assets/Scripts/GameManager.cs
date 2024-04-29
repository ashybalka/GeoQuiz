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

    private readonly string[] contryNames = { "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua and Barbuda", "Argentina", "Armenia", "Australia",
        "Austria", "Azerbaijan", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana",
        "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Central African Republic", "Chad", "Chile", "China",
        "Colombia", "Comoros", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica",
        "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini", "Ethiopia",
        "Federated States of Micronesia", "Finland", "France", "Gabon", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea-Bissau", "Guinea",
        "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Ivory Coast", "Jamaica",
        "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein",
        "Lithuania", "Luxembourg", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Moldova",
        "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger",
        "Nigeria", "North Korea", "North Macedonia", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland",
        "Portugal", "Qatar", "Republic of the Congo", "Romania", "Russia", "Rwanda", "Saint Thomas and Prince", "Saint Kitts and Nevis", "Saint Lucia", 
        "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia",
        "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Sweden", "Switzerland", "Syria",
        "Tajikistan", "Tanzania", "Thailand", "The Gambia", "The Bahamas", "Togo", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Tuvalu",
        "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican", "Venezuela", "Vietnam", 
        "Yemen", "Zambia", "Zimbabwe"};
    private readonly string[] contryNamesRu = { "Афганистан", "Албания", "Алжир", "Андорра", "Ангола", "Антигуа и Барбуда", "Аргентина", "Армения", "Австралия",
        "Австрия", "Азербайджан", "Бахрейн", "Бангладеш", "Барбадос", "Беларусь", "Бельгия", "Белиз", "Бенин", "Бутан", "Боливия", "Босния и Герцеговина", "Ботсвана",
        "Бразилия", "Бруней", "Болгария", "Буркина-Фасо", "Бурунди", "Камбоджа", "Камерун", "Канада", "Кабо-Верде", "Центральноафриканская Республика", "Чад", "Чили", 
        "Китай", "Колумбия", "Коморы", "Коста-Рика", "Хорватия", "Куба", "Кипр", "Чехия", "Демократическая Республика Конго", "Дания", "Джибути", "Доминика",
        "Доминиканская Республика", "Восточный Тимор", "Эквадор", "Египет", "Сальвадор", "Экваториальная Гвинея", "Эритрея", "Эстония", "Эсватини", "Эфиопия",
        "Федеративные Штаты Микронезии", "Финляндия", "Франция", "Габон", "Грузия", "Германия", "Гана", "Греция", "Гренада", "Гватемала", "Гвинея-Бисау", "Гвинея",
        "Гайана", "Гаити", "Гондурас", "Венгрия", "Исландия", "Индия", "Индонезия", "Иран", "Ирак", "Ирландия", "Израиль", "Италия", "Кот-д'Ивуар", "Ямайка",
        "Япония", "Иордания", "Казахстан", "Кения", "Кирибати", "Кувейт", "Кыргызстан", "Лаос", "Латвия", "Ливан", "Лесото", "Либерия", "Ливия", "Лихтенштейн",
        "Литва", "Люксембург", "Мадагаскар", "Малави", "Малайзия", "Мальдивы", "Мали", "Мальта", "Маршалловы Острова", "Мавритания", "Маврикий", "Мексика", "Молдова",
        "Монако", "Монголия", "Черногория", "Марокко", "Мозамбик", "Мьянма", "Намибия", "Науру", "Непал", "Нидерланды", "Новая Зеландия", "Никарагуа", "Нигер",
        "Нигерия", "Северная Корея", "Северная Македония", "Норвегия", "Оман", "Пакистан", "Палау", "Панама", "Папуа - Новая Гвинея", "Парагвай", "Перу", "Филиппины", 
        "Польша", "Португалия", "Катар", "Республика Конго", "Румыния", "Россия", "Руанда", "Сан-Томе и Принсипи", "Сент-Китс и Невис", "Сент-Люсия",
        "Сент-Винсент и Гренадины", "Самоа", "Сан-Марино", "Саудовская Аравия", "Сенегал", "Сербия", "Сейшелы", "Сьерра-Леоне", "Сингапур", "Словакия", "Словения",
        "Соломоновы Острова", "Сомали", "Южная Африка", "Южная Корея", "Южный Судан", "Испания", "Шри-Ланка", "Судан", "Суринам", "Швеция", "Швейцария", "Сирия",
        "Таджикистан", "Танзания", "Таиланд", "Гамбия", "Багамские Острова", "Того", "Тонга", "Тринидад и Тобаго", "Тунис", "Турция", "Туркменистан", "Тувалу",
        "Уганда", "Украина", "Объединенные Арабские Эмираты", "Великобритания", "Соединенные Штаты", "Уругвай", "Узбекистан", "Вануату", "Ватикан", "Венесуэла", "Вьетнам",
        "Йемен", "Замбия", "Зимбабве" };
    private readonly string[] contryNamesTr = { "Afganistan", "Arnavutluk", "Cezayir", "Andorra", "Angola", "Antigua ve Barbuda", "Arjantin", "Ermenistan", "Avustralya",
        "Avusturya", "Azerbaycan", "Bahreyn", "Bangladeş", "Barbados", "Belarus", "Belçika", "Belize", "Benin", "Butan", "Bolivya", "Bosna-Hersek", "Botsvana",
        "Brezilya", "Brunei", "Bulgaristan", "Burkina Faso", "Burundi", "Kamboçya", "Kamerun", "Kanada", "Cape Verde", "Orta Afrika Cumhuriyeti", "Çad", "Şili", "Çin",
        "Kolombiya", "Komorlar", "Kosta Rika", "Hırvatistan", "Küba", "Kıbrıs", "Çek Cumhuriyeti", "Demokratik Kongo Cumhuriyeti", "Danimarka", "Cibuti", "Dominika",
        "Dominik Cumhuriyeti", "Doğu Timor", "Ekvador", "Mısır", "El Salvador", "Ekvator Ginesi", "Eritre", "Estonya", "Esvatini", "Etiyopya",
        "Mikronezya Federal Devletleri", "Finlandiya", "Fransa", "Gabon", "Gürcistan", "Almanya", "Gana", "Yunanistan", "Grenada", "Guatemala", "Gine-Bissau", "Gine",
        "Guyana", "Haiti", "Honduras", "Macaristan", "İzlanda", "Hindistan", "Endonezya", "İran", "Irak", "İrlanda", "İsrail", "İtalya", "Fildişi Sahili", "Jamaika",
        "Japonya", "Ürdün", "Kazakistan", "Kenya", "Kiribati", "Kuveyt", "Kırgızistan", "Laos", "Letonya", "Lübnan", "Lesotho", "Liberya", "Libya", "Lihtenştayn",
        "Litvanya", "Lüksemburg", "Madagaskar", "Malavi", "Malezya", "Maldivler", "Mali", "Malta", "Marshall Adaları", "Moritanya", "Mauritius", "Meksika", "Moldova",
        "Monako", "Moğolistan", "Karadağ", "Fas", "Mozambik", "Myanmar", "Namibya", "Nauru", "Nepal", "Hollanda", "Yeni Zelanda", "Nikaragua", "Nijer",
        "Nijerya", "Kuzey Kore", "Kuzey Makedonya", "Norveç", "Umman", "Pakistan", "Palau", "Panama", "Papua Yeni Gine", "Paraguay", "Peru", "Filipinler", "Polonya",
        "Portekiz", "Katar", "Kongo Cumhuriyeti", "Romanya", "Rusya", "Ruanda", "São Tomé ve Príncipe", "Saint Kitts ve Nevis", "Saint Lucia",
        "Saint Vincent ve Grenadinler", "Samoa", "San Marino", "Suudi Arabistan", "Senegal", "Sırbistan", "Seyşeller", "Sierra Leone", "Singapur", "Slovakya", "Slovenya",
        "Solomon Adaları", "Somali", "Güney Afrika", "Güney Kore", "Güney Sudan", "İspanya", "Sri Lanka", "Sudan", "Surinam", "İsveç", "İsviçre", "Suriye",
        "Tacikistan", "Tanzanya", "Tayland", "Gambiya", "Bahamalar", "Togo", "Tonga", "Trinidad ve Tobago", "Tunus", "Türkiye", "Türkmenistan", "Tuvalu",
        "Uganda", "Ukrayna", "Birleşik Arap Emirlikleri", "Birleşik Krallık", "Amerika Birleşik Devletleri", "Uruguay", "Özbekistan", "Vanuatu", "Vatikan", "Venezuela", 
        "Vietnam", "Yemen", "Zambiya", "Zimbabve" };


    public string etalonCountryName;
    public int level;
    public int score;
    private int health = 3;

    public int pointsTotal;



    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    private void Start()
    {
        pointsTotalText.text = "0";
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    public void CreateNewQuestion()
    {
        CleanGameScreen();
        string[] countryArray;
        if (level <= 8)
        {
            countryArray = new string[level + 2];
        }
        else 
        {
            countryArray = new string[10];
        }

        int i = Random.Range(0, contryNames.Length);

        countryName.text = TranslateCountryName(i);
        scoreText.text = score.ToString();
        levelText.text = level.ToString();

        countryArray[0] = contryNames[i];
        etalonCountryName = contryNames[i];

        
        int k = 1;

        while ( k < countryArray.Length)
        {
            int z = Random.Range(0, contryNames.Length);
            if (!countryArray.Contains(contryNames[z]))
            {
                countryArray[k] = contryNames[z];
                k++;
            }
        }

        countryArray = RandomizeWithFisherYates(countryArray);

        for (int j = 0; j < countryArray.Length; j++)
        { 
            var generatedCountry = Instantiate(CountryPrefab);
            string spriteName = "Images/Flags/" + countryArray[j];
            generatedCountry.GetComponent<Image>().sprite = Resources.Load<Sprite>(spriteName);
            generatedCountry.transform.SetParent(CountriesObjectsParent.transform, false);
            generatedCountry.GetComponent<CountryController>().countryName = countryArray[j];
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
            "en" => contryNames[id],
            "ru" => contryNamesRu[id],
            "tr" => contryNamesTr[id],
            _ => contryNamesRu[id],
        };
    }
    public static string[] RandomizeWithFisherYates(string[] array)
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
