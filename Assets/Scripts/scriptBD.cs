using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class scriptBD : MonoBehaviour {

    static private string url;
    static private IDbConnection dbconn = null;

    static public void criarBancoDados()
    {
        try
        {
            print("Testando existencia do banco de dados");
            IDataReader sqlTest = pesquisarSQL("select Usu_Id, Usu_Nick, Usu_senha, Usu_maiorPont from Usuario");
            print("Banco de dados ja foi criado...");
        }catch(Exception e)
        {
            print("Criando banco de dados....");
            String sqlCreate = "CREATE TABLE Questoes (\n" +
                        "       Quest_Id       integer not null primary key autoincrement,\n" +
                        "       Quest_Per       varchar(20),\n" +
                        "       Quest_RespCer   varchar(10),\n" +
                        "       Quest_RespErrA  varchar(10),\n" +
                        "       Quest_RespErrB  varchar(10),\n" +
                        "       Quest_Tempo     Integer\n" +
                        ");\n" +

                        "INSERT INTO Questoes VALUES(1,'3+5.2–4:2','11','6','8',10);\n" +
                        "INSERT INTO Questoes VALUES(2,'9+(21–15).2','21','29','17',10);\n" +
                        "INSERT INTO Questoes VALUES(3,'67–[(98+4)–34] ','-1','1','20',12);\n" +
                        "INSERT INTO Questoes VALUES(4,'3+{7+[23–(10+3)–2]–1','3','4','18',14);\n" +
                        "INSERT INTO Questoes VALUES(5,'6:2(1+2)','9','6','3',10);\n" +
                        "INSERT INTO Questoes VALUES(6,'(10+5)-(1+6)','8','22','16',12);\n" +
                        "INSERT INTO Questoes VALUES(7,'9-(5-1+2)','3','15','12',10);\n" +
                        "INSERT INTO Questoes VALUES(8,'50-[37-(15-8)]','20','80','40',12);\n" +
                        "INSERT INTO Questoes VALUES(9,'52-{12+[15-(8-4)]}','29','83','75',16);\n" +
                        "INSERT INTO Questoes VALUES(10,'10:2+8 ','13','1','0',10);\n" +
                        "INSERT INTO Questoes VALUES(11,'64:8+5.5-3 ','30','18','12',14);\n" +
                        "INSERT INTO Questoes VALUES(12,'32+12:2','38','22','11',10);\n" +
                        "INSERT INTO Questoes VALUES(13,'90-[25+(5.2-1)+3]','53','57','73',18);\n" +
                        "INSERT INTO Questoes VALUES(14,'10+[4+(7.3+1)]-3','33','31','25',16);\n" +
                        "INSERT INTO Questoes VALUES(15,'3.(4-3)+8 ','12','17','33',14);\n" +
                        "INSERT INTO Questoes VALUES(16,'15.(3+2)+1','76','48','90',12);\n" +
                        "INSERT INTO Questoes VALUES(17,'6-(-8.-2)+32','22','60','54',14);\n" +
                        "INSERT INTO Questoes VALUES(18,'25-[10+(7-4)]','12','27','18',12);\n" +
                        "INSERT INTO Questoes VALUES(19,'32+[10-(9-4)+8]','45','37','42',12);\n" +
                        "INSERT INTO Questoes VALUES(20,'70:7-1','9','10','64',12);\n" +
                        "INSERT INTO Questoes VALUES(21,'25+(8:2+1)-1','29','10','33',14);\n" +
                        "INSERT INTO Questoes VALUES(22,'(12+2.5)-8','14','6','10',12);\n" +
                        "INSERT INTO Questoes VALUES(23,'60-[8+(10-2):2]','46','30','59',14);\n" +
                        "INSERT INTO Questoes VALUES(24,'20:10+10','12','20','1',10);\n" +
                        "INSERT INTO Questoes VALUES(25,'25-[10-(2x3+1)]','22','17','23',14);\n" +

                        "CREATE TABLE Usuario (\n" +
                        "       Usu_Id        integer not null primary key autoincrement,\n" +
                        "       Usu_Nick      varchar(12),\n" +
                        "       Usu_senha     varchar(20),\n" +
                        "       Usu_MaiorPont integer(20)\n" +
                        ");\n" +
                        "INSERT INTO Usuario VALUES(1,'leticia','123',0);\n" +
                        "INSERT INTO Usuario VALUES(2,'bolo','9050',0);\n" +
                        "INSERT INTO Usuario VALUES(3,'wartemax','123',0);\n" +
                        "DELETE FROM sqlite_sequence;\n" +
                        "INSERT INTO sqlite_sequence VALUES('Questoes',25);\n" +
                        "INSERT INTO sqlite_sequence VALUES('Usuario',12);\n";
            try
            {
                pesquisarSQL(sqlCreate);
                print("Banco de dados criado");
            }catch(Exception e1)
            {
                print(e1.Message);
                print(e1.StackTrace);
                print("Erro ao criar o BD"); 

            }
        }


    }

    static public void conectarBD(string url) {
        if (dbconn == null) {
            try
            {
                print(url);
                dbconn = new SqliteConnection(url);
                dbconn.Open(); //Abre uma conexao com o BD 
                print("Conectou com sucesso"); // apenas para teste
            }
            catch (Exception e)
            {
                print(e.Message);
                print(e.StackTrace);
                print("Erro ao conectar no BD"); // apenas para teste
            }
        }
    }

	static private void desconectarBD(IDbConnection dbconn) {
		dbconn.Close();
		dbconn = null;
	}

	static protected string executarSQL(string sql) {
		int resp = 0;
		try {
			IDbCommand dbcmd = dbconn.CreateCommand();
			dbcmd.CommandText = sql;
			resp = dbcmd.ExecuteNonQuery();
		}
		catch {
			resp = 0;
		}
		print ("Resp = " + resp); // apenas para teste
		if (resp == 0) {
			return "Erro";
		} else {
			return "Sucesso";
		}
	}

	static protected IDataReader pesquisarSQL(string sql) {
		IDbCommand dbcmd = dbconn.CreateCommand();
		dbcmd.CommandText = sql;
		IDataReader data = dbcmd.ExecuteReader();
		return data;
	}
	void OnDisable() {
		dbconn.Close();
		dbconn = null;
		Debug.Log ("encerrou");
	}
	void OnApplicatinoQuit(){
		dbconn.Close();
		dbconn = null;

		Debug.Log ("encerrou");
	}

}
