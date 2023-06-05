using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SemDBMongoDB
{
    public partial class Form1 : Form
    {
        
        //SportClub.cs
        IMongoCollection<SportClub> sportClubCollection;
        //Sportsman.cs
        IMongoCollection<Sportsman> sportsmanCollection;
        //Coach.cs
        IMongoCollection<Coach> coachCollection;
        //SportLocation.cs
        IMongoCollection<SportLocation> sportLocationCollection;
        //Tournament.cs
        IMongoCollection<Tournament> tournamentCollection;
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //App.config: connectionString = указать путь к БД, name = название соединения 
            var connectionString = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);
            //Название коллекции в БД
            sportClubCollection = database.GetCollection<SportClub>("sportClub");
            sportsmanCollection = database.GetCollection<Sportsman>("sportsman");
            coachCollection = database.GetCollection<Coach>("coach");
            sportLocationCollection = database.GetCollection<SportLocation>("sportLocation");
            tournamentCollection = database.GetCollection<Tournament>("tournament");
            loadSportClubData();
            loadSportsmanData();
            loadCoachData();
            loadSportLocationData();
            loadTournament();
        }
        
        private void loadTournament()
        {
            List<Tournament> tournament = tournamentCollection.AsQueryable().ToList();
            dataGridViewTournament.DataSource = tournament;
        }

        private void loadSportLocationData()
        {
            List<SportLocation> sportLocation = sportLocationCollection.AsQueryable().ToList();
            dataGridViewSportLoc.DataSource = sportLocation;
        }
            
        
        private void loadSportsmanData()
        {

            List<Sportsman> sporstman = sportsmanCollection.AsQueryable().ToList();
            dataGridViewSportsman.DataSource = sporstman;
            

            var coachIdColumn = new DataGridViewTextBoxColumn();
            coachIdColumn.HeaderText = "Coach_Id";
            dataGridViewSportsman.Columns.Add(coachIdColumn);

            var sportsmanSportTypeColumn = new DataGridViewTextBoxColumn();
            sportsmanSportTypeColumn.HeaderText = "Sport_Type";
            dataGridViewSportsman.Columns.Add(sportsmanSportTypeColumn);

            foreach (DataGridViewRow row in dataGridViewSportsman.Rows)
            {
                var sportsman = row.DataBoundItem as Sportsman;
                if (sportsman != null)
                {
                    var sportTypes = string.Join(", ", sportsman.Sport_Type);
                    row.Cells[sportsmanSportTypeColumn.Index].Value = sportTypes;
                }
            }

            foreach (DataGridViewRow row in dataGridViewSportsman.Rows)
            {
                var sportsman = row.DataBoundItem as Sportsman;
                if (sportsman != null)
                {
                    var coachIds = string.Join(", ", sportsman.Coach_Id);
                    row.Cells[coachIdColumn.Index].Value = coachIds;
                }
            }

        }
        private void loadCoachData()
        {
            List<Coach> coach = coachCollection.AsQueryable().ToList();
            dataGridViewCoach.DataSource = coach;
         
            var sportsmanIdColumn = new DataGridViewTextBoxColumn();
            sportsmanIdColumn.HeaderText = "Sportsman_Id";
            dataGridViewCoach.Columns.Add(sportsmanIdColumn);

            dataGridViewCoach.DataSource = coach;
            foreach (DataGridViewRow row in dataGridViewCoach.Rows)
            {
                var coach1 = row.DataBoundItem as Coach;
                if (coach1 != null)
                {
                    var sportsmanIds = string.Join(", ", coach1.Sportsman_Id);
                    row.Cells[sportsmanIdColumn.Index].Value = sportsmanIds;
                }
            }
        }
        private void loadSportClubData()
        {

            List<SportClub> sportClub = sportClubCollection.AsQueryable().ToList();
            dataGridViewSportClub.DataSource = sportClub;
            //loadSportsmanData();
            
        }

        private void buttonSportClubAdd_Click(object sender, EventArgs e)
        {
            var sportclub = new SportClub
            {
                Id = Convert.ToInt32(textBoxSportClubId.Text),
                Name = textBoxSportClubName.Text

            };
            sportClubCollection.InsertOne(sportclub);
            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportClubData();
        }

        private void buttonSportClubUpdate_Click(object sender, EventArgs e)
        {
            
            var filterDefinition = Builders<SportClub>.Filter.Eq("Id", textBoxSportClubId.Text);
            var updateDefinition = Builders<SportClub>.Update
               .Set(a => a.Name, textBoxSportClubName.Text);
            sportClubCollection.UpdateOne(filterDefinition, updateDefinition);
            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportClubData();
        }

        private void buttonSportClubDel_Click(object sender, EventArgs e)
        {
            
            var filterDefinition = Builders<SportClub>.Filter.Eq("Id", textBoxSportClubId.Text);
            sportClubCollection.DeleteOne(filterDefinition);
            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportClubData();
        }

        private void buttonSportsmanAdd_Click(object sender, EventArgs e)
        {
            var sportsman = new Sportsman
            {
                Id = int.Parse(textBoxSportsmanId.Text),
                Last_Name = textBoxSportsmanLastName.Text,
                First_Name = textBoxSportsmanFirstName.Text,
                Sport_Type = new List<string>(textBoxSportsmanSportType.Text.Split(',')),
                Club_Id = int.Parse(textBoxSportsmanClubId.Text),
                Coach_Id = new List<int>(textBoxSportsmanCoachId.Text.Split(',').Select(int.Parse))
            
            };

            sportsmanCollection.InsertOne(sportsman);

            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportsmanData();

        }

        private void buttonSportsmanUpdate_Click(object sender, EventArgs e)
        {

            var filterDefinition = Builders<Sportsman>.Filter.Eq(s => s.Id, int.Parse(textBoxSportsmanId.Text));

            var updateDefinition = Builders<Sportsman>.Update
               .Set(s => s.Last_Name, textBoxSportsmanLastName.Text)
               .Set(s => s.First_Name, textBoxSportsmanFirstName.Text)
               .Set(s => s.Club_Id, int.Parse(textBoxSportsmanClubId.Text))
               .Set(s => s.Sport_Type, new List<string>(textBoxSportsmanSportType.Text.Split(',')))
               .Set(s => s.Coach_Id, new List<int>(textBoxSportsmanCoachId.Text.Split(',').Select(int.Parse)));


            sportsmanCollection.UpdateOne(filterDefinition, updateDefinition);
            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportsmanData();
            
        }

        private void buttonSportsmanDel_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Sportsman>.Filter.Eq(s => s.Id, int.Parse(textBoxSportsmanId.Text));
            sportsmanCollection.DeleteOne(filterDefinition);
            dataGridViewSportsman.Columns.Clear();
            dataGridViewSportsman.Refresh();
            loadSportsmanData();
        }

        private void buttonCoachAdd_Click(object sender, EventArgs e)
        {
            var coach = new Coach
            {
                Id = int.Parse(textBoxCoachId.Text),
                Last_Name = textBoxCoachLastName.Text,
                First_Name = textBoxCoachFirstName.Text,
                Sport_Type = textBoxCoachSportType.Text,
                Sportsman_Id = new List<int>(textBoxCoachSportsmanId.Text.Split(',').Select(int.Parse))

            };

            coachCollection.InsertOne(coach);

            dataGridViewCoach.Columns.Clear();
            dataGridViewCoach.Refresh();
            loadCoachData();    

        }

        private void buttonCoachUpdate_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Coach>.Filter.Eq(s => s.Id, int.Parse(textBoxCoachId.Text));
            var updateDefinition = Builders<Coach>.Update
               .Set(s => s.Last_Name, textBoxCoachLastName.Text)
               .Set(s => s.First_Name, textBoxCoachFirstName.Text)
               .Set(s => s.Sport_Type, textBoxCoachSportType.Text)
               .Set(s => s.Sportsman_Id, new List<int>(textBoxCoachSportsmanId.Text.Split(',').Select(int.Parse)));


            coachCollection.UpdateOne(filterDefinition, updateDefinition);
            dataGridViewCoach.Columns.Clear();
            dataGridViewCoach.Refresh();
            loadCoachData();

        }

        private void buttonCoachDel_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Coach>.Filter.Eq(s => s.Id, int.Parse(textBoxCoachId.Text));
            coachCollection.DeleteOne(filterDefinition);
            dataGridViewCoach.Columns.Clear();
            dataGridViewCoach.Refresh();
            loadCoachData();

        }

        private void buttonSportLocAdd_Click(object sender, EventArgs e)
        {
            var sportLocation = new SportLocation
            {
                Id = int.Parse(textBoxSportLocId.Text),
                Name = textBoxSportLocName.Text,
                Type = textBoxSportLocType.Text,
                Property = BsonDocument.Parse(textBoxSportLocProperty.Text) //Property нужно вводить в формате JSON
            };
            sportLocationCollection.InsertOne(sportLocation);
            dataGridViewSportLoc.Columns.Clear();
            dataGridViewSportLoc.Refresh();
            loadSportLocationData();
        }
        private void buttonSportLocUpdate_Click(object sender, EventArgs e)
        {
            var filter = Builders<SportLocation>.Filter.Eq("_id", int.Parse(textBoxSportLocId.Text));
            var update = Builders<SportLocation>.Update
                .Set("name", textBoxSportLocName.Text)
                .Set("type", textBoxSportLocType.Text)
                //Изменяет текущие Property документа. Новые добавлять можно, удалять уже имеющиеся нельзя
                .Set("property", BsonDocument.Parse(textBoxSportLocProperty.Text));
            sportLocationCollection.UpdateOne(filter, update);
            dataGridViewSportLoc.Columns.Clear();
            dataGridViewSportLoc.Refresh();
            loadSportLocationData();
        }

        private void buttonSportLocDel_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<SportLocation>.Filter.Eq(s => s.Id, int.Parse(textBoxSportLocId.Text));
            sportLocationCollection.DeleteOne(filterDefinition);
            dataGridViewSportLoc.Columns.Clear();
            dataGridViewSportLoc.Refresh();
            loadSportLocationData();
        }


        private void buttonTournamentAdd_Click(object sender, EventArgs e)
        {
            var tournament = new Tournament
            {
                Id = int.Parse(textBoxTournamentId.Text),
                Name = textBoxTournamentName.Text,
                Organizator = textBoxTournamentOrganizator.Text,
                Sport_Type = textBoxTournamentSportType.Text,
                Date = DateTime.Parse(textBoxTournamentDate.Text),
                Sport_Location_Id = int.Parse(textBoxTournamentSportLocId.Text),
                Sportsman_Places = BsonDocument.Parse(textBoxTournamentSportsmanPlace.Text) //SportsmanPlaces нужно вводить в формате JSON
            };
            tournamentCollection.InsertOne(tournament);
            dataGridViewTournament.Columns.Clear();
            dataGridViewTournament.Refresh();
            loadTournament();
        }

        private void buttonTournamentUpdate_Click(object sender, EventArgs e)
        {
            var filter = Builders<Tournament>.Filter.Eq("_id", int.Parse(textBoxTournamentId.Text));
            var update = Builders<Tournament>.Update
                .Set("name", textBoxTournamentName.Text)
                .Set("organizator", textBoxTournamentOrganizator.Text)
                .Set("sport_type", textBoxTournamentSportType.Text)
                .Set("date", DateTime.Parse(textBoxTournamentDate.Text))
                .Set("sport_location_id", int.Parse(textBoxTournamentSportLocId.Text))
                .Set("sportsman_places", BsonDocument.Parse(textBoxTournamentSportsmanPlace.Text));
            tournamentCollection.UpdateOne(filter, update);
            dataGridViewTournament.Columns.Clear();
            dataGridViewTournament.Refresh();
            loadTournament();
        }

        private void buttonTournamentDdel_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Tournament>.Filter.Eq(s => s.Id, int.Parse(textBoxTournamentId.Text));
            tournamentCollection.DeleteOne(filterDefinition);
            dataGridViewTournament.Columns.Clear();
            dataGridViewTournament.Refresh();
            loadTournament(); ;
        }

        
    }

       
}


        
 
 
