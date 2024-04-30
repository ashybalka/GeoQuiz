using System;
using System.Collections.Generic;
using UnityEngine;

public class CountryStats : MonoBehaviour
{
    [SerializeField] TextAsset CountryStatsJson;
    public CountriesList Country;

    private void Awake()
    {
        LoadFromJson();
    }
    public void LoadFromJson()
    {
        Country = JsonUtility.FromJson<CountriesList>(CountryStatsJson.ToString());
    }
}
[Serializable]
public class CountriesList
{
    public List<Country> _countriesList;
}

[Serializable]
public class Country
{
    public int Id;
    public Name Name;
}
[Serializable]
public class Name
{
    public string EnName;
    public string RuName;
    public string TrName;
}
