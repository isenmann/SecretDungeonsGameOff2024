using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class UsernameHandling : MonoBehaviour
{
    public string Username { get; set; }

    public string GetUsername()
    {
        var username = PlayerPrefs.GetString("username");
        if (string.IsNullOrWhiteSpace(username))
        {
            username = "PlayerOne";
        }

        return username;
    }

    public void SetUsername(string username)
    {
        Username = username;
        SetUsername();
    }

    public void SetUsername()
    {
        PlayerPrefs.SetString("username", Username);
    }
}