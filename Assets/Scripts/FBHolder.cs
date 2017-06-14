using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBHolder : MonoBehaviour {
	public GameObject DialogLoggedin;
	public GameObject DialogLoggedout;
	public GameObject DialogUsername;
	public GameObject DialogProfilePic;
	public GameObject DialogInterfaceLoggedFB;
	void Awake()
	{
		FB.Init (SetInit, onHideUnity);
	}

	void SetInit()
	{
		if (FB.IsLoggedIn) {
			Debug.Log ("FB is Logged in");
		} else {
			Debug.Log ("FB is not Logged in");
		}
		DealWithFBMenus (FB.IsLoggedIn);
	}

	void onHideUnity(bool isGameShown)
	{
		if (isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}

	public void FBLogin()
	{
		List<string> permissions = new List<string> ();
		permissions.Add("public_profile");

		FB.LogInWithReadPermissions (permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result)
	{
		if (result.Error != null) {
			Debug.Log (result.Error);
		} else {
			if (FB.IsLoggedIn) {
				Debug.Log ("FB is Logged in");
			} else {
				Debug.Log ("FB is not Logged in");
			}
			DealWithFBMenus (FB.IsLoggedIn);
		}
	}

	void DealWithFBMenus(bool isLoggedIn)
	{
		if (DialogLoggedin) {
			DialogLoggedin.SetActive (true);
			DialogLoggedout.SetActive (false);


			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		} else {
			DialogLoggedin.SetActive (false);
			DialogLoggedout.SetActive (true);
		}
	}

	void DisplayUsername(IResult result)
	{
		Text UserName = DialogUsername.GetComponent<Text> ();

		if (result.Error == null) {
			DialogInterfaceLoggedFB.SetActive (true);
			UserName.text = "Ola, " + result.ResultDictionary ["first_name"];
		} else {
			Debug.Log (result.Error);
		}
	}

	void DisplayProfilePic(IGraphResult result)
	{
		if (result.Texture != null) {
			Image ProfilePic = DialogProfilePic.GetComponent<Image> ();
			ProfilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}
	}
}
