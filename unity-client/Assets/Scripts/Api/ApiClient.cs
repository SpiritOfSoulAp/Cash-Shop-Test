using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Core;
using Api;

public class ApiClient
{
    private readonly ApiConfig _config;

    public ApiClient(ApiConfig config)
    {
        _config = config;
    }

    // ---------- GET ----------
    public async Task<Result<T>> Get<T>(string path)
    {
        using var req = new UnityWebRequest(_config.BaseUrl + path, "GET");
        req.downloadHandler = new DownloadHandlerBuffer();
        req.timeout = _config.TimeoutSeconds;

        await req.SendWebRequest();

        return HandleResponse<T>(req);
    }

    // ---------- POST ----------
    public async Task<Result<T>> Post<T>(string path, object body, string requestId = null)
    {
        var json = JsonConvert.SerializeObject(body);
        var bytes = Encoding.UTF8.GetBytes(json);

        using var req = new UnityWebRequest(_config.BaseUrl + path, "POST");
        req.uploadHandler = new UploadHandlerRaw(bytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        if (!string.IsNullOrEmpty(requestId))
            req.SetRequestHeader("X-Request-Id", requestId);

        req.timeout = _config.TimeoutSeconds;

        await req.SendWebRequest();

        return HandleResponse<T>(req);
    }

    // ---------- COMMON ----------
    private Result<T> HandleResponse<T>(UnityWebRequest req)
    {
        if (req.result == UnityWebRequest.Result.ConnectionError ||
            req.result == UnityWebRequest.Result.ProtocolError)
        {
            return Result<T>.Fail(new ApiError
            {
                Code = req.responseCode.ToString(),
                Message = req.downloadHandler?.text
            });
        }

        var response = JsonConvert.DeserializeObject<ApiResponse<T>>(req.downloadHandler.text);

        if (response == null)
        {
            return Result<T>.Fail(new ApiError
            {
                Code = "INVALID_RESPONSE",
                Message = "Response is null"
            });
        }

        if (!response.success)
        {
            return Result<T>.Fail(new ApiError
            {
                Code = response.code,
                Message = response.message
            });
        }

        return Result<T>.Ok(response.data);
    }
}
