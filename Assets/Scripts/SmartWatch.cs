using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using TMPro;

public class SmartWatch : MonoBehaviour
{
    // Weather API endpoint and API key
    private string weatherAPIEndpoint = "https://api.openweathermap.org/data/2.5/weather";
    private string apiKey = "b64f05a2d0302c5404a556e68bf25a67"; // Replace with your actual API key

    // Location coordinates for India (example)
    private float latitude = 20.5937f; // Latitude for India
    private float longitude = 78.9629f; // Longitude for India

    public TextMeshProUGUI locationText; // UI text for displaying location
    public TextMeshProUGUI weatherText; // UI text for displaying weather
    public TextMeshProUGUI timeText; // UI text for displaying time
    public TextMeshProUGUI dateText; // UI text for displaying date

    [Obsolete]
    void Start()
    {
        StartCoroutine(FetchWeatherData());
    }

    [Obsolete]
    IEnumerator FetchWeatherData()
    {
        // Fetch weather data
        string weatherUrl = string.Format("{0}?lat={1}&lon={2}&appid={3}", weatherAPIEndpoint, latitude, longitude, apiKey);
        UnityWebRequest weatherWebRequest = UnityWebRequest.Get(weatherUrl);
        yield return weatherWebRequest.SendWebRequest();

        if (weatherWebRequest.isNetworkError || weatherWebRequest.isHttpError)
        {
            Debug.LogError("Failed to fetch weather data: " + weatherWebRequest.error);
        }
        else
        {
            // Parse the JSON response and extract relevant weather data
            string weatherResponseJson = weatherWebRequest.downloadHandler.text;
            WeatherData weatherData = JsonUtility.FromJson<WeatherData>(weatherResponseJson);

            // Access the relevant data from the WeatherData object
            string weatherCondition = weatherData.weather[0].description;
            int temperature = Mathf.RoundToInt(weatherData.main.temp - 273.15f); // Convert temperature from Kelvin to Celsius

            // Update UI text with fetched weather data
            locationText.text = "India ,"; // Update with actual location information
            weatherText.text = string.Format("{1}°C", weatherCondition, temperature);

            StartCoroutine(UpdateTimeAndDate());
        }
    }

    IEnumerator UpdateTimeAndDate()
    {
        while (true)
        {
            // Fetch current time and date
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm:ss"); // Format time as HH:mm
            string date = currentTime.ToString("dd-MMMM-yyyy"); // Format date as dd/MM/yyyy

            // Update UI text with fetched time and date
            timeText.text =   time;
            dateText.text =   date;

            yield return new WaitForSeconds(1f); // Update every 1 second
        }
    }

    [System.Serializable]
    public class WeatherData
    {
        public WeatherInfo[] weather;
        public TemperatureInfo main;
    }

    [System.Serializable]
    public class WeatherInfo
    {
        public string description;
    }

    [System.Serializable]
    public class TemperatureInfo
    {
        public float temp;
    }
}
