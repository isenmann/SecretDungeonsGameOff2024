using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.UI;

public class SetUsernameInputField : MonoBehaviour
{
    private InputField userNameInputField;
    void Start()
    {
        userNameInputField = GetComponent<InputField>();
        var userName = GameManager.Instance.GetComponent<UsernameHandling>().GetUsername();
        userNameInputField.text = userName;
    }
}
