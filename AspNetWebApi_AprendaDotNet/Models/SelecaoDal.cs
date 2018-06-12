using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AspNetWebApi_AprendaDotNet.Models
{
    public class SelecaoDal : ISelecaoDal
    {
        readonly string _connectionString;

        public SelecaoDal(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        //Método que obtem todos as selecoes
        public IEnumerable<Selecao> ObterSelecoes()
        {
            var selecaosList = new List<Selecao>();
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("ObterTodasAsSelecoes", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var employee = new Selecao
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        NomeSelecao = reader["Selecao"].ToString(),
                        Continente = reader["Continente"].ToString(),
                        MelhorResultado = reader["MelhorResultado"].ToString(),
                        NumeroParticipacoes = Convert.ToInt32(reader["NumeroParticipacoes"])
                    };

                    selecaosList.Add(employee);
                }
                con.Close();
            }
            return selecaosList;
        }

        //Inclusao de uma nova selecao 
        public int IncluirSelecao(Selecao selecao)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("IncluirSelecao", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@Selecao", selecao.NomeSelecao);
                    cmd.Parameters.AddWithValue("@Continente", selecao.Continente);
                    cmd.Parameters.AddWithValue("@NumeroParticipacoes", selecao.NumeroParticipacoes);
                    cmd.Parameters.AddWithValue("@MelhorResultado", selecao.MelhorResultado);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

        //Atualizar uma selecao
        public int AtualizarSelecao(Selecao selecao)
        {

            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("AtualizarSelecao", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", selecao.Id);
                cmd.Parameters.AddWithValue("@Selecao", selecao.NomeSelecao);
                cmd.Parameters.AddWithValue("@Continente", selecao.Continente);
                cmd.Parameters.AddWithValue("@NumeroParticipacoes", selecao.NumeroParticipacoes);
                cmd.Parameters.AddWithValue("@MelhorResultado", selecao.MelhorResultado);
                con.Open();
                return cmd.ExecuteNonQuery();
            }

        }

        //Obter selecao por id
        public Selecao ObterSelecaoPorId(int id)
        {
            var selecao = new Selecao();

            using (var con = new SqlConnection(_connectionString))
            {
                var query =
                    $"SELECT Id, Selecao, Continente, NumeroParticipacoes, MelhorResultado FROM SelecoesCopa2018 WHERE Id = {id}";
                var cmd = new SqlCommand(query, con);

                con.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        selecao.Id = Convert.ToInt32(reader["Id"]);
                        selecao.NomeSelecao = reader["Selecao"].ToString();
                        selecao.Continente = reader["Continente"].ToString();
                        selecao.NumeroParticipacoes = Convert.ToInt32(reader["NumeroParticipacoes"]);
                        selecao.MelhorResultado = reader["MelhorResultado"].ToString();
                    }
                }
                else
                    return null;
            }
            return selecao;
        }

        //Excluir selecao por id
        public int ExcluirSelecao(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("ExcluirSelecao", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }

        }

    }
}
