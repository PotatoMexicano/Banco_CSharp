﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Banco
{
    static class Control
    {
        static MySqlConnection conn = new MySqlConnection("server=sql157.main-hosting.eu;port=3306;User Id=u736501739_team;database=u736501739_bank;password=Pimenta10");

        public static bool Conectar()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Usuario Login(int agencia, int conta, string senha)
        {
            MySqlCommand command = new MySqlCommand();
            conn.Open();
            command.Connection = conn;
            command.Parameters.Add("@agencia", MySqlDbType.Int32).Value = agencia;
            command.Parameters.Add("@conta", MySqlDbType.Int32).Value = conta;
            command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;
            command.CommandText = "SELECT * FROM usuarios where agencia = @agencia AND conta = @conta AND senha = @senha";

            
            MySqlDataReader r = command.ExecuteReader();
            if (r.Read())
            {
                Usuario usuario = new Usuario(r.GetInt32(0), r.GetString(1), r.GetString(2), r.GetString(3), r.GetString(4), r.GetInt32(5), r.GetInt32(6), r.GetString(7), r.GetInt32(8), r.GetChar(9), r.GetInt32(10));
                conn.Close();
                return usuario;

            }
            else
            {
                conn.Close();
                return null;
            }
        }

        public static void Cadastrar(string nome, string sobrenome, string cpf, string rg, int agencia, string senha, int tipo, char sexo, int idade, int conta)
        {

            MySqlCommand signup = new MySqlCommand();
            conn.Open();
            signup.Connection = conn;
            signup.Parameters.AddWithValue("@nome", nome);
            signup.Parameters.AddWithValue("@sobrenome", sobrenome);
            signup.Parameters.AddWithValue("@cpf", cpf);
            signup.Parameters.AddWithValue("@rg", rg);
            signup.Parameters.Add("@agencia", MySqlDbType.Int32).Value = agencia;
            signup.Parameters.AddWithValue("@senha", senha);
            signup.Parameters.Add("@tipo", MySqlDbType.Int32).Value = tipo;
            signup.Parameters.Add("@sexo", MySqlDbType.VarChar).Value = sexo;
            signup.Parameters.Add("@idade", MySqlDbType.Int32).Value = idade;
            signup.Parameters.Add("@conta", MySqlDbType.Int32).Value = conta;

            signup.CommandText = "INSERT INTO usuarios VALUES ('',@nome,@sobrenome,@cpf,@rg,@conta,@agencia,@senha,@tipo,@sexo,@idade)";
            signup.ExecuteNonQuery();
            conn.Close();
        }

        public static Conta Extrato(int id)
        {
            MySqlCommand extrato = new MySqlCommand();
            conn.Open();
            extrato.Connection = conn;
            extrato.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            extrato.CommandText = "SELECT * FROM corrente WHERE id_usuario = @id";

            MySqlDataReader r = extrato.ExecuteReader();
            if (r.Read())
            {
                Conta conta = new Conta(r.GetInt32(0), r.GetInt32(1), r.GetDouble(2), r.GetDateTime(3));
                conn.Close();
                return conta;
            }
            else{
                conn.Clone();
                return null;
            }
        }

        public static void Depositar(double valor, int id)
        {
            MySqlCommand dep = new MySqlCommand();
            conn.Open();
            dep.Connection = conn;
            dep.Parameters.Add("@valor", MySqlDbType.Double).Value = valor;
            dep.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            dep.CommandText = "UPDATE corrente SET saldo = saldo + @valor, ultima_alteracao = CURRENT_TIMESTAMP WHERE id_usuario = @id";
            dep.ExecuteNonQuery();
            conn.Close();
        }
    }
}
