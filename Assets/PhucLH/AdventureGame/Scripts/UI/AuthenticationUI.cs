using UnityEngine;
using UnityEngine.UI;

namespace PhucLH.AdventureGame
{
    public class AuthenticationUI : Singleton<AuthenticationUI>
    {
        [Header("Register")] 
        public InputField registerEmailInput;
        public InputField registerPasswordInput; 
        public InputField registerConfirmPasswordInput;
        public Text registerErrorText;
        [Header("Login")] 
        public InputField loginEmailInput;
        public InputField loginPasswordInput;
        public Text loginErrorText;
        [Header("Switch Interaction")] 
        public GameObject loginUI;
        public GameObject registerUI;
        
        public override void Awake()
        {
            MakeSingleton(false);
        }

        public void Register()
        {
            FirebaseAuthentication.Ins.RegisterUser(registerEmailInput.text, registerPasswordInput.text, registerConfirmPasswordInput.text);
        }

        public void Login()
        {
            FirebaseAuthentication.Ins.LoginUser(loginEmailInput.text, loginPasswordInput.text);
        }

        public void SwitchUpUI()
        {
            loginUI.SetActive(registerUI.activeInHierarchy);
            registerUI.SetActive(!loginUI.activeInHierarchy);
            registerErrorText.text = "";
            loginErrorText.text = "";
        }
    }
}