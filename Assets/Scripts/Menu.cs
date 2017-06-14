using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Data;

public class Menu : scriptBD {
    void Awake()
    {
        url = "URI=file:" + Application.persistentDataPath + "/" + "BDPIV"; //Caminho do Banco de Dados
        conectarBD(url);
        criarBancoDados();
    }
    public string nomeUsu;
    static private string url;
    static private IDbConnection dbconn = null;
    public InputField usuCriar, usu, senha, senhaCriar;
	public int maiorPont;
	public GameObject painelInterfaceEntrar, painelInterfaceLogin, painelInterfaceRegister;
	public static jogador Usuario;
	// Use this for initialization
	void Start () {
		Usuario = new jogador ();
		painelInterfaceEntrar.SetActive (true);
		painelInterfaceLogin.SetActive (false);
		painelInterfaceRegister.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public string Nick
	{
		get { return usuCriar.text; }
		set { usuCriar.text = value; }
	}

	public string NickLogin
	{
		get { return usu.text; }
		set { usu.text = value; }
	}

	public string SenhaLogin
	{
		get { return senha.text; }
		set { senha.text = value; }
	}

	public string Senha
	{
		get { return senhaCriar.text; }
		set { senhaCriar.text = value; }
	}

	public void Registrar(){
		maiorPont = 0;
        
        IDataReader dados = pesquisarSQL("select Usu_Id, Usu_Nick, Usu_senha, Usu_maiorPont from Usuario where Usu_senha = " +Senha);
        dados.Read();

        string sql = "insert into Usuario (Usu_Nick, Usu_senha, Usu_MaiorPont)" + " values ('" + Nick + "', '" + Senha + "', '" + maiorPont + "')";

		string linhasAfetadas = executarSQL(sql);

  //      if (dados.GetString (1) == NickLogin && dados.GetString (2) == SenhaLogin) { 
		//	Usuario.Id = dados.GetInt32 (0);
		//	Usuario.Nick = dados.GetString (1);
		//	Usuario.Senha = dados.GetString (2);
		//	Usuario.MaiorPontos = dados.GetInt32 (3);
		//}
		print (linhasAfetadas);
		IrLobby ();
	}

	public void Entrar(){
		
		IDataReader dados = pesquisarSQL("select Usu_Id, Usu_Nick, Usu_senha, Usu_maiorPont from Usuario where Usu_senha = " + SenhaLogin);
		dados.Read ();
        nomeUsu = NickLogin;
        Debug.Log(NickLogin);
        if (dados.GetString(1) == NickLogin && dados.GetString(2) == SenhaLogin){
			Usuario.Id 		= dados.GetInt32(0);
			Usuario.Nick 	= dados.GetString(1);
			Usuario.Senha 	= dados.GetString(2);
			Usuario.MaiorPontos 	= dados.GetInt32(3);
            
            Debug.Log(NickLogin);

		}
        
        IrLobby ();
	}

	public void ButtonIrTelaLogin()
	{
		painelInterfaceEntrar.SetActive (false);
		painelInterfaceLogin.SetActive (true);
		painelInterfaceRegister.SetActive (false);
	}

	public void ButtonIrTelaEntrada()
	{
		painelInterfaceEntrar.SetActive (true);
		painelInterfaceLogin.SetActive (false);
		painelInterfaceRegister.SetActive (false);
	}

	public void ButtonIrTelaRegistro()
	{
		painelInterfaceEntrar.SetActive (false);
		painelInterfaceLogin.SetActive (false);
		painelInterfaceRegister.SetActive (true);
	}

	public void ButtonSairJogo()
	{
        Debug.Log("apertou pra sair");
		Application.Quit ();
	}

	public void IrLobby()
	{
		SceneManager.LoadScene ("Lobby");
	}

	public void StartarGame()
	{
		SceneManager.LoadScene ("Game");
	}
}
