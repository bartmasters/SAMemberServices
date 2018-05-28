using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WoWDetails;

namespace ParseCharacters
{
    public class CharacterParser
    {
        private static int LevelCount = 0;
        private static String InputLine;
        private static bool EndFile;

        public List<WoWCharacter> DoParse(Stream InputStream)
        {
            List<WoWCharacter> proudmooreChars = new List<WoWCharacter>();

            using (StreamReader sr = new StreamReader(InputStream))
            {
                InputLine = GetNextLine(sr);
                while (!EndFile)
                {
                    Regex rg = new Regex("myProfile");
                    Match m = rg.Match(InputLine);

                    if (m.Success)
                    {
                        FindProudmoore(sr, proudmooreChars);
                    }
                    InputLine = GetNextLine(sr);
                }
            }

            return proudmooreChars;
        }
        static string GetNextLine(StreamReader sr)
        {
           InputLine = sr.ReadLine();
           if (InputLine == null)
           {
               EndFile = true;
           }
           else
           {
               EndFile = false;
               Regex rgUp = new Regex("{");
               Match mtchUp = rgUp.Match(InputLine);

               Regex rgDown = new Regex("}");
               Match mtchDown = rgDown.Match(InputLine);

               if (mtchUp.Success)
               {
                   LevelCount++;
               }
               if (mtchDown.Success)
               {
                   LevelCount--;
               }
           }
           return InputLine;
        }
        static void FindProudmoore(StreamReader sr, List<WoWCharacter> proudmooreChars)
        {
            while (LevelCount >= 1)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("Proudmoore");
                Match m = rg.Match(InputLine);

                if ((LevelCount == 2) && (m.Success))
                {
                    ProcessProudmooreCharacters(sr, proudmooreChars);
                }
            }
        }
        static void ProcessProudmooreCharacters(StreamReader sr, List<WoWCharacter> proudmooreChars)
        {
            while (LevelCount >= 2)
            {
                InputLine = GetNextLine(sr);
                if (LevelCount == 3)
                {
                    Regex rg = new Regex("(\\w+)");
                    Match m = rg.Match(InputLine);

                    WoWCharacter currChar = new WoWCharacter(m.Value);
                    currChar = ProcessChar(sr, currChar);
                    proudmooreChars.Add(currChar);
                }
            }
        }
        static WoWCharacter ProcessChar(StreamReader sr, WoWCharacter currChar)
        {
            while (LevelCount >= 3)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("Professions");
                Match mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 4))
                {
                    List<Profession> professions = new List<Profession>();
                    List<Recipe> recipes = new List<Recipe>();
                    currChar.RecipeList = recipes;
                    professions = GetProfessions(sr, professions, currChar);
                    currChar.ProfessionList = professions;
                }

                rg = new Regex("Quests");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 4))
                {
                    List<Quest> quests = new List<Quest>();
                    quests = GetQuests(sr, quests);
                    currChar.QuestList = quests;
                }

                rg = new Regex("Guild");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 4))
                {
                    currChar = GetGuild(sr, currChar);
                }

                rg = new Regex("Reputation");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 4))
                {
                    List<Reputation> reputations = new List<Reputation>();
                    reputations = GetReputations(sr, reputations);
                    currChar.ReputationList = reputations;
                }

                rg = new Regex("Class\"] = \"(.+)\",$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.CharClass = mg.Groups[1].Value;
                }

                rg = new Regex("TimeLevelPlayed\"] = (\\d+),$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.TimePlayedThisLevel = Convert.ToInt32(mg.Groups[1].Value);
                }

                rg = new Regex("TimePlayed\"] = (\\d+),$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.TimePlayed = Convert.ToInt32(mg.Groups[1].Value);
                }

                rg = new Regex("Level\"] = (\\d+),$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.Level = Convert.ToInt32(mg.Groups[1].Value);
                }

                rg = new Regex("Race\"] = \"(.+)\",$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.Race = mg.Groups[1].Value;
                }

                rg = new Regex("Sex\"] = \"(.+)\",$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.Sex = mg.Groups[1].Value;
                }

                rg = new Regex("DodgePercent\"] = \"(.+)\",$");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 3))
                {
                    currChar.DodgePercent = Convert.ToDouble(mg.Groups[1].Value);
                }

                rg = new Regex("Stats");
                mg = rg.Match(InputLine);
                if ((mg.Success) && (LevelCount == 4))
                {
                    currChar = ProcessStats(sr, currChar);
                }
            }
            return currChar;
        }
        
        static List<Profession> GetProfessions(StreamReader sr, List<Profession> professions, WoWCharacter currChar)
        {
            while (LevelCount >= 4)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 5))
                {
                    Regex rg2 = new Regex("([A-Za-z ]+)");
                    Match m2 = rg2.Match(InputLine);
                    Profession profession = new Profession(m2.Value);
                    profession = ProcessProfession(sr, profession, currChar);
                    professions.Add(profession);
                }
            }
            return professions;
        }
        static Profession ProcessProfession(StreamReader sr, Profession profession, WoWCharacter currChar)
        {
            List<Recipe> recipes = new List<Recipe>();
            List<Recipe> recipeCategory = new List<Recipe>();
            while (LevelCount >= 5)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 6))
                {
                    recipes = ProcessProfessionCategory(sr, profession);
                    recipeCategory.AddRange(recipes);
                }
            }

            currChar.RecipeList.AddRange(recipeCategory);
            return profession;
        }
        static List<Recipe> ProcessProfessionCategory(StreamReader sr, Profession profession)
        {
            List<Recipe> tempRecipes = new List<Recipe>();
            while (LevelCount >= 6)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 7))
                {
                    Regex rg2 = new Regex("([A-Za-z- ]+)");
                    Match m2 = rg2.Match(InputLine);
                    Recipe recipe = new Recipe(m2.Value);
                    recipe = ProcessRecipe(sr, recipe, profession);
                    tempRecipes.Add(recipe);
                }
            }
            return tempRecipes;
        }
        static Recipe ProcessRecipe(StreamReader sr, Recipe recipe, Profession profession)
        {
            while (LevelCount >= 7)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("(\\w+)\"] = (.+),$");
                Match m = rg.Match(InputLine);

                String key = m.Groups[1].Value;
                String value = m.Groups[2].Value;

                switch (key)
                {
                   /* case "Difficulty":
                        recipe.Difficulty = Convert.ToInt32(value);
                        break;*/
                    case "Tooltip":
                        value = Regex.Replace(value, "\"", "");
                        recipe.Tooltip = value;
                        break;
                    case "Texture":
                        recipe.Texture = value;
                        break;
                }

                recipe.ProfessionName = profession.Name;
            }
            return recipe;
        }

        static List<Quest> GetQuests(StreamReader sr, List<Quest> quests)
        {
            while (LevelCount >= 4)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 5))
                {
                    Regex rg2 = new Regex("([A-Za-z ]+)");
                    Match m2 = rg2.Match(InputLine);
                    string groupZone = m2.Value.ToString();

                    quests = ProcessQuest(sr, quests, groupZone);
                }
            }
            return quests;
        }
        static List<Quest> ProcessQuest(StreamReader sr, List<Quest> quests, String groupZone)
        {
            while (LevelCount >= 5)
            {
                Quest quest = new Quest();
                quest.Zone = groupZone;

                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 6))
                {
                    quest = ProcessQuestDetails(sr, quest);
                    quests.Add(quest);
                }
            }

            return quests;
        }
        static Quest ProcessQuestDetails(StreamReader sr, Quest quest)
        {
            while (LevelCount >= 6)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("(\\w+)\"] = (.+),$");
                Match m = rg.Match(InputLine);

                String key = m.Groups[1].Value;
                String value = m.Groups[2].Value;

                switch (key)
                {
                    case "Level":
                        quest.QuestLevel = Convert.ToInt16(value);
                        break;
                    case "Title":
                        value = Regex.Replace(value, "\"", "");
                        value = Regex.Replace(value, "\\[\\d+.\\] ", "");
                        quest.Name = value;
                        break;
                    case "Tag":
                        value = Regex.Replace(value, "\"", ""); 
                        quest.Tag = value;
                        break;
                }
            }
            return quest;
        }
        
        static WoWCharacter GetGuild(StreamReader sr, WoWCharacter currChar)
        {
            while (LevelCount >= 4)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("(\\w+)\"] = (.+),$");
                Match m = rg.Match(InputLine);

                String key = m.Groups[1].Value;
                String value = m.Groups[2].Value;

                switch (key)
                {
                    case "Title":
                        value = Regex.Replace(value, "\"", "");
                        currChar.Title = value;
                        break;
                }
            }
            return currChar;
        }

        static WoWCharacter ProcessStats(StreamReader sr, WoWCharacter currChar)
        {
            while (LevelCount >= 4)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("(\\w+)\"] = (.+),$");
                Match m = rg.Match(InputLine);

                String key = m.Groups[1].Value;
                String value = m.Groups[2].Value;
                rg = new Regex("^\"(\\d+):");
                m = rg.Match(value);

                switch (key)
                {
                    case "Intellect":
                        currChar.Intellect = Convert.ToInt32(m.Groups[1].Value);
                        break;
                    case "Agility":
                        currChar.Agility = Convert.ToInt32(m.Groups[1].Value);
                        break;
                    case "Defense":
                        currChar.Defence = Convert.ToInt32(m.Groups[1].Value);
                        break;
                    case "Stamina":
                        currChar.Stamina = Convert.ToInt32(m.Groups[1].Value);
                        break;
                    case "Strength":
                        currChar.Strength = Convert.ToInt32(m.Groups[1].Value);
                        break;
                    case "Spirit":
                        currChar.Spirit = Convert.ToInt32(m.Groups[1].Value);
                        break;
                }
            }
            return currChar;
        }

        static List<Reputation> GetReputations(StreamReader sr, List<Reputation> reputations)
        {
            while (LevelCount >= 4)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 5))
                {
                    Regex rg2 = new Regex("([A-Za-z ]+)");
                    Match m2 = rg2.Match(InputLine);
                    string group = m2.Value.ToString();

                    reputations = ProcessReputation(sr, reputations, group);
                }
            }
            return reputations;
        }
        static List<Reputation> ProcessReputation(StreamReader sr, List<Reputation> reputations, String group)
        {
            while (LevelCount >= 5)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("\\{");
                Match m = rg.Match(InputLine);

                if ((m.Success) && (LevelCount == 6))
                {
                    Regex rg2 = new Regex("([A-Za-z- ]+)");
                    Match m2 = rg2.Match(InputLine);
                    Reputation reputation = new Reputation(m2.Value);
                    reputation.Group = group;

                    reputation = ProcessReputationDetails(sr, reputation);
                    reputations.Add(reputation);
                }
            }

            return reputations;
        }
        static Reputation ProcessReputationDetails(StreamReader sr, Reputation reputation)
        {
            while (LevelCount >= 6)
            {
                InputLine = GetNextLine(sr);
                Regex rg = new Regex("(\\w+)\"] = (.+),$");
                Match m = rg.Match(InputLine);

                String key = m.Groups[1].Value;
                String value = m.Groups[2].Value;

                switch (key)
                {
                    case "Value":
                        value = Regex.Replace(value, "\"", ""); 
                        reputation.Value = value;
                        break;
                    case "Standing":
                        value = Regex.Replace(value, "\"", "");
                        reputation.Standing = value;

                        switch (value)
                        {
                            case "Hated":
                                reputation.StandingNum = 10;
                                break;
                            case "Hostile":
                                reputation.StandingNum = 20;
                                break;
                            case "Unfriendly":
                                reputation.StandingNum = 30;
                                break;
                            case "Neutral":
                                reputation.StandingNum = 40;
                                break;
                            case "Friendly":
                                reputation.StandingNum = 50;
                                break;
                            case "Honored":
                                reputation.StandingNum = 60;
                                break;
                            case "Revered":
                                reputation.StandingNum = 70;
                                break;
                            case "Exalted":
                                reputation.StandingNum = 80;
                                break;
                        }
                        break;
                    case "AtWar":
                        if (Convert.ToInt32(value) == 0)
                        {
                            reputation.AtWar = false;
                        }
                        else
                        {
                            reputation.AtWar = true;
                        }
                        break;

                }
            }
            return reputation;
        }
    }
}
