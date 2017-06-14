using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Looby : MonoBehaviour {


	public GameObject InterfaceLogout, InterfaceRanking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonTelaLogout()
	{
		InterfaceLogout.SetActive (true);
	}

	public void ButtonTelaRanking()
	{
		InterfaceRanking.SetActive (true);
	}

	public void VoltarTelaLobby()
	{
		InterfaceLogout.SetActive (false);
		InterfaceRanking.SetActive (false);
	}

	public void StartarGame()
	{
		SceneManager.LoadScene ("Game");
	}

	public void Deslogar()
	{
		SceneManager.LoadScene ("Menu");
	}

	public void IrDicas()
	{
		SceneManager.LoadScene ("Dicas");
	}
}
