using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Data;



public class Game : scriptBD {

    void Awake()
    {
        url = "URI=file:" + Application.persistentDataPath + "/" + "BDPIV"; //Caminho do Banco de Dados
        conectarBD(url);
        criarBancoDados();
    }
    static private string url;
    public Text usuario, pontuacao, questaoNum, questao, tempo, textQndAcer, textQndErr, pontFinal, Status;
	public Button a,b,c;
	public GameObject InterfacePause, InterfaceEndGame;
	float time;
	public bool comecou = false;
	List<Perguntas> listaPerguntas = new List<Perguntas>();
    public jogador usu = new jogador();
    string statusAcer= "ACERTOU";
    string statusErr = "ERROU";
    string statusAcaTemp = "O tempo acabou...";
    Menu menu= new Menu();
    
    public int inteiro;
    int QndAcertos, QndErros;
    
	int pontos;
    int numquest;

    int countAlternativas;
    string[] alternativas;
	// Use this for initialization
	void Start () {
        
        countAlternativas = 0;
        inteiro = 0;
        numquest = 1;
        QndAcertos = 0;
        QndErros = 0;
        ArrayList listaJogs = new ArrayList();
        Debug.Log(menu.nomeUsu);
        usuario.GetComponentInChildren<Text>().text = menu.nomeUsu;
        
        IDataReader dados = pesquisarSQL("select Quest_id, Quest_Per, Quest_RespCer, Quest_RespErrA, Quest_RespErrB, Quest_Tempo from Questoes");
       
        while (dados.Read())
		{
            Debug.Log("entrou onde escreve");

            Perguntas perguntas = new Perguntas();
            perguntas.id 		= dados.GetInt32(0);
			perguntas.pergunta 	= dados.GetString(1);
			perguntas.opcaoCerta 	= dados.GetString(2);
			perguntas.opcaoErrada 	= dados.GetString(3);
			perguntas.opcaoErrada2 	= dados.GetString(4);
			perguntas.tempo 	= dados.GetInt32(5);
			listaPerguntas.Add(perguntas);
		}
		questao.text = listaPerguntas [inteiro].pergunta;
        
        a.GetComponentInChildren<Text> ().text = listaPerguntas [inteiro].opcaoCerta;
		b.GetComponentInChildren<Text> ().text = listaPerguntas [inteiro].opcaoErrada;
		c.GetComponentInChildren<Text> ().text = listaPerguntas [inteiro].opcaoErrada2;
		pontuacao.text = "0";
        tempo.GetComponentInChildren<Text>().text= listaPerguntas[inteiro].tempo.ToString();
		time = listaPerguntas[inteiro].tempo;
		comecou = true;
        Time.timeScale = 1;
        questaoNum.text = numquest.ToString();
    }
	
	// Update is called once per frame
	void Update () {

        Tempo();


        
    }

	public void VerificarResposta(Button botao){
		if (botao.GetComponentInChildren<Text> ().text == listaPerguntas [inteiro].opcaoCerta) {
			pontos = pontos + 100;
            numquest++;
            inteiro++;
            QndAcertos++;
            Status.GetComponentInChildren<Text>().text = statusAcer;
            pontuacao.text = pontos.ToString ();
            questaoNum.text = numquest.ToString() ;
            Debug.Log ("Acertou");

		} else{
			inteiro++;
            QndErros++;
            numquest++;
			Debug.Log ("errou");
            questaoNum.text = numquest.ToString();
            Status.GetComponentInChildren<Text>().text = statusErr;
        }
        time = listaPerguntas[inteiro].tempo;
    }

    //public void PreencherBotoes()
    //{
    //    while (countAlternativas < 3)
    //    {
    //        int rnd = UnityEngine.Random.Range(0, 2);

    //        if (alternativas[rnd] == null)
    //        {
    //            alternativas[rnd] = listaPerguntas[inteiro].opcaoCerta;
    //            countAlternativas++;
    //            if (alternativas[rnd] == null)
    //            {
    //                alternativas[rnd] = listaPerguntas[inteiro].opcaoErrada;
    //                countAlternativas++;
    //                if (alternativas[rnd] == null)
    //                {
    //                    alternativas[rnd] = listaPerguntas[inteiro].opcaoErrada2;
    //                    countAlternativas++;

    //                }

    //            }

    //        }
            
            

    //    }
        //a.GetComponentInChildren<Text>().text = alternativas[0];
        //b.GetComponentInChildren<Text>().text = alternativas[1];
        //c.GetComponentInChildren<Text>().text = alternativas[2];
    //}
    public void Tempo()
    {
        if (comecou)
        {
            time -= Time.deltaTime;
            var someString = String.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
            tempo.text = someString;


        }

        questao.text = listaPerguntas[inteiro].pergunta;


        if (time < 1)
        {
            time = listaPerguntas[inteiro].tempo;
            numquest++;
            inteiro++;
            QndErros++;
            questaoNum.text = numquest.ToString();
            Status.GetComponentInChildren<Text>().text = statusAcaTemp;

            Debug.Log("errou");
        }

        a.GetComponentInChildren<Text>().text = listaPerguntas[inteiro].opcaoCerta;
        b.GetComponentInChildren<Text>().text = listaPerguntas[inteiro].opcaoErrada;
        c.GetComponentInChildren<Text>().text = listaPerguntas[inteiro].opcaoErrada2;

        if (numquest > 10)
        {
            Time.timeScale = 0;
            InterfaceEndGame.SetActive(true);
            textQndAcer.GetComponentInChildren<Text>().text = QndAcertos.ToString();
            textQndErr.GetComponentInChildren<Text>().text = QndErros.ToString();
            pontFinal.GetComponentInChildren<Text>().text = pontos.ToString();

        }
    }


	public void TelaPause()
	{
		InterfacePause.SetActive (true);
        Time.timeScale = 0;
	}

	public void TelaGame()
	{
		InterfacePause.SetActive (false);
        Time.timeScale = 1;
    }

	public void VoltarLooby()
	{
		SceneManager.LoadScene ("Lobby");
        
    }
}
