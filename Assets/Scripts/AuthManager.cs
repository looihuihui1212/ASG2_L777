using TMPro;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Firebase;

public class AuthManager : MonoBehaviour
{
    [Header("Sign-In Fields")]
    public TMP_InputField signInEmail;
    public TMP_InputField signInPassword;
    public TextMeshProUGUI signInMessage;

    [Header("Sign-Up Fields - Part 1")]
    public TMP_InputField signUpEmail;
    public TMP_InputField signUpPassword;
    public TMP_InputField signUpConfirmPassword;
    public TextMeshProUGUI signUpMessage;

    [Header("Sign-Up Fields - Part 2")]
    public TMP_InputField usernameField;
    public TMP_InputField firstNameField;
    public TMP_InputField lastNameField;
    public TextMeshProUGUI part2Message;

    [Header("Forgot Password Fields")]
    public TMP_InputField resetEmail;
    public TextMeshProUGUI resetMessage;

    [Header("Canvas Management")]
    public GameObject signUpCanvasPart1;
    public GameObject signUpCanvasPart2;
    public GameObject loginCanvas;
    public GameObject forgotPasswordCanvas;

    private FirebaseAuth auth;
    private FirebaseDatabase db;
    private string currentUserId;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                db = FirebaseDatabase.GetInstance("https://your-database-url.firebaseio.com");
            }
            else
            {
                Debug.LogError("Firebase dependencies are not available: " + task.Result);
            }
        });
    }

    // Part 1: Sign-Up with Email and Password
    public void SignUpPart1()
    {
        string email = signUpEmail.text;
        string password = signUpPassword.text;
        string confirmPassword = signUpConfirmPassword.text;

        if (password != confirmPassword)
        {
            signUpMessage.text = "Passwords do not match.";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                signUpMessage.text = "Sign-up failed. Try again.";
            }
            else
            {
                signUpMessage.text = "Account created successfully!";
                currentUserId = task.Result.User.UserId;
                // Switch to part 2 canvas
                signUpCanvasPart1.SetActive(false);
                signUpCanvasPart2.SetActive(true);
            }
        });
    }

    // Part 2: Collect Additional User Information and Save to Firebase
    public void SubmitAdditionalInfo()
    {
        string username = usernameField.text;
        string firstName = firstNameField.text;
        string lastName = lastNameField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            part2Message.text = "Please fill in all fields.";
            return;
        }

        // Create a reference to the user's data in the database
        DatabaseReference userRef = db.GetReference("users").Child(currentUserId);

        // Save user data
        userRef.SetRawJsonValueAsync(JsonUtility.ToJson(new UserData(signUpEmail.text, username, firstName, lastName)))
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    part2Message.text = "Failed to save user data.";
                }
                else
                {
                    part2Message.text = "Account setup complete!";
                    // Switch back to the login canvas
                    signUpCanvasPart2.SetActive(false);
                    loginCanvas.SetActive(true);
                }
            });
    }

    // Sign-In functionality
    public void SignIn()
    {
        string email = signInEmail.text;
        string password = signInPassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                signInMessage.text = "Sign-in failed. Check your credentials.";
            }
            else
            {
                signInMessage.text = "Sign-in successful!";
                // Load your next scene or gameplay here
                // Example: SceneManager.LoadScene("MainGameScene");
            }
        });
    }

    // Forgot password functionality
    public void ResetPassword()
    {
        string email = resetEmail.text;

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                resetMessage.text = "Reset email failed to send.";
            }
            else
            {
                resetMessage.text = "Password reset email sent!";
            }
        });
    }

    // Show Forgot Password screen
    public void ShowForgotPasswordCanvas()
    {
        loginCanvas.SetActive(false);
        forgotPasswordCanvas.SetActive(true);
    }

    // Go back to the login screen
    public void ShowLoginCanvas()
    {
        forgotPasswordCanvas.SetActive(false);
        loginCanvas.SetActive(true);
    }

    // Helper class for user data
    [System.Serializable]
    public class UserData
    {
        public string email;
        public string username;
        public string firstName;
        public string lastName;

        public UserData(string email, string username, string firstName, string lastName)
        {
            this.email = email;
            this.username = username;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
