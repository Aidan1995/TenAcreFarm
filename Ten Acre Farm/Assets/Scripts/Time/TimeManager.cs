using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header ("Internal clock")]
    [SerializeField] GameTime timeStamp;

    public float timeScale = 1f;

    [Header ("Day and night cycle")]
    public Transform sunTransform;

    List<ITimeTracker> listeners = new List<ITimeTracker>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timeStamp = new GameTime(0, GameTime.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
            
        }        
    }

    public void Tick()
    {
        timeStamp.UpdateClock();

        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }

        UpdateSun();
        
    }

    void UpdateSun()
    {
        int timeInMinutes = GameTime.HoursToMinute(timeStamp.hour) + timeStamp.minute;

        float sunAngle = 0.25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    public GameTime GetGameTime()
    {
        return new GameTime(timeStamp);
    }

    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
