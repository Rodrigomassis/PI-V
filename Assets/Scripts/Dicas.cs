using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Dicas : MonoBehaviour {
	public Sprite[] sprites;
	SpriteRenderer spriterenderer;
	public int Index = 0;
	public Button buttonVoltar, buttonAvancar;
	void Start () {	
		spriterenderer = GetComponent<SpriteRenderer>();
		spriterenderer.sprite = sprites [Index];
	}
	
	// Update is called once per frame
	void Update () {

        DisabledButton();
        

    }
		

    public void DisabledButton()
    {
        if (Index > 5)
        {
            buttonAvancar.interactable = false;
        }
        else
        {
            buttonAvancar.interactable = true;
        }

        if (Index < 1)
        {
            buttonVoltar.interactable = false;
        }
        else
        {

            buttonVoltar.interactable = true;
        }

    }
	public void ButtonAvancar()
	{
		
			Index++;
			spriterenderer.sprite = sprites [Index];
		

	}

	public void ButtonVoltar()
	{
		
			Index--;
			spriterenderer.sprite = sprites [Index];

		

	}
	public void ButtonVoltarLobby()
	{
		SceneManager.LoadScene ("Lobby");

	}


}
