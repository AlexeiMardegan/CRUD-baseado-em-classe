using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Pessoa pessoa = new Pessoa();
            List<Pessoa> pessoas = pessoa.listapessoas();
            dgvPessoa.DataSource = pessoas;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == " " && txtCidade.Text == " " && txtEndereco.Text == " ") // abre condicional para verficar se o usuario digitou alguma coisa no form
                {
                    MessageBox.Show("Por Favor, preencha o formulário para inserir!");
                    this.txtNome.Focus();
                }
                else
                {
                    Pessoa pessoa = new Pessoa(); //verifica se o que o usuario botou no banco de dados se já existe lá dentro, se já tem ele lá ira mostrar a mensagem abaixo e deixara os campos brancos.
                    if (pessoa.RegistroRepetido(txtNome.Text, txtCelular.Text, txtEmail.Text) != false)
                    {
                        MessageBox.Show("Este cliente já está em nossa base de dados!");
                        this.txtNome.Focus();
                    }
                    else // se não ele insire os dados e mostra uma mensage.
                    {
                        pessoa.Inserir(txtNome.Text, txtCidade.Text, txtEndereco.Text, txtCelular.Text, txtEmail.Text, txtData_De_Nascimento.Text);
                        MessageBox.Show("Pessoa registrada com sucesso!");
                        List<Pessoa> pessoas = pessoa.listapessoas();
                        dgvPessoa.DataSource = pessoas;
                        txtNome.Text = "";
                        txtCidade.Text = "";
                        txtEndereco.Text = "";
                        txtCelular.Text = "";
                        txtEmail.Text = "";
                        txtData_De_Nascimento.Text = "";
                        this.txtNome.Focus();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }


        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Deseja realmente sair?", "Sair da Aplicação", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)

            {
                Application.Exit();

            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            {
                int Id = Convert.ToInt32(txtId.Text.Trim()); // O ''Trim'' retira os espaço dos campos.
                Pessoa pessoa = new Pessoa();
                pessoa.Atualizar(Id, txtNome.Text, txtCidade.Text, txtEndereco.Text, txtCelular.Text, txtEmail.Text, txtData_De_Nascimento.Text);
                MessageBox.Show("Pessoa atualizada com sucesso!");
                List<Pessoa> pessoas = pessoa.listapessoas();
                dgvPessoa.DataSource = pessoas;
                txtNome.Text = "";
                txtCidade.Text = "";
                txtEndereco.Text = "";
                txtCelular.Text = "";
                txtEmail.Text = "";
                txtData_De_Nascimento.Text = "";
                txtId.Text = "";
                this.txtNome.Focus();
                btnAtualizar.Enabled = false;
                btnExcluir.Enabled = false;
            }


        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == " ") // se receber nada e tentar localizar, ira aparecer a mensagem abaixo.
            { 
                MessageBox.Show("Por favor, digite um ID.");
            this.txtId.Focus();
        }
        else // se não ele pega os dados.
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
        Pessoa pessoa = new Pessoa();
        pessoa.Localizar(Id);
            txtNome.Text = pessoa.nome;
            txtCidade.Text = pessoa.cidade;
            txtEndereco.Text = pessoa.endereco;
            txtCelular.Text = pessoa.celular;
            txtEmail.Text = pessoa.email;
            txtData_De_Nascimento.Text = pessoa.data_de_nascimento;
                btnAtualizar.Enabled = true;
                btnExcluir.Enabled = true;
            }
}
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtId.Text.Trim());
            Pessoa pessoa = new Pessoa();
            pessoa.Excluir(Id);
            MessageBox.Show("Pessoa excluída com sucesso!");
            List<Pessoa> pessoas = pessoa.listapessoas();
            dgvPessoa.DataSource = pessoas;
            txtId.Text = "";
            txtNome.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtEmail.Text = "";
            txtData_De_Nascimento.Text = "";
            txtId.Text = "";
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
        }

 
        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPessoa.Rows[e.RowIndex]; // a linha do meu datagrid vai receber esse objeto ''dgvPessoa'' e vai virar Rows.
                row.Selected = true; // nao importa em qual celula a pessoa selecionar ela ira considerar a linha inteira em vez de uma celulas apenas.
                txtId.Text = row.Cells[0].Value.ToString(); 
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCidade.Text = row.Cells[2].Value.ToString();
                txtEndereco.Text = row.Cells[3].Value.ToString();
                txtCelular.Text = row.Cells[4].Value.ToString();
                txtEmail.Text = row.Cells[5].Value.ToString();
                txtData_De_Nascimento.Text = row.Cells[6].Value.ToString();
            }
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
        }

  
    }
}
   
