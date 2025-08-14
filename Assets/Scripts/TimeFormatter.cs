using System;
using UnityEngine;

public static class TimeFormatter
{
    public static string GetFormatTime(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60f);
        var seconds = Mathf.FloorToInt(time % 60f);
        
        return $"{minutes} min {seconds} sec";
    }
    
    /// <param name="timeString">format: n min m sec</param>
    public static int ParseTimeToSeconds(string timeString)
    {
        var parts = timeString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    
        if (parts.Length != 4) return 0;

        if (int.TryParse(parts[0], out var min) && int.TryParse(parts[2], out var sec))
        {
            return min * 60 + sec;
        }

        return 0;
    }
}