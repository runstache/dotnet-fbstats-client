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
    public partial class frmGameLog : Form
    {
        private long statId;        
        private IRepository repo;
        private GameStat stat;

        public frmGameLog(long statId)
        {
            this.statId = statId;
            repo = RepositoryFactory.NewRepository();

            InitializeComponent();
            
        }

        private void frmGameLog_Load(object sender, EventArgs e)
        {
            try
            {
                stat = repo.GetStat(statId);

                LoadTeams();
                if (stat.TeamId > 0)
                {
                    cmbTeam.SelectedValue = stat.TeamId;
                }

                if (stat.OpponentId > 0)
                {
                    cmbOpponent.SelectedValue = stat.OpponentId;
                }

                if (stat.WeekNumber > 0)
                {
                    txtWeek.Text = stat.WeekNumber.ToString();
                }

                LoadGameStat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadTeams()
        {
            List<Team> teams = repo.GetTeams().ToList();
            List<Team> opponents = repo.GetTeams().ToList();

            if (teams != null && teams.Count > 0)
            {
                cmbTeam.DataSource = teams;
                cmbTeam.ValueMember = "Id";
                cmbTeam.DisplayMember = "TeamName";
            }

            if (opponents != null && opponents.Count > 0)
            {
                cmbOpponent.DataSource = opponents;
                cmbOpponent.DisplayMember = "TeamName";
                cmbOpponent.ValueMember = "Id";
            }
        }

        private GameStat PopulateStat()
        {
            return new GameStat()
            {
                AvgPerCompletion = TypeHelper.DefaultDouble(txtYdsPerPass.Text),
                AvgPerRush = TypeHelper.DefaultDouble(txtYdsPerRush.Text),
                ConversionRate = TypeHelper.DefaultDouble(txt3rdDownRatio.Text),
                FieldGoals = TypeHelper.DefaultInteger(txtFieldGoal.Text),
                ForcedFumbles = TypeHelper.DefaultInteger(txtFumbles.Text),
                Id = statId,
                OpponentId = TypeHelper.DefaultInteger(cmbOpponent.SelectedValue.ToString()),
                PassAttempts = TypeHelper.DefaultInteger(txtAttemptPass.Text),
                PassCompleted = TypeHelper.DefaultInteger(txtAttemptPass.Text),
                PassOverFifteen = TypeHelper.DefaultInteger(txtBigPass.Text),
                PassYards = TypeHelper.DefaultInteger(txtPassYards.Text),
                Penalties = TypeHelper.DefaultInteger(txtPenalties.Text),
                PenaltyYards = TypeHelper.DefaultInteger(txtPenaltyYds.Text),
                PointsAllowed = TypeHelper.DefaultInteger(txtGiven.Text),
                PointsScored = TypeHelper.DefaultInteger(txtScored.Text),
                Punts = TypeHelper.DefaultInteger(txtPunts.Text),
                TotalPlays = TypeHelper.DefaultInteger(txtTotalPlays.Text),
                TeamId = TypeHelper.DefaultInteger(cmbTeam.SelectedValue.ToString()),
                Touchdowns = TypeHelper.DefaultInteger(txtTouchdown.Text),
                Turnovers = TypeHelper.DefaultInteger(txtTurnOver.Text),
                ReturnYards = TypeHelper.DefaultInteger(txtReturnYds.Text),
                RushAttempts = TypeHelper.DefaultInteger(txtRushAttempt.Text),
                RushOverTen = TypeHelper.DefaultInteger(txtBigRun.Text),
                RushYards = TypeHelper.DefaultInteger(txtRushYards.Text),
                WeekNumber = TypeHelper.DefaultInteger(txtWeek.Text)
                
            };

           
        }

        private void LoadGameStat()
        {
            if (stat != null)
            {
                txt3rdDownRatio.Text = stat.ConversionRate.ToString();
                txtAttemptPass.Text = stat.PassAttempts.ToString();
                txtBigPass.Text = stat.PassOverFifteen.ToString();
                txtBigRun.Text = stat.RushOverTen.ToString();
                txtCompletedPass.Text = stat.PassCompleted.ToString();
                txtFieldGoal.Text = stat.FieldGoals.ToString();
                txtFumbles.Text = stat.ForcedFumbles.ToString();
                txtPassYards.Text = stat.PassYards.ToString();
                txtPenalties.Text = stat.Penalties.ToString();
                txtPenaltyYds.Text = stat.PenaltyYards.ToString();
                txtPunts.Text = stat.Punts.ToString();
                txtReturnYds.Text = stat.ReturnYards.ToString();
                txtRushAttempt.Text = stat.RushAttempts.ToString();
                txtRushYards.Text = stat.RushYards.ToString();
                txtTotalPlays.Text = stat.TotalPlays.ToString();
                txtTouchdown.Text = stat.Touchdowns.ToString();
                txtTurnOver.Text = stat.Turnovers.ToString();
                txtWeek.Text = stat.WeekNumber.ToString();
                txtYdsPerPass.Text = stat.AvgPerCompletion.ToString();
                txtYdsPerRush.Text = stat.AvgPerRush.ToString();
                txtScored.Text = stat.PointsScored.ToString();
                txtGiven.Text = stat.PointsAllowed.ToString();
            } 
            else
            {
                MessageBox.Show("Stat Not Found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txt3rdDownConverted_TextChanged(object sender, EventArgs e)
        {
            CalculateRatio();
        }

        private void txt3rdDownAttempts_TextChanged(object sender, EventArgs e)
        {
            CalculateRatio();
        }

        private void CalculateRatio()
        {
            if (!string.IsNullOrEmpty(txt3rdDownAttempts.Text) && !string.IsNullOrEmpty(txt3rdDownConverted.Text))
            {
                double attempt = TypeHelper.DefaultDouble(txt3rdDownAttempts.Text);
                double converted = TypeHelper.DefaultDouble(txt3rdDownConverted.Text);

                if (converted > 0)
                {
                    double ratio = Math.Round(converted / attempt, 2);
                    txt3rdDownRatio.Text = ratio.ToString();

                }
            }
        }

        private void btnAddTurnOver_Click(object sender, EventArgs e)
        {
            int turnover = TypeHelper.DefaultInteger(txtTurnOver.Text);
            turnover++;
            txtTurnOver.Text = turnover.ToString();
        }

        private void btnAddRun_Click(object sender, EventArgs e)
        {
            int bigrun = TypeHelper.DefaultInteger(txtBigRun.Text);
            bigrun++;
            txtBigRun.Text = bigrun.ToString();
        }

        private void btnAddPass_Click(object sender, EventArgs e)
        {
            int bigpass = TypeHelper.DefaultInteger(txtBigPass.Text);
            bigpass++;
            txtBigPass.Text = bigpass.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TypeHelper.DefaultInteger(cmbTeam.SelectedValue.ToString()) > 0 && TypeHelper.DefaultInteger(cmbOpponent.SelectedValue.ToString()) > 0 && TypeHelper.DefaultInteger(txtWeek.Text) > 0)
            {

                try
                {
                    stat = PopulateStat();
                    stat = repo.Update(stat);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            } 
            else
            {
                MessageBox.Show("Missing Team, Opponent, and/or Week", "Missing Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            int teamId = TypeHelper.DefaultInteger(cmbTeam.SelectedValue.ToString());
            int opponentId = TypeHelper.DefaultInteger(cmbOpponent.SelectedValue.ToString());
            int week = TypeHelper.DefaultInteger(txtWeek.Text);

            txt3rdDownAttempts.Text = "";
            txt3rdDownConverted.Text = "";

            stat = PopulateStat();
            repo.Update(stat);

            GameStat oppStat = repo.GetStats(opponentId).Where(c => c.OpponentId == teamId && c.WeekNumber == week).FirstOrDefault();
            if (oppStat == null)
            {
                oppStat = new GameStat();
                oppStat.TeamId = opponentId;
                oppStat.OpponentId = teamId;
                oppStat.WeekNumber = week;
                oppStat = repo.Insert(oppStat);
            }

            stat = oppStat;
            statId = oppStat.Id;
            LoadGameStat();
            cmbTeam.SelectedValue = stat.TeamId;
            cmbOpponent.SelectedValue = stat.OpponentId;
            txtWeek.Text = stat.WeekNumber.ToString();

        }
    }
}
