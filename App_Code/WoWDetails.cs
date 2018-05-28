using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WoWDetails
{
    public class WoWCharacter : IComparable
    {
        private int agility; 
        private int armour;
        private string charClass; 
        private int databaseId;
        private int defence;
        private double dodgePercent; 
        private int health;
        private int intellect;
        private int level;
        private string logonName; 
        private int mana; 
        private string name;

        private string race;
        private string sex; 
        private int spirit;
        private int stamina;
        private int strength;
        private int timePlayed; 
        private int timePlayedThisLevel;
        private string title;

        private List<Profession> professionList;
        private List<Quest> questList;
        private List<Reputation> reputationList;
        private List<Recipe> recipeList;

        public WoWCharacter(string name)
        {
            this.name = name;
        }
        public WoWCharacter()
        {
        }

        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
            }
        }
        public int Armour
        {
            get
            {
                return armour;
            }
            set
            {
                armour = value;
            }
        }
        public string CharClass
        {
            get
            {
                if (charClass != null)
                {
                    return charClass;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                charClass = value;
            }
        }
        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public int Defence
        {
            get
            {
                return defence;
            }
            set
            {
                defence = value;
            }
        }
        public double DodgePercent
        {
            get
            {
                return dodgePercent;
            }
            set
            {
                dodgePercent = value;
            }
        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }
        public int Intellect
        {
            get
            {
                return intellect;
            }
            set
            {
                intellect = value;
            }
        }
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        public string LogonName
        {
            get
            {
                if (logonName != null)
                {
                    return logonName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                logonName = value;
            }
        }
        public int Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
            }
        }
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }
        public string Race
        {
            get
            {
                if (race != null)
                {
                    return race;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                race = value;
            }
        }
        public string Sex
        {
            get
            {
                if (sex != null)
                {
                    return sex;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                sex = value;
            }
        }
        public int Spirit
        {
            get
            {
                return spirit;
            }
            set
            {
                spirit = value;
            }
        }
        public int Stamina
        {
            get
            {
                return stamina;
            }
            set
            {
                stamina = value;
            }
        }
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }
        public int TimePlayed
        {
            get
            {
                return timePlayed;
            }
            set
            {
                timePlayed = value;
            }
        }
        public int TimePlayedThisLevel
        {
            get
            {
                return timePlayedThisLevel;
            }
            set
            {
                timePlayedThisLevel = value;
            }
        }
        public string Title
        {
            get
            {
                if (title != null)
                {
                    return title;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                title = value;
            }
        }

        public List<Profession> ProfessionList
        {
            get
            {
                return professionList;
            }
            set
            {
                professionList = value;
            }
        }
        public List<Quest> QuestList
        {
            get
            {
                return questList;
            }
            set
            {
                questList = value;
            }
        }
        public List<Recipe> RecipeList
        {
            get
            {
                return recipeList;
            }
            set
            {
                recipeList = value;
            }
        }
        public List<Reputation> ReputationList
        {
            get
            {
                return reputationList;
            }
            set
            {
                reputationList = value;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is WoWCharacter)
            {
                WoWCharacter otherChar = (WoWCharacter)obj;
                return Name.CompareTo(otherChar.Name);
            }
            throw new ArgumentException("Object is not a WoW Character");
        }
        public int GetDBID(SqlConnection myConnection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand IDQuery = new SqlCommand("select ID from Character where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            int CharID;

            if (myReader.Read())
            {
                CharID = (short)myReader["ID"];
            }
            else
            {
                CharID = 0;
            }
            myReader.Close();
            return CharID;
        }
        public void ReadFromDB()
        {
            // First create the connection, then read the Character from the database.
            // This method expects the this.Name field to already be populated, else it won't find anything.

            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";
            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            
            SqlCommand IDQuery = new SqlCommand("select * from Character where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();

            if (myReader.Read())
            {
                this.DatabaseId = (short)myReader["ID"];
                this.LogonName = (string)myReader["LogonName"];
                this.Agility = (short)myReader["Agility"];
                this.Armour = (short)myReader["Armour"];
                this.CharClass = (string)myReader["CharClass"];
                this.DodgePercent = (double)myReader["DodgePercent"];
                this.Health = (short)myReader["Health"];
                this.Intellect = (short)myReader["Intellect"];
                this.Level = (short)myReader["Level"];
                this.Mana = (short)myReader["Mana"];
                this.Race = (string)myReader["Race"];
                this.Sex = (string)myReader["Sex"];
                this.Spirit = (short)myReader["Spirit"];
                this.Strength = (short)myReader["Strength"];
                this.TimePlayed = (int)myReader["TimePlayed"];
                this.TimePlayedThisLevel = (int)myReader["TimePlayedThisLevel"];
                this.Title = (string)myReader["Title"];
            }
            else
            {
                // Cannot find anything in the database - so blank out the WoWCharacter.
                this.DatabaseId = 0;
            }
            myReader.Close();

            // Now we have the character loaded, get their Professions.

            if (this.DatabaseId != 0)
            {
                IDQuery = new SqlCommand("select a.* from Profession a, Char_Prof b where a.ID = b.ProfessionID and b.CharID = @CharID order by a.Name", myConnection);
                IDParam = new SqlParameter();
                IDParam.ParameterName = "@CharID";
                IDParam.Value = this.DatabaseId;
                IDQuery.Parameters.Add(IDParam);

                myReader = IDQuery.ExecuteReader();

                if (myReader.Read())
                {
                    this.professionList = new List<Profession>();
                    Profession profession = new Profession((string)myReader["Name"]);
                    professionList.Add(profession); 
                    
                    while (myReader.Read())
                    {
                        Profession professionInner = new Profession((string)myReader["Name"]);
                        professionList.Add(professionInner);
                    }
                }
                myReader.Close();
            }
            // Next work through their recipes.

            if (this.DatabaseId != 0)
            {
                IDQuery = new SqlCommand("select a.* from Recipe a, Char_Recipe b where a.ID = b.RecipeID and b.CharID = @CharID order by a.Name", myConnection);
                IDParam = new SqlParameter();
                IDParam.ParameterName = "@CharID";
                IDParam.Value = this.DatabaseId;
                IDQuery.Parameters.Add(IDParam);

                myReader = IDQuery.ExecuteReader();

                if (myReader.Read())
                {
                    this.recipeList = new List<Recipe>();
                    Recipe recipe = new Recipe((string)myReader["Name"]);
                    recipe.Tooltip = (string)myReader["Tooltip"];
                    recipe.ProfessionName = (string)myReader["ProfName"];
                    recipeList.Add(recipe);

                    while (myReader.Read())
                    {
                        Recipe recipeInner = new Recipe((string)myReader["Name"]);
                        recipeInner.Tooltip = (string)myReader["Tooltip"];
                        recipeInner.ProfessionName = (string)myReader["ProfName"];
                        recipeList.Add(recipeInner);
                    }
                }
                myReader.Close();
            }


            // Quests...

            if (this.DatabaseId != 0)
            {
                IDQuery = new SqlCommand("select a.* from Quest a, Char_Quest b where a.ID = b.QuestID and b.CharID = @CharID order by a.name", myConnection);
                IDParam = new SqlParameter();
                IDParam.ParameterName = "@CharID";
                IDParam.Value = this.DatabaseId;
                IDQuery.Parameters.Add(IDParam);

                myReader = IDQuery.ExecuteReader();

                if (myReader.Read())
                {
                    this.questList = new List<Quest>();
                    Quest quest = new Quest((string)myReader["Name"]);
                    quest.QuestLevel = (short)myReader["Quest_Level"];
                    quest.Tag = (string)myReader["Tag"];
                    quest.Zone = (string)myReader["Zone"];
                    questList.Add(quest);

                    while (myReader.Read())
                    {
                        Quest questInner = new Quest((string)myReader["Name"]);
                        questInner.QuestLevel = (short)myReader["Quest_Level"];
                        questInner.Tag = (string)myReader["Tag"];
                        questInner.Zone = (string)myReader["Zone"];
                        questList.Add(questInner);
                    }
                }
                myReader.Close();
            }

            // Finished all reading, so get rid of our connection to free up the database.
            myConnection.Dispose();
        }
        public void RemoveLinks(SqlConnection myConnection)
        {
            // Before updating/inserting a character, remove all their linked
            // quests, recipes, professions and reputations.
            string delString = @"delete from char_prof where charID = @charID";
            SqlCommand delProf = new SqlCommand(delString, myConnection);
            SqlParameter p = new SqlParameter("@CharID", this.databaseId);
            delProf.Parameters.Add(p);
            delProf.ExecuteNonQuery();
            delProf.Dispose();

            delString = @"delete from char_quest where charID = @charID";
            SqlCommand delQuest = new SqlCommand(delString, myConnection);
            p = new SqlParameter("@CharID", this.databaseId);
            delQuest.Parameters.Add(p);
            delQuest.ExecuteNonQuery();
            delQuest.Dispose();

            delString = @"delete from char_quest where charID = @charID";
            SqlCommand delRecipe = new SqlCommand(delString, myConnection);
            p = new SqlParameter("@CharID", this.databaseId);
            delRecipe.Parameters.Add(p);
            delRecipe.ExecuteNonQuery();
            delRecipe.Dispose();

            delString = @"delete from char_reputation where charID = @charID";
            SqlCommand delReputation = new SqlCommand(delString, myConnection);
            p = new SqlParameter("@CharID", this.databaseId);
            delReputation.Parameters.Add(p);
            delReputation.ExecuteNonQuery();
            delReputation.Dispose();
        }
        public void UpdateToDB()
        {
            // First create the connection, then see if the Character already exists in the Database.

            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            int DBID = GetDBID(myConnection);

            if (DBID > 0)
            {
                // We've found this character exists, so store their ID for later use

                this.DatabaseId = DBID;
                this.RemoveLinks(myConnection);

                // Now update the existing character.

                string updateString = @"update Character set Name = @Name, LogonName = @LogonName, Agility = @Agility, Armour = @Armour, CharClass = @CharClass, DodgePercent = @DodgePercent, Health = @Health, Intellect = @Intellect, Level = @Level, Mana = @Mana, Race = @Race, Sex = @Sex, Spirit = @Spirit, Strength = @Strength, TimePlayed = @TimePlayed, TimePlayedThisLevel = @TimePlayedThisLevel, Title = @Title where ID = @DatabaseID";
                SqlCommand myCommand = new SqlCommand(updateString, myConnection);
                SqlParameter[] myParms = new SqlParameter[18];
                myParms[0] = new SqlParameter("@Name", this.Name);
                myParms[1] = new SqlParameter("@LogonName", "Bart");
                myParms[2] = new SqlParameter("@Agility", this.Agility);
                myParms[3] = new SqlParameter("@Armour", this.Armour);
                myParms[4] = new SqlParameter("@CharClass", this.CharClass);
                myParms[5] = new SqlParameter("@DodgePercent", this.DodgePercent);
                myParms[6] = new SqlParameter("@Health", this.Health);
                myParms[7] = new SqlParameter("@Intellect", this.Intellect);
                myParms[8] = new SqlParameter("@Level", this.Level);
                myParms[9] = new SqlParameter("@Mana", this.Mana);
                myParms[10] = new SqlParameter("@Race", this.Race);
                myParms[11] = new SqlParameter("@Sex", this.Sex);
                myParms[12] = new SqlParameter("@Spirit", this.Spirit);
                myParms[13] = new SqlParameter("@Strength", this.Strength);
                myParms[14] = new SqlParameter("@TimePlayed", this.TimePlayed);
                myParms[15] = new SqlParameter("@TimePlayedThisLevel", this.TimePlayedThisLevel);
                myParms[16] = new SqlParameter("@Title", this.Title);
                myParms[17] = new SqlParameter("@DatabaseID", this.DatabaseId);
                myCommand.Parameters.AddRange(myParms);

                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
            }
            else
            {
                // Gotta insert a new character
                SqlDataAdapter adapter = new SqlDataAdapter();

                string insertString = @"insert into Character (Name, LogonName, Agility, Armour, CharClass, DodgePercent, Health, Intellect, Level, Mana, Race, Sex, Spirit, Strength, TimePlayed, TimePlayedThisLevel, Title) values (@Name, @LogonName, @Agility, @Armour, @CharClass, @DodgePercent, @Health, @Intellect, @Level, @Mana, @Race, @Sex, @Spirit, @Strength, @TimePlayed, @TimePlayedThisLevel, @Title)";
                SqlCommand myCommand = new SqlCommand(insertString, myConnection);
                SqlParameter[] myParms = new SqlParameter[17];
                myParms[0] = new SqlParameter("@Name", this.Name);
                myParms[1] = new SqlParameter("@LogonName", "Bart");
                myParms[2] = new SqlParameter("@Agility", this.Agility);
                myParms[3] = new SqlParameter("@Armour", this.Armour);
                myParms[4] = new SqlParameter("@CharClass", this.CharClass);
                myParms[5] = new SqlParameter("@DodgePercent", this.DodgePercent);
                myParms[6] = new SqlParameter("@Health", this.Health);
                myParms[7] = new SqlParameter("@Intellect", this.Intellect);
                myParms[8] = new SqlParameter("@Level", this.Level);
                myParms[9] = new SqlParameter("@Mana", this.Mana);
                myParms[10] = new SqlParameter("@Race", this.Race);
                myParms[11] = new SqlParameter("@Sex", this.Sex);
                myParms[12] = new SqlParameter("@Spirit", this.Spirit);
                myParms[13] = new SqlParameter("@Strength", this.Strength);
                myParms[14] = new SqlParameter("@TimePlayed", this.TimePlayed);
                myParms[15] = new SqlParameter("@TimePlayedThisLevel", this.TimePlayedThisLevel);
                myParms[16] = new SqlParameter("@Title", this.Title);
                myCommand.Parameters.AddRange(myParms);

                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

                // Now get the ID of the newly inserted Character.

                this.DatabaseId = GetDBID(myConnection);
            }

            // Now we have the character created, work through their Professions.

            if (this.professionList != null)
            {
                foreach (Profession currProf in this.professionList)
                {
                    currProf.InsertToDB(myConnection);
                    currProf.AddToChar(myConnection, this.DatabaseId);

                    // We now have the profession's database ID, so update its recipes with the ID.
                    foreach (Recipe currRecipe in this.RecipeList)
                    {
                        if (currRecipe.ProfessionName == this.Name)
                        {
                            currRecipe.ProfessionID = this.DatabaseId;
                        }
                    }
                }
            }

            // Next work through their recipes.

            if (this.recipeList != null)
            {
                foreach (Recipe currRecipe in this.RecipeList)
                {
                    currRecipe.InsertToDB(myConnection);
                    currRecipe.AddToChar(myConnection, this.DatabaseId);
                }
            }

            // Quests...

            if (this.questList != null)
            {
                foreach (Quest currQuest in this.QuestList)
                {
                    currQuest.InsertToDB(myConnection);
                    currQuest.AddToChar(myConnection, this.DatabaseId);
                }
            }
            // Reputations...

            if (this.reputationList != null)
            {
                foreach (Reputation currRep in this.ReputationList)
                {
                    currRep.AddToChar(myConnection, this.DatabaseId);
                }
            }

            // Finished all updating, so get rid of our connection to free up the database.
            myConnection.Dispose();
        }
        public List<Quest> getQuests()
        {
            return questList;
        }
        public List<Recipe> getRecipes()
        {
            return recipeList;
        }

    }
    public class WoWCharacterSummary : IComparable
    {
        private string charClass;
        private int databaseId;
        private int level;
        private string logonName;
        private string name;
        private string race;
        private string sex;
        private string title;
        private string item;

        public WoWCharacterSummary(string name)
        {
            this.name = name;
        }

        public string CharClass
        {
            get
            {
                if (charClass != null)
                {
                    return charClass;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                charClass = value;
            }
        }
        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        public string LogonName
        {
            get
            {
                if (logonName != null)
                {
                    return logonName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                logonName = value;
            }
        }
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }
        public string Race
        {
            get
            {
                if (race != null)
                {
                    return race;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                race = value;
            }
        }
        public string Sex
        {
            get
            {
                if (sex != null)
                {
                    return sex;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                sex = value;
            }
        }
        public string Title
        {
            get
            {
                if (title != null)
                {
                    return title;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                title = value;
            }
        }
        public string Item
        {
            get
            {
                if (item != null)
                {
                    return item;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                item = value;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is WoWCharacterSummary)
            {
                WoWCharacter otherChar = (WoWCharacter)obj;
                return Name.CompareTo(otherChar.Name);
            }
            throw new ArgumentException("Object is not a WoW Character");
        }
        public void ReadFromDB()
        {
            // First create the connection, then read the Character summary from the database.
            // This method expects the this.Name field to already be populated, else it won't find anything.

            //string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Documents and Settings\\Bart\\My Documents\\Visual Studio 2005\\WebSites\\SAMemberServices\\App_Data\\SACharacters.mdf';Integrated Security=True;User Instance=True";

            //string connectionString = "server=(local);uid=CS_Admin;pwd=orpheus1;Trusted_Connection=no;database=SouthernArmada_Chardata";
            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            SqlCommand IDQuery = new SqlCommand("select id, logonname, charclass, level, race, sex, title from Character where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();

            if (myReader.Read())
            {
                this.DatabaseId = (short)myReader["ID"];
                this.LogonName = (string)myReader["LogonName"];
                this.CharClass = (string)myReader["CharClass"];
                this.Level = (short)myReader["Level"];
                this.Race = (string)myReader["Race"];
                this.Sex = (string)myReader["Sex"];
                this.Title = (string)myReader["Title"];
            }
            else
            {
                // Cannot find anything in the database - so blank out the WoWCharacter.
                this.DatabaseId = 0;
            }
            myReader.Close();

            // Finished all reading, so get rid of our connection to free up the database.
            myConnection.Dispose();
        }
    }
    public class Profession
    {
        private string name;
        private int databaseId;

        public Profession(string name)
        {
            this.name = name;
        }

        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }

        public void AddToChar(SqlConnection myConnection, int CharID)
        {
            // If the Current Profession and passed in Char Id both exist together, don't do
            // anything.  If they don't exist, insert them into the Char_Prof table.

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand IDQuery = new SqlCommand("select * from Char_Prof where CharID = @CharID and ProfessionID = @ProfID", myConnection);
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@CharID", CharID);
            p[1] = new SqlParameter("@ProfID", this.DatabaseId);
            IDQuery.Parameters.AddRange(p);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            if (myReader.Read())
            {
                myReader.Close();
                return;
            }
            else
            {
                myReader.Close();
                string insertString = @"insert into Char_Prof (CharID, ProfessionID) values (@CharID, @ProfID)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);

                SqlParameter[] pi = new SqlParameter[2];
                pi[0] = new SqlParameter("@CharID", CharID);
                pi[1] = new SqlParameter("@ProfID", this.DatabaseId);
                CharInsert.Parameters.AddRange(pi);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();
            }
        }
        public int GetDBID(SqlConnection myConnection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand IDQuery = new SqlCommand("select ID from Profession where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            int DBID;

            if (myReader.Read())
            {
                DBID = (short)myReader["ID"];
            }
            else
            {
                DBID = 0;
            }
            myReader.Close();
            return DBID;
        }
        public void InsertToDB(SqlConnection myConnection)
        {
            int DBID = GetDBID(myConnection);

            if (DBID > 0)
            {
                // We've found this profession exists, so store their ID for later use

                this.DatabaseId = DBID;
            }
            else
            {
                // Gotta insert a new profession

                string insertString = @"insert into Profession (Name) values (@Name)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);
                SqlParameter InsertName = new SqlParameter();
                InsertName.ParameterName = "@Name";
                InsertName.Value = this.Name;
                CharInsert.Parameters.Add(InsertName);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();

                // Now get the ID of the newly inserted Profession.

                this.DatabaseId = GetDBID(myConnection);
            }
        }
    }
    public class Quest
    {
        private int databaseId;
        private short questLevel;
        private string name;
        private string tag;
        private string zone;

        public Quest()
        {
        }
        public Quest(string name)
        {
            this.name = name;
        }

        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public short QuestLevel
        {
            get
            {
                return questLevel;
            }
            set
            {
                questLevel = value;
            }
        }
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }
        public string Tag
        {
            get
            {
                if (tag != null)
                {
                    return tag;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                tag = value;
            }
        }
        public string Zone
        {
            get
            {
                if (zone != null)
                {
                    return zone;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                zone = value;
            }
        }

        public void AddToChar(SqlConnection myConnection, int CharID)
        {
            // If the Current Quest and passed in Char Id both exist together, don't do
            // anything.  If they don't exist, insert them into the Char_Quest table.

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand IDQuery = new SqlCommand("select * from Char_Quest where CharID = @CharID and QuestID = @QuestID", myConnection);
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@CharID", CharID);
            p[1] = new SqlParameter("@QuestID", this.DatabaseId);
            IDQuery.Parameters.AddRange(p);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            if (myReader.Read())
            {
                myReader.Close();
                return;
            }
            else
            {
                myReader.Close();
                string insertString = @"insert into Char_Quest (CharID, QuestID) values (@CharID, @QuestID)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);

                SqlParameter[] pi = new SqlParameter[2];
                pi[0] = new SqlParameter("@CharID", CharID);
                pi[1] = new SqlParameter("@QuestID", this.DatabaseId);
                CharInsert.Parameters.AddRange(pi);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();
            }
        }
        public int GetDBID(SqlConnection myConnection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand IDQuery = new SqlCommand("select ID from Quest where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            int DBID;

            if (myReader.Read())
            {
                DBID = (short)myReader["ID"];
            }
            else
            {
                DBID = 0;
            }
            myReader.Close();
            return DBID;
        }
        public void InsertToDB(SqlConnection myConnection)
        {
            int DBID = GetDBID(myConnection);

            if (DBID > 0)
            {
                // We've found this Quest exists, so store their ID for later use

                this.DatabaseId = DBID;
            }
            else
            {
                // Gotta insert a new Quest

                string insertString = @"insert into Quest (Name, Quest_Level, Tag, Zone, Text) values (@Name, @QuestLevel, @Tag, @Zone, @Text)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);
                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("@Name", this.Name);
                p[1] = new SqlParameter("@QuestLevel", this.QuestLevel);
                if (this.Tag == null)
                {
                    p[2] = new SqlParameter("@Tag", "");
                }
                else
                {
                    p[2] = new SqlParameter("@Tag", this.Tag);
                }
                p[3] = new SqlParameter("@Zone", this.Zone);
                p[4] = new SqlParameter("@Text", "text goes here");
                CharInsert.Parameters.AddRange(p);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();

                // Now get the ID of the newly inserted quest.

                this.DatabaseId = GetDBID(myConnection);
            }
        }
    }
    public class Recipe
    {
        private string name;
        private int databaseId;
        private int professionID;
        private string professionName;
        private string tooltip;
        private string texture;

        public Recipe(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }
        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public int ProfessionID
        {
            get
            {
                return professionID;
            }
            set
            {
                professionID = value;
            }
        }
        public string ProfessionName
        {
            get
            {
                if (professionName != null)
                {
                    return professionName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                professionName = value;
            }
        }
        public string Tooltip
        {
            get
            {
                if (tooltip != null)
                {
                    return tooltip;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                tooltip = value;
            }
        }
        public string Texture
        {
            get
            {
                if (texture != null)
                {
                    return texture;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                texture = value;
            }
        }

        public void AddToChar(SqlConnection myConnection, int CharID)
        {
            // If the Current Recipe and passed in Char Id both exist together, don't do
            // anything.  If they don't exist, insert them into the Char_Prof table.

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand IDQuery = new SqlCommand("select * from Char_Recipe where CharID = @CharID and RecipeID = @RecipeID", myConnection);
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@CharID", CharID);
            p[1] = new SqlParameter("@RecipeID", this.DatabaseId);
            IDQuery.Parameters.AddRange(p);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            if (myReader.Read())
            {
                myReader.Close();
                return;
            }
            else
            {
                myReader.Close();
                string insertString = @"insert into Char_Recipe (CharID, RecipeID) values (@CharID, @RecipeID)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);

                SqlParameter[] pi = new SqlParameter[2];
                pi[0] = new SqlParameter("@CharID", CharID);
                pi[1] = new SqlParameter("@RecipeID", this.DatabaseId);
                CharInsert.Parameters.AddRange(pi);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();
            }
        }
        public int GetDBID(SqlConnection myConnection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand IDQuery = new SqlCommand("select ID from Recipe where name = @Name", myConnection);
            SqlParameter IDParam = new SqlParameter();
            IDParam.ParameterName = "@Name";
            IDParam.Value = this.Name;
            IDQuery.Parameters.Add(IDParam);

            SqlDataReader myReader = IDQuery.ExecuteReader();
            int DBID;

            if (myReader.Read())
            {
                DBID = (short)myReader["ID"];
            }
            else
            {
                DBID = 0;
            }
            myReader.Close();
            return DBID;
        }
        public void InsertToDB(SqlConnection myConnection)
        {
            int DBID = GetDBID(myConnection);

            if (DBID > 0)
            {
                // We've found this recipe exists, so store their ID for later use

                this.DatabaseId = DBID;
            }
            else
            {
                // Gotta insert a new recipe

                string insertString = @"insert into Recipe (Name, Tooltip, ProfName, ProfID) values (@Name, @Tooltip, @ProfName, @ProfID)";
                SqlCommand CharInsert = new SqlCommand(insertString, myConnection);
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@Name", this.Name);
                p[1] = new SqlParameter("@Tooltip", this.Tooltip);
                p[2] = new SqlParameter("@ProfName", this.ProfessionName);
                p[3] = new SqlParameter("@ProfID", this.ProfessionID);
                CharInsert.Parameters.AddRange(p);

                CharInsert.ExecuteNonQuery();
                CharInsert.Dispose();

                // Now get the ID of the newly inserted recipe.

                this.DatabaseId = GetDBID(myConnection);
            }
        }
    }
    public class Reputation
    {
        private string name;
        private int databaseId;
        private string group;
        private string standing;
        private int standingNum;
        private string val;
        private bool atWar;
        
        public Reputation(string name)
        {
            this.name = name;
        }

        public int DatabaseId
        {
            get
            {
                return databaseId;
            }
            set
            {
                databaseId = value;
            }
        }
        public string Group
        {
            get
            {
                if (group != null)
                {
                    return group;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                group = value;
            }
        }
        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                name = value;
            }
        }
        public string Standing
        {
            get
            {
                if (standing != null)
                {
                    return standing;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                standing = value;
            }
        }
        public int StandingNum
        {
            get
            {
                return standingNum;
            }
            set
            {
                standingNum = value;
            }
        }
        public string Value
        {
            get
            {
                if (val != null)
                {
                    return val;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                val = value;
            }
        }
        public bool AtWar
        {
            get
            {
                return atWar;
            }
            set
            {
                atWar = value;
            }
        }

        public void AddToChar(SqlConnection myConnection, int CharID)
        {
            // Update char_reputation with the collection of reputations that the character has.

            string insertString = "insert into Char_Reputation (CharID, Name, [Group], Standing, StandingNum, Value, AtWar) values (@CharID, @Name, @Group, @Standing, @StandingNum, @Value, @AtWar)";
            SqlCommand CharInsert = new SqlCommand(insertString, myConnection);

            SqlParameter[] pi = new SqlParameter[7];
            pi[0] = new SqlParameter("@CharID", CharID);
            pi[1] = new SqlParameter("@Name", this.Name);
            pi[2] = new SqlParameter("@Group", this.Group);
            pi[3] = new SqlParameter("@Standing", this.Standing);
            pi[4] = new SqlParameter("@StandingNum", this.StandingNum);
            pi[5] = new SqlParameter("@Value", this.Value);
            pi[6] = new SqlParameter("@AtWar", this.AtWar);
    
            CharInsert.Parameters.AddRange(pi);
            CharInsert.ExecuteNonQuery();
            CharInsert.Dispose();
        }
    }
    public class WoWCharacterSummaryList
    {
        private List<WoWCharacterSummary> characterList;
        public List<WoWCharacterSummary> getAll()
        {
            // This will get all characters in the database
            // First connect and get the list of names

            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            SqlCommand IDQuery = new SqlCommand("select Name from Character order by Name", myConnection);
            SqlParameter IDParam = new SqlParameter();

            SqlDataReader myReader = IDQuery.ExecuteReader();

            this.characterList = new List<WoWCharacterSummary>();

            // Work through the names and create WoWCharacterSummaries for each.
            while (myReader.Read())
            {
                WoWCharacterSummary currChar = new WoWCharacterSummary((string)myReader["Name"]);
                currChar.ReadFromDB();
                characterList.Add(currChar);
            }
            myReader.Close();
            return characterList;
        }
        public List<WoWCharacterSummary> search(string searchString)
        {
            // This will search for everything and stick it into a big list

            this.characterList = new List<WoWCharacterSummary>();
            characterList = this.searchQuests(searchString);
            
            return characterList;
        }

        public List<WoWCharacterSummary> searchQuests(string searchString)
        {
            // This will search for characters that have these quests

            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // WARNING!  This can be open for a sql injection attack - have to parameterise it somehow.

            string queryString = "select character.Name as CharName, quest.Name as QuestName from Character, char_quest, quest where quest.name like '%" + searchString + "%' and character.id = charid and quest.id = questid order by character.Name";
            SqlCommand IDQuery = new SqlCommand(queryString, myConnection);

            SqlDataReader myReader = IDQuery.ExecuteReader();

            this.characterList = new List<WoWCharacterSummary>();

            // Work through the names and create WoWCharacterSummary for each.
            while (myReader.Read())
            {
                WoWCharacterSummary currChar = new WoWCharacterSummary((string)myReader["CharName"]);
                currChar.ReadFromDB();
                currChar.Item = (string)myReader["QuestName"];
                characterList.Add(currChar);
            }
            myReader.Close();
            myConnection.Close();
            return characterList;
        }

        public List<WoWCharacterSummary> searchRecipes(string searchString)
        {
            // This will search for characters that have these recipes

            string connectionString = "Data Source=sql2005.aspnix.com;Initial Catalog=samembers;User ID=bart-member;Database=samembers;Password=coffee01";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            // WARNING!  This can be open for a sql injection attack - have to parameterise it somehow.

            string queryString = "select character.Name as CharName, recipe.name as RecipeName from Character, char_recipe, recipe where recipe.name like '%" + searchString + "%' and character.id = charid and recipe.id = recipeid order by character.Name";
            SqlCommand IDQuery = new SqlCommand(queryString, myConnection);

            SqlDataReader myReader = IDQuery.ExecuteReader();

            this.characterList = new List<WoWCharacterSummary>();

            // Work through the names and create WoWCharacters for each.
            while (myReader.Read())
            {
                WoWCharacterSummary currChar = new WoWCharacterSummary((string)myReader["CharName"]);
                currChar.ReadFromDB();
                currChar.Item = (string)myReader["RecipeName"];
                characterList.Add(currChar);
            }
            myReader.Close();
            myConnection.Close();
            return characterList;
        }
    }
}