using UnityEngine;

[CreateAssetMenu(menuName = "Config/ApiConfig")]
public class ApiConfig : ScriptableObject
{
    public string BaseUrl = "http://localhost:8080";
    public int TimeoutSeconds = 10;
}
