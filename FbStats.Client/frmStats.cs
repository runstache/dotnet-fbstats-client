using FbStats.Client.Factories;
using FbStats.Client.Helpers;
using FbStats.Client.Models;
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
    public partial class frmStats : Form
    {
        private IRepository repo;

        public frmStats()
        {
            repo = RepositoryFactory.NewRepository();
            InitializeComponent();
        }

        private void frmStats_Load(object sender, EventArgs e)
        {
           
            try
            {
                List<Team> teams = repo.GetTeams().OrderBy(c => c.TeamName).ToList();
                cmbTeam.DataSource = teams;
                cmbTeam.ValueMember = "Id";
                cmbTeam.DisplayMember = "TeamName";

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int teamId = TypeHelper.DefaultInteger(cmbTeam.SelectedValue.ToString());
                List<GameStat> stats = repo.GetStats(teamId).OrderBy(c => c.WeekNumber).ToList();
                List<DisplayModel> models = new List<DisplayModel>();
                foreach (GameStat stat in stats)
                {
                    Team team = repo.GetTeam(stat.TeamId);
                    Team opp = repo.GetTeam(stat.OpponentId);

                    DisplayModel model = new DisplayModel()
                    {
                        TeamName = team.TeamName,
                        OpponentName = opp.TeamName,
                        StatId = stat.Id,
                        WeekNumber = stat.WeekNumber
                        
                    };
                    models.Add(model);

                }

                grdResults.DataSource = models;
                grdResults.Columns["StatId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayModel model = (DisplayModel)grdResults.SelectedRows[0].DataBoundItem;
                frmGameLog gameLog = new frmGameLog(model.StatId);
                gameLog.Show();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int teamId = TypeHelper.DefaultInteger(cmbTeam.SelectedValue.ToString());
            if (teamId > 0)
            {
                frmAddStat add = new frmAddStat(teamId);
                add.Show();
            }
            else
            {
                MessageBox.Show("Select a Team.", "Select Team", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
    }
}
