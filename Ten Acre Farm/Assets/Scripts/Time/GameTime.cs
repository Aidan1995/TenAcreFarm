using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTime
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    public enum Day
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    
    public Season season;
    
    public int day, hour, minute;


    public GameTime(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;   
    }

    public GameTime(GameTime timeStamp)
    {
        this.year = timeStamp.year;
        this.season = timeStamp.season;
        this.day = timeStamp.day;
        this.hour = timeStamp.hour;
        this.minute = timeStamp.minute;
    }

    public void UpdateClock()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            day++;
        }

        if (day > 30)
        {
            day = 1;            

            if (season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }            
        }
    }

    public Day GetDay()
    {
        int daysPassed = YearsToDays(year) * SeasonsToDays(season) + day;

        int dayIndex = daysPassed % 7;

        return(Day)dayIndex;
    }

    public static int HoursToMinute(int hour)
    {
        return hour * 60;
    }

    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }

    public static int CompareTimeStamps(GameTime timeStamp1, GameTime timeStamp2)
    {
        int timeStamp1Hours = DaysToHours(YearsToDays(timeStamp1.year)) + DaysToHours(SeasonsToDays(timeStamp1.season)) + DaysToHours(timeStamp1.day)
            + timeStamp1.hour;

        int timeStamp2Hours = DaysToHours(YearsToDays(timeStamp2.year)) + DaysToHours(SeasonsToDays(timeStamp2.season)) + DaysToHours(timeStamp2.day)
            + timeStamp2.hour;

        int difference = timeStamp2Hours - timeStamp1Hours;

        return Mathf.Abs(difference);

    }

}


