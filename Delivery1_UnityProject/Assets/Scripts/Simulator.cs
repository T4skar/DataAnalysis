using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Simulator : MonoBehaviour
{

    public static Action<string,int, string ,string, DateTime> OnNewPlayer; //Name, Country and date
    //(string playerName, int playerAge, string playerGender, string playerCountry, DateTime signUpTime)
    public static Action<int, DateTime> OnNewSession; //user id, time
    public static Action<DateTime, int, int> OnEndSession; //time, session id, user id
    public static Action<int, int, DateTime, int> OnBuyItem; //User id, money spent, time, sessionId

    public static Action GetPlayerID;

    private DateTime _currentDate;

    public int MaxPlayers=100;
    public float ReplayChance => _currentDate.Month < 6 ? 0.25f: 0.5f;
    public float BuyProbability = 0.1f;


    private int _nPlayers;

    private List<string> Countries;

    [SerializeField]
    private Lexic.NameGenerator namegen;

    #region Subscribe
    private void OnEnable()
    {
        CallbackEvents.OnAddPlayerCallback += OnPlayerAdded;
        CallbackEvents.OnNewSessionCallback += OnNewSessionAdded;
        CallbackEvents.OnEndSessionCallback += OnEndSessionAdded;
        CallbackEvents.OnItemBuyCallback += OnItemBought;
    }

    private void OnDisable()
    {
        CallbackEvents.OnAddPlayerCallback -= OnPlayerAdded;
        CallbackEvents.OnNewSessionCallback -= OnNewSessionAdded;
        CallbackEvents.OnEndSessionCallback -= OnEndSessionAdded;
        CallbackEvents.OnItemBuyCallback -= OnItemBought;
    }

    #endregion

    void Start()
    {
        Countries = new List<string>();
        int nCountries = Random.Range(1, 10);

        string[] coutryNames = System.Enum.GetNames(typeof(AllCountries));
        for (int i = 0; i < nCountries; i++)
        {
            int rdm = Random.Range(0, coutryNames.Length);
            Countries.Add(((AllCountries)rdm).ToString());
        }
        MakeOnePlayer();    
    }

    void MakeOnePlayer()
    {
        _nPlayers++;
        if (_nPlayers > MaxPlayers)
        {
            Debug.Log("Finished");
            return;
        }  

        _currentDate = GetNewPlayerDate();
        AddNewPlayer(_currentDate);
    }

    void AddNewPlayer(DateTime dateTime)
    {
        string name = namegen.GetNextRandomName();
        int rdm = Random.Range(0, Countries.Count);
        string country = Countries[rdm];
        int age = Random.Range(9, 99);
        float rdm2 = Random.value;
        string gender = rdm2 < 0.4f ? "F" : rdm2 < 0.8f ? "M" : "NB";

        OnNewPlayer?.Invoke(name, age, gender, country, dateTime);
    }

    void AddNewSession(int userId)
    {
        DateTime dateTime = _currentDate;
        OnNewSession?.Invoke(userId, dateTime);
    }

    void EndSession(int sessionId, int userId)
    {
        _currentDate = _currentDate.Add(GetSessionLength());
        DateTime dateTime = _currentDate;
        OnEndSession?.Invoke(dateTime, sessionId, userId);
    }

    void TryBuy(int userId, int sessionId)
    {
        _currentDate = _currentDate.Add(GetSessionLength());
        if (UserBuys())
            OnBuyItem?.Invoke(userId, GetItem(), _currentDate, sessionId);
        else
            EndSession(sessionId, userId);
    }

    private bool UserBuys()
    {
        return Random.value < BuyProbability;
    }

    private int GetItem()
    {
        float rdm = Random.value;
        if (rdm < 0.5f)
            return 1;
        if (rdm < 0.75f)
            return 2;
        if (rdm < 0.9f)
            return 3;
        if (rdm < 0.91f)
            return 4;

        return 5;

    }

    #region Probabilistic values
    DateTime GetNewPlayerDate()
    {
        int year = 2022;
        int month = Random.Range(1, 13);
        int numOfDays = DateTime.DaysInMonth(year, month);
        int day = Random.Range(1, numOfDays + 1);

        int hour = Random.Range(0, 24);
        int minut = Random.Range(0, 60);
        int second = Random.Range(0, 60);

        return new DateTime(year, month, day, hour, minut, second);

    }

    private TimeSpan GetSessionLength()
    {
        float lengthInSeconds = Random.Range(30, 500);
        return TimeSpan.FromSeconds(lengthInSeconds);
    }

    private TimeSpan GetBuyTime()
    {
        float lengthInSeconds = Random.Range(10, 120);
        return TimeSpan.FromSeconds(lengthInSeconds);
    }

    private TimeSpan TimeTillNextSession()
    {
        float lengthInSeconds = Random.Range(3000, 30000);
        return TimeSpan.FromSeconds(lengthInSeconds);
    }
    #endregion

    #region callback subscribers
    private void OnPlayerAdded(int userId)
    {
        AddNewSession(userId);
    }

    private void OnNewSessionAdded(int userId, int sessionId)
    {
        TryBuy(userId, sessionId);
    }

    private void OnItemBought(int sessionId, int userId)
    {
        EndSession(sessionId, userId);
    }

    private void OnEndSessionAdded(int userId)
    {
        
        if(Random.value > ReplayChance)
        {
            MakeOnePlayer();
            return;
        }
        TimeSpan timeSpan = TimeTillNextSession();
       
        _currentDate = _currentDate.Add(timeSpan);
       // Debug.Log(_currentDate.ToLongDateString());

        if (_currentDate.Year == 2022)
            AddNewSession(userId);
        else
            MakeOnePlayer();
    }

   
    #endregion
}

public class CallbackEvents
{
    public static Action<int> OnEndSessionCallback;
    public static Action<int, int> OnNewSessionCallback;
    public static Action<int> OnAddPlayerCallback;
    public static Action<int, int> OnItemBuyCallback;
}
