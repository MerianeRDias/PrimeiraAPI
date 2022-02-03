using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MinhaPrimeiraApi.Conexoes
{
    public class Sql
    {
        private readonly SqlConnection _conexao;

        public Sql()
        {
            string conexao = System.IO.File.ReadAllText(@"C:\Users\Meriane\Documents\RumoAcademy\VisualStudio\conexao\stringConexaoES01.txt");
            this._conexao = new SqlConnection(conexao);

        }


        public void InserirCliente(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string sql = @"INSERT INTO Cliente
                                (Cpf,Nome,Genero,Idade,Nacionalidade)
                               VALUES
                                (@cpf, @nome, @sexo , @idade, @nacionalidade);";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("sexo", cliente.Sexo);
                    cmd.Parameters.AddWithValue("idade", cliente.Idade);
                    cmd.Parameters.AddWithValue("nacionalidade", cliente.Nacionalidade);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }

        public void AtualizarCliente(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string sql = @"UPDATE Cliente
                                   SET Nome = @Nome
                                      ,Genero = @Genero
                                      ,Nacionalidade = @Nacionalidade
                                      ,Idade = @Idade
                                 WHERE Cpf = @Cpf";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("Cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("Genero", cliente.Sexo);
                    cmd.Parameters.AddWithValue("Idade", cliente.Idade);
                    cmd.Parameters.AddWithValue("Nacionalidade", cliente.Nacionalidade);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }

        public bool VerificarExistenciaCliente(string cpf)
        {


            try
            {
                _conexao.Open();

                string sql = @"select Count(Cpf) AS total from Cliente WHERE Cpf = @Cpf;";

                using (SqlCommand cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("cpf", cpf);
                    return System.Convert.ToBoolean(cmd.ExecuteScalar());

                }
            }
            finally
            {
                _conexao.Close();
            }


        }

        public void DeletarCliente(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string sql = @"DELETE FROM Cliente
                                 WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public Entidades.Cliente SelecionarCliente(string cpf)
        {
            try
            {
                _conexao.Open();

                string sql = @"Select * FROM Cliente
                                 WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(sql, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cpf);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var cliente = new Entidades.Cliente();
                        cliente.Cpf = cpf;
                        cliente.Nome = rdr["Nome"].ToString();
                        cliente.Nacionalidade = rdr["Nacionalidade"].ToString();
                        cliente.Idade = Convert.ToInt16(rdr["Idade"]);
                        cliente.Sexo = rdr["Genero"].ToString();

                        return cliente;
                    }
                    else
                    {
                        throw new InvalidOperationException("Cpf " + cpf + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public List<Entidades.Cliente> ListarClientes()
        {
            var clientes = new List<Entidades.Cliente>();
            try
            {
                _conexao.Open();

                string sql = @"Select * FROM Cliente";

                using (var cmd = new SqlCommand(sql, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var cliente = new Entidades.Cliente();
                        cliente.Cpf = rdr["Cpf"].ToString();
                        cliente.Nome = rdr["Nome"].ToString();
                        cliente.Nacionalidade = rdr["Nacionalidade"].ToString();
                        cliente.Idade = Convert.ToInt16(rdr["Idade"]);
                        cliente.Sexo = rdr["Genero"].ToString();

                        clientes.Add(cliente);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return clientes;



        }

    }
}