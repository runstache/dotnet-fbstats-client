using FbStats.Client.Factories;
using FbStats.Client.Helpers;
using FbStats.Data.DataObjects;
using FbStats.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FbStats.Client
{
    public partial class frmTeams : Form
    {
        private IRepository repo;
        public frmTeams()
        {
            InitializeComponent();
        }

        private void frmTeams_Load(object sender, EventArgs e)
        {
            repo = RepositoryFactory.NewRepository();

            try
            {
                List<Team> teams = repo.GetTeams().ToList();
                lstTeams.DataSource = teams;
                lstTeams.ValueMember = "Id";
                lstTeams.DisplayMember = "TeamName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int teamId = TypeHelper.DefaultInteger(lstTeams.SelectedValue.ToString());
            if (teamId > 0)
            {
                frmTeam teamForm = new frmTeam(teamId);
                teamForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Select a Team.", "Select Teams", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
