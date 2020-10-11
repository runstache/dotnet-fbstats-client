using FbStats.Client.Factories;
using FbStats.Data.DataObjects;
using FbStats.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FbStats.Client
{
    public partial class frmTeam : Form
    {
        private IRepository repo;
        private int teamId;
        public frmTeam(int teamId)
        {
            repo = RepositoryFactory.NewRepository();
            this.teamId = teamId;
            InitializeComponent();
        }

        private void LoadTeam(int teamId)
        {
            Team team = repo.GetTeam(teamId);
            txtCode.Text = team.TeamCode;
            txtName.Text = team.TeamName;
        }

        private void SaveTeam()
        {
            Team team = new Team()
            {
                TeamCode = txtCode.Text,
                TeamName = txtName.Text,
                Id = teamId
            };
            repo.Update(team);

        }

        private void frmTeam_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTeam(teamId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtCode.Text))
            {
                try
                {
                    SaveTeam();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                frmTeams teamsForm = new frmTeams();
                teamsForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Name and Code are required.", "Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
