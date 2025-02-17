using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace PhucLH.AdventureGame
{
    public class FirebaseAuthentication : Singleton<FirebaseAuthentication>
    {
        private FirebaseAuth auth;
        public DatabaseReference dbRef { get;private set; }
        public FirebaseUser user { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                    dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                }
                else
                {
                    Debug.LogError(task.Result);
                }
            });
        }
        
        public void RegisterUser(string email, string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.Log(task.Exception);
                        AuthenticationUI.Ins.registerErrorText.text = "Type the correct email, password must be more than 6 characters!!!";
                        return;
                    }

                    if (task.IsCompletedSuccessfully)
                    {
                        user = task.Result.User;
                        dbRef.Child("users").Child(user.UserId).SetValueAsync(user.UserId).ContinueWithOnMainThread(task1 =>
                        {
                            if (task1.IsFaulted || task1.IsCanceled)
                            {
                                Debug.Log(task1.Exception);
                                AuthenticationUI.Ins.registerErrorText.text = "Lost server connection!";
                            }

                            if (task1.IsCompletedSuccessfully)
                            {
                                Debug.Log(user.UserId);
                                AuthenticationUI.Ins.SwitchUpUI();
                            }
                        });
                        
                    }
                });
                
            }
            else
            {
                AuthenticationUI.Ins.registerErrorText.text="Please confirm the correct password";
            }
        }
        public void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                    AuthenticationUI.Ins.loginErrorText.text = "Wrong email or password";
                    return;
                }

                if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("Success");
                    user = task.Result.User;
                    SceneManager.LoadScene("MainMenu");
                }
            });
        }
    }
}

