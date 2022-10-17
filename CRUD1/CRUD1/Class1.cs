using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD1
{
    class Pessoa
    {
        public int Id { get; set; }         //|  
        public string nome { get; set; }    //|  
        public string cidade { get; set; }  //|  
        public string endereco { get; set; }//|    Esse é o construtor, que deve seguir o banco de dados.
        public string celular { get; set; } //| 
        public string email { get; set; }   //| 
        public string data_de_nascimento { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\Desktop\\CRUD1\\CRUD1\\DbPessoa1.mdf;Integrated Security=True");
        public List<Pessoa> listapessoas()
        {
            List<Pessoa> li = new List<Pessoa>();
            string sql = "SELECT * FROM Pessoa";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pessoa p = new Pessoa();                                                       
                p.Id = (int)dr["Id"];                                                       
                p.nome = dr["nome"].ToString();                                              
                p.cidade = dr["cidade"].ToString();                                         
                p.endereco = dr["endereco"].ToString();                                      
                p.celular = dr["celular"].ToString();                                        
                p.email = dr["email"].ToString();                                            
                p.data_de_nascimento = dr["data_de_nascimento"].ToString();                 
                
                li.Add(p);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string nome, string cidade, string endereco, string celular, string email, string data_de_nascimento)
        {
            string sql = "INSERT INTO Pessoa(nome,cidade,endereco,celular,email,data_de_nascimento) VALUES ('" + nome + "','" + cidade + "','" + endereco + "','" + celular + "','" + email + "','" + data_de_nascimento + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Atualizar(int Id, string nome, string cidade, string endereco, string celular, string email, string data_de_nascimento)
        {
            string sql = "UPDATE Pessoa SET nome='" + nome + "',cidade='" + cidade + "',endereco='" + endereco + "',celular='" + celular + "',email='" + email + "',data_de_nascimento='" + data_de_nascimento + "' WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(int Id) // usa o construtor
        {
            string sql = "DELETE FROM Pessoa WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Pessoa WHERE Id='" + Id + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                cidade = dr["cidade"].ToString();
                endereco = dr["endereco"].ToString();
                celular = dr["celular"].ToString();
                email = dr["email"].ToString();
                data_de_nascimento = dr["data_de_nascimento"].ToString();
            }
            dr.Close();
            con.Close();
        }
        public bool RegistroRepetido(string nome, string celular, string email)
        {
            string sql= "SELECT * FROM Pessoa WHERE nome='"+nome+"' AND celular='"+celular+"' AND email='"+email+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if(result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}
