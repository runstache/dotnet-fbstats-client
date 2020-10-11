using FbStats.Client.Factories;
using FbStats.Client.Helpers;
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
    public partial class frmAddStat : Form
    {
        private IRepository repo;
        private int teamId;
        public frmAddStat(int teamId)
        {
            this.teamId = teamId;
            repo = RepositoryFactory.NewRepository();
            InitializeComponent();
        }

        private void frmAddStat_Load(object sender, EventArgs e)
        {
            try
            {
                LoadWeeks();
                LoadTeams();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTeams()
        {
            
            List<Team> teams = repo.GetTeams().OrderBy(c => c.TeamName).ToList();
            teams.RemoveAll(c => c.Id == teamId);
            cmbOpponent.DataSource = teams;
            cmbOpponent.DisplayMember = "TeamName";
            cmbOpponent.ValueMember = "Id";

        }

        private void LoadWeeks()
        {
            cmbWeek.Items.Clear();
            for (int i = 1; i < 25; i++)
            {
                cmbWeek.Items.Add(i);
            }
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int week = TypeHelper.DefaultInteger(cmbWeek.SelectedItem.ToString());
            int oppId = TypeHelper.DefaultInteger(cmbOpponent.SelectedValue.ToString());

            if (week > 0 && oppId > 0)
            {
                GameStat stat = repo.GetStats(teamId).Where(c => c.OpponentId == oppId && c.WeekNumber == week).FirstOrDefault();
                if (stat == null)
                {
                    stat = new GameStat();
                    stat.TeamId = teamId;
                    stat.OpponentId = oppId;
                    stat.WeekNumber = week;
                    stat = repo.Insert(stat);
                    
                }
                frmGameLog gameLog = new frmGameLog(stat.Id);
                gameLog.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Select a week and opponent", "Missing values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
