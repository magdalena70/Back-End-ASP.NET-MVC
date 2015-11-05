namespace SportSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SportSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SportSystemDbContext context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            if (!context.Teams.Any())
            {
                this.SeedTeams(context);
            }

            if (!context.Matches.Any())
            {
                this.SeedMatches(context);
            }

            if (!context.Players.Any())
            {
                this.SeedPlayers(context);
            }

            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }

            if (!context.Bets.Any())
            {
                SeedBets(context);
            }

            if (!context.Votes.Any())
            {
                SeedVotes(context);
            }
            
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
        }

        private void SeedVotes(SportSystemDbContext context)
        {
            var listVotes = new List<string>()
            {
                @"Bayern Munich | tanya@gmail.com",
                @"Real Madrid | tanya@gmail.com",
                @"Bayern Munich | alex@usa.net"
            };

            foreach (var listVote in listVotes)
            {
                var cmd = listVote.Split('|');
                var teamName = cmd[0].Trim();
                var userName = cmd[1].Trim();
                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                var user = context.Users.FirstOrDefault(u => u.UserName == userName);
                var voteDb = new Vote()
                {
                    Team = team,
                    UserId = user.Id
                };

                context.Votes.Add(voteDb);
            }

            context.SaveChanges();
        }

        private void SeedBets(SportSystemDbContext context)
        {
            var listBets = new List<string>()
            {
                @"Chelsea | FC Barcelona | 30.00 |0.00 | alex@usa.net",
                @"Chelsea | FC Barcelona | 0.00| 50.00 | alex@usa.net",
                @"Chelsea | FC Barcelona | 0.00	| 120.00 | alex@usa.net",
                @"FC Barcelona | Manchester City | 120.00 | 0.00 | alex@usa.net",
                @"Bayern Munich | Manchester United F.C. | 500.00 | 0.00 | alex@usa.net",
                @"Bayern Munich | Manchester United F.C. | 50.00 | 0.00	| tanya@gmail.com",
                @"Bayern Munich | Manchester United F.C. | 0.00	| 20.00 | tanya@gmail.com",
                @"Chelsea | FC Barcelona | 0.00	| 220.00 | tanya@gmail.com"
            };

            foreach (var listBet in listBets)
            {
                var cmd = listBet.Split('|');
                var homeTeam = cmd[0].Trim();
                var awayTeam = cmd[1].Trim();
                var homeNum = decimal.Parse(cmd[2].Trim());
                var awayNum = decimal.Parse(cmd[3].Trim());
                var userName = cmd[4].Trim();
                var user = context.Users.FirstOrDefault(u => u.UserName == userName);
                var match = context.Matches
                    .FirstOrDefault(m => m.HomeTeam.Name == homeTeam && m.AwayTeam.Name == awayTeam);
                var betDb = new Bet()
                {
                    Match = match,
                    BettingUserId = user.Id,
                    HomeBet = homeNum,
                    AwayBet = awayNum
                };

                context.Bets.Add(betDb);
            }

            context.SaveChanges();
        }

        private void SeedComments(SportSystemDbContext context)
        {
            var alex = context.Users.FirstOrDefault(u => u.UserName == "alex@usa.net");
            var tanya = context.Users.FirstOrDefault(u => u.UserName == "tanya@gmail.com");
            var match = context.Matches
                    .FirstOrDefault(m => m.HomeTeam.Name == "Bayern Munich" && m.AwayTeam.Name == "Manchester United F.C.");
            var commentBarsa1 = new Comment()
            {
                Match = match,
                CreationTime = DateTime.Now.AddDays(-6),
                AuthorId = alex.Id,
                Text = "The best match this summer!"
            };

            context.Comments.Add(commentBarsa1);

            var commentBarsa2 = new Comment()
            {
                Match = match,
                CreationTime = DateTime.Now.AddDays(-2),
                AuthorId = tanya.Id,
                Text = "The good football this evening."
            };

            context.Comments.Add(commentBarsa2);
            //---
            var match1 = context.Matches
                    .FirstOrDefault(m => m.HomeTeam.Name == "FC Barcelona" && m.AwayTeam.Name == "Manchester City");
            var commentBarca3 = new Comment()
            {
                Match = match1,
                CreationTime = DateTime.Now.AddDays(-9),
                AuthorId = tanya.Id,
                Text = "Barca!"
            };

            context.Comments.Add(commentBarca3);
            //---
            var match2 = context.Matches
                    .FirstOrDefault(m => m.HomeTeam.Name == "Real Madrid" && m.AwayTeam.Name == "Manchester City");
            var commentReal = new Comment()
            {
                Match = match2,
                CreationTime = DateTime.Now.AddDays(-1),
                AuthorId = alex.Id,
                Text = "Real forever!"
            };

            var commentReal1 = new Comment()
            {
                Match = match2,
                CreationTime = DateTime.Now.AddDays(-1),
                AuthorId = alex.Id,
                Text = "Real, real, real"
            };

            var commentReal2 = new Comment()
            {
                Match = match2,
                CreationTime = DateTime.Now.AddDays(-1),
                AuthorId = alex.Id,
                Text = "Real again :)"
            };

            context.Comments.Add(commentReal);
            context.Comments.Add(commentReal1);
            context.Comments.Add(commentReal2);
            //---
            var match3 = context.Matches
                    .FirstOrDefault(m => m.HomeTeam.Name == "Chelsea" && m.AwayTeam.Name == "Real Madrid");
            var commentChelsa = new Comment()
            {
                Match = match3,
                CreationTime = DateTime.Now.AddDays(-2),
                AuthorId = tanya.Id,
                Text = "Chelsea champion!"
            };

            context.Comments.Add(commentChelsa);

            context.SaveChanges();
        }

        private void SeedPlayers(SportSystemDbContext context)
        {
            var listPlayers = new List<string>()
            {
                @"Alexis Sanchez | 1982 - 01 - 03 | 1.65 | FC Barcelona",
                @"Arjen Robben  |  1982 - 01 - 03 | 1.65 | Real Madrid",
                @"Franck Ribery | 1982 - 01 - 03 | 1.65 | Manchester United F.C.",
                @"Wayne Rooney | 1982 - 01 - 03 | 1.65 | Manchester United F.C.",
                @"Lionel Messi | 1982 - 01 - 13 | 1.65 | (NULL)",
                @"Theo Walcott | 1983 - 02 - 17 | 1.75 | (NULL)",
                @"Cristiano Ronaldo | 1984 - 03 - 16 | 1.85|(NULL)",
                @"Aaron Lennon | 1985 - 04 - 15 | 1.95 | (NULL)",
                @" Gareth Bale | 1986 - 05 - 14 | 1.90 | (NULL)",
                @"Antonio Valencia | 1987 - 05 - 23 | 1.82 | (NULL)",
                @"Robin van Persie | 1988 - 06 - 13 | 1.84 | (NULL)",
                @"  Ronaldinho | 1989 - 07 - 30 | 1.87 | (NULL)",
            };

            foreach (var player in listPlayers)
            {
                var comp = player.Split('|');
                var name = comp[0].Trim();
                var date = comp[1].Trim();
                var height = comp[2].Trim();
                var teamName = comp[3].Trim() == "(NULL)" ? null : comp[3].Trim();

                var playerDb = new Player()
                {
                    Name = name,
                    DateOfBirth = DateTime.Parse(date),
                    Height = double.Parse(height),
                    Team = teamName == null ? null : context.Teams.FirstOrDefault(t => t.Name == teamName)
                };

                context.Players.Add(playerDb);
            }

            context.SaveChanges();
        }

        private void SeedMatches(SportSystemDbContext context)
        {
            var listMatches = new List<string>()
            {
                @"Real Madrid | Manchester United F.C. | 2015 - Jun - 13" ,
                @"Bayern Munich |  Manchester United F.C. | 2015 - Jun - 14" ,
                @"FC Barcelona | Manchester City | 2015 - Jun - 15" ,
                @"Chelsea | FC Barcelona | 2015 - Jun - 16" ,
                @"Real Madrid | Manchester City | 2015 - Jun - 17" ,
                @"Manchester United F.C.| Chelsea | 2015 - Jun - 18" ,
                @"Arsenal | Bayern Munich  | 2015 - Jun - 12" ,
                @"Chelsea | Real Madrid | 2015 - Jun - 11" ,
                @"Chelsea | Manchester City | 2015 - Jun - 10" ,
                @"Chelsea | Arsenal | 2015 - Jun - 19" ,
                @"Arsenal | FC Barcelona | 2015 - Jun - 20"
            };

            foreach (var match in listMatches)
            {
                var comp = match.Split('|');
                var tn1 = comp[0].Trim();
                var tn2 = comp[1].Trim();
                var team1 = context.Teams.FirstOrDefault(t => t.Name == tn1);
                var team2 = context.Teams.FirstOrDefault(t => t.Name == tn2);
                var date = comp[2].Trim();

                var matchDb = new Match()
                {
                    HomeTeam = team1,
                    AwayTeam = team2,
                    DateAndTime = DateTime.Parse(date),
                };

                context.Matches.Add(matchDb);

            }
        }

        private void SeedTeams(SportSystemDbContext context)
        {
            var listTeams = new List<string>()
            {
                @"Manchester United F.C. | http://www.manutd.com | 1-Jun-1878 |The Red Devils",
                @"Real Madrid | http://www.realmadrid.com |	6-Mar-1902 | The Whites",
                @"FC Barcelona  |  http://www.fcbarcelona.com |	12-Nov-1899 | Barca",
                @"Bayern Munich  | http://www.fcbayern.de | 13-Feb-1900 | The Bavarians",
                @"Manchester City | http://www.mcfc.com | 1-May-1880 | The Citizens",
                @"Chelsea | https://www.chelseafc.com | 10-Mar-1905 | The Pensioners",
                @"Arsenal | http://www.arsenal.com | 1-Sep-1886	| The Gunners"
            };

            foreach (var team in listTeams)
            {
                var comp = team.Split('|');
                var name = comp[0].Trim();
                var web = comp[1].Trim();
                var date = comp[2].Trim();
                var nick = comp[3].Trim();

                var teamDb = new Team()
                {
                    Name = name,
                    DateFounded = DateTime.Parse(date),
                    Nickname = nick,
                    Website = web
                };

                context.Teams.Add(teamDb);
            }

            context.SaveChanges();
        }

        private void SeedUsers(SportSystemDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var userToInsert = new User { UserName = "alex@usa.net", Email = "alex@usa.net" };
            userManager.Create(userToInsert, "12qw!@QW");
            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);
            userToInsert = new User { UserName = "tanya@gmail.com", Email = "tanya@gmail.com" };
            userManager.Create(userToInsert, "Password@123");
        }
    }
}
