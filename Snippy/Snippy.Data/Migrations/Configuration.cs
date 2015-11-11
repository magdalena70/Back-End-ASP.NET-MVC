namespace Snippy.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Snippy.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SnippyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SnippyDbContext context)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            if (!context.Users.Any())
            {
                this.SeedRoles(context);
                this.SeedUsers(context);
            }

            if (!context.ProgrammingLanguages.Any())
            {
                this.SeedLanguages(context);
            }

            if (!context.Labels.Any())
            {
                this.SeedLabels(context);
            } 

            if (!context.Snippets.Any())
            {
                this.SeedSnippets(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }          
        }

        private void SeedRoles(SnippyDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
        }

        private void SeedUsers(SnippyDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var userToInsert = new User { UserName = "someUser", Email = "someUser@example.com" };
            userManager.Create(userToInsert, "someUserPass123");

            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);
            userToInsert = new User { UserName = "newUser", Email = "new_user@gmail.com" };
            userManager.Create(userToInsert, "userPass123");

            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);
            var userToRole = new User { UserName = "admin", Email = "admin@snippy.softuni.com" };
            userManager.Create(userToRole, "adminPass123");
            userManager.AddToRole(userToRole.Id, "Admin");
        }

        private void SeedLanguages(SnippyDbContext context)
        {
            var listLanguage = new List<string>()
            {
                @"C#",
                @"JavaScript",
                @"Python", 
                @"CSS",
                @"SQL", 
                @"PHP"
            };

            foreach (var lang in listLanguage)
            {
                var name = lang.Trim();
                var langDb = new ProgrammingLanguage()
                {
                    Name = name//
                };

                context.ProgrammingLanguages.Add(langDb);
            }

            context.SaveChanges();           
        }

        private void SeedLabels(SnippyDbContext context)
        {
            var listLabels = new List<string>()
            {
                @"bug",
                @"funny",
                @"jquery", 
                @"mysql",
                @"useful", 
                @"web",
                @"geometry",
                @"back-end",
                @"front-end",
                @"games"
            };

            foreach (var label in listLabels)
            {
                var text = label.Trim();
                var labelDb = new Label()
                {
                    Text = text
                };

                context.Labels.Add(labelDb);
            }

            context.SaveChanges();
        }

        private void SeedSnippets(SnippyDbContext context)
        {
            var author1 = context.Users.FirstOrDefault(u => u.UserName == "admin");
            var listSnippets = new List<string>()
            {
                @"Ternary Operator Madness|This is how you DO NOT user ternary operators in C#!|
    bool X = Glob.UserIsAdmin ? true : false;
                |26.10.2015 17:20:33|C#|funny",
                @"Check for a Boolean value in JS|How to check a Boolean value - the wrong but funny way :D|                                                                                          
    var b = true;                                                                                                                             
    if (b.toString().length < 5) {                                             
    //...                                     
                |admin| 22.10.2015 05:30:04|JavaScript|funny",
                @"Reverse a String|Almost not worth having a function for...
                |
    def reverseString(s):
    '''Reverses a string given to it.'''
    return s[::-1]                      
                |admin|26.10.2015 09:35:13|Python|useful"
            };

            var minutes = 20;
            foreach (var snippet in listSnippets)
            {
                var item = snippet.Split('|');
                var title = item[0].Trim();
                var description = item[1].Trim();
                var code = item[2].Trim();
                var authorName = item[3].Trim();
                var author = context.Users.FirstOrDefault(u => u.UserName == authorName);
                var creationTime = item[4].Trim();
                var languageName = item[5].Trim();
                var language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == languageName);
                var labelText = item[6].Trim();

                var snippetDb = new Snippet()
                {
                    Title = title,
                    Description = description,
                    Code = code,
                    Author = author,
                    //CreationTime = DateTime.Parse(creationTime),
                    CreationTime = new DateTime(2015, 10, 26, 17, minutes, 33),
                    Language = language,
                    Labels = new List<Label>()
                    {
                        context.Labels.FirstOrDefault(l=>l.Text == labelText)
                    }
                };

                context.Snippets.Add(snippetDb);
                minutes += 5;
            }

            context.Snippets.Add(new Snippet()
            {
                Title = "Points Around A Circle For GameObject Placement",
                Description = "Determine points around a circle which can be used to place elements around a central point",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                Code = @"private Vector3 DrawCircle(float centerX,                                  "+
                        "                                                                           "+                                                                             
                        "    float centerY,                                                         "+
                        "    float radius,                                                          "+
                        "    float totalPoints,                                                     "+
                        "    float currentPoint)                                                    "+
                        "{                                                                          "+
                        "    float ptRatio = currentPoint / totalPoints;                            "+
                        "    float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius; "+
                        "    float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius; "+
                        "                                                                           "+
                        "    Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);               "+
                        "                                                                           "+
                        "    return panelCenter;                                                    "+
                        "}                                                                          ",
                CreationTime = DateTime.Parse("26.10.2015 20:15:30"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "C#"),
                Labels = new List<Label>()
                {
                     context.Labels.FirstOrDefault(l=>l.Text=="geometry"),
                     context.Labels.FirstOrDefault(l=>l.Text=="games"),
                }
            });

            context.Snippets.Add(new Snippet()
            {
                Title = "forEach. How to break?",
                Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                Code = "var ary = [\"JavaScript\",                                                  "+
                       "    \"Java\",                                                               "+
                       "    \"CoffeeScript\",                                                       "+
                       "     \"TypeScript\"];                                                       "+
                       "                                                                            "+
                       "ary.some(function (value, index, _ary) {                                    "+
                       "     console.log(index + \": \" + value);                                   "+
                       "     return value === \"CoffeeScript\";                                     "+
                       "});                                                                         "+
                       "// output:                                                                  "+
                       "// 0: JavaScript                                                            "+
                       "// 1: Java                                                                  "+
                       "// 2: CoffeeScript                                                          "+
                       "                                                                            "+
                       "ary.every(function(value, index, _ary) {                                    "+
                       "     console.log(index + \": \" + value);                                   "+
                       "     return value.indexOf(\"Script\") > -1;                                 "+
                       "});                                                                         "+
                       "// output:                                                                  "+
                       "// 0: JavaScript                                                            "+
                       "// 1: Java                                                                  ",
                CreationTime = DateTime.Parse("27.10.2015 10:53:20"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "JavaScript"),
                Labels = new List<Label>()
                {
                    context.Labels.FirstOrDefault(l=>l.Text=="jquery"),
                    context.Labels.FirstOrDefault(l=>l.Text=="useful"),
                    context.Labels.FirstOrDefault(l=>l.Text=="web"),
                    context.Labels.FirstOrDefault(l=>l.Text=="front-end")
                }
            });

            context.Snippets.Add(new Snippet()
            {
                Title = "Numbers only in an input field",
                Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                Code = "$(\"#input\").keypress(function(event){                                     "+
                       "     var charCode = (event.which) ? event.which : window.event.keyCode;     "+
                       "     if (charCode <= 13) { return true; }                                   "+
                       "     else {                                                                 "+
                       "             var keyChar = String.fromCharCode(charCode);                   "+
                       "             var regex = new RegExp(\"[0-9,.-]\");                          "+
                       "             return regex.test(keyChar);                                    "+
                       "     }                                                                      "+
                       "});                                                                         "+
                       "                                                                            ",
                CreationTime = DateTime.Parse("20.10.2015 09:00:56"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "JavaScript"),
                Labels = new List<Label>()
                {
                    context.Labels.FirstOrDefault(l=>l.Text=="web"),
                     context.Labels.FirstOrDefault(l=>l.Text=="front-end"),
                }
            });
            context.SaveChanges();

            context.Snippets.Add(new Snippet()
            {
                Title = "Create a link directly in an SQL query",
                Description = "That's how you create links - directly in the SQL!",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                Code = "Code:                                                                       "+                                                                                   
                         "SELECT DISTINCT                                                           "+                                                                                    
                         "      b.Id,                                                               "+                                                                             
                         "      concat('<button type=\"\"button\"\"                                 "+
                         "           onclick=\"\"DeleteContact(', cast(b.Id as char),               "+
                         "          ')\"\">Delete...</button>') as lnkDelete                        "+
                         "FROM tblContact   b                                                       "+                                                                              
                         "WHERE ....                                                                "+                                                                              
                         "                                                                          ",
                CreationTime = DateTime.Parse("30.10.2015 11:20:00"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "SQL"),
                Labels = new List<Label>()
                {
                     context.Labels.FirstOrDefault(l=>l.Text=="bug"),
                     context.Labels.FirstOrDefault(l=>l.Text=="funny"),
                     context.Labels.FirstOrDefault(l=>l.Text=="mysql"),
                }
            });

            context.Snippets.Add(new Snippet()
            {
                Title = "Reverse a String",
                Description = "Almost not worth having a function for...",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                Code = "def reverseString(s):                                                       "+
                       "     \"\"\"Reverses a string given to it.\"\"\"                             "+
                       "     return s[::-1]                                                         ",
                CreationTime = DateTime.Parse("26.10.2015 09:35:13"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "Python"),
                Labels = new List<Label>()
                {
                     context.Labels.FirstOrDefault(l=>l.Text=="useful"),
                   
                }
            });

            context.Snippets.Add(new Snippet()
            {
                Title = "Pure CSS Text Gradients",
                Description = "code describes how to create text gradients using only pure CSS",
                Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                Code = "/* CSS text gradients */                                                    "+                                                                                       
                       "h2[data-text] {                                                             "+                                                                                     
                       "     position: relative;                                                    "+                                                                                        
                       "}                                                                           "+                                                                                      
                       "h2[data-text]::after {                                                      "+                                                                                     
                       "    content: attr(data-text);                                               "+                                                                                        
                       "    z-index: 10;                                                            "+                                                                                        
                       "    color: #e3e3e3;                                                         "+                                                                                        
                       "    position: absolute;                                                     "+                                                                                         
                       "    top: 0;                                                                 "+                                                                                        
                       "    left: 0;                                                                "+                                                                                        
                       "    -webkit-mask-image: -webkit-gradient(linear                             "+
                       "            left top, left bottom,                                          "+
                       "            from(rgba(0,0,0,0)),                                            "+
                       "            color-stop(50%, rgba(0,0,0,1)),                                 "+
                       "             to(rgba(0,0,0,0)));                                            ",
                CreationTime = DateTime.Parse("22.10.2015 19:26:42"),
                Language = context.ProgrammingLanguages.FirstOrDefault(l => l.Name == "CSS"),
                Labels = new List<Label>()
                {
                     context.Labels.FirstOrDefault(l=>l.Text=="web"),
                     context.Labels.FirstOrDefault(l=>l.Text=="front-end"),
                }
            });

            context.SaveChanges();
            
        }

        private void SeedComments(SnippyDbContext context)
        {
            var listComments = new List<string>()
            {
                @"Now that's really funny! I like it! | admin | 30-10-2015-11-50-38 | Ternary Operator Madness",
                @"Here, have my comment! | newUser | 1-11-2015-15-52-42 | Ternary Operator Madness",
                @"I didn't manage to comment first :( | someUser | 2-11-2015-5-30-20 | Ternary Operator Madness",
                @"That's why I love Python - everything is so simple! | newUser | 27-10-2015-15-28-14 | Reverse a String",
                @"I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject | someUser | 29-10-2015-15-08-42 | Points Around A Circle For GameObject Placement",
                @"Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.| admin|3-11-2015-12-56-20 | Numbers only in an input field"
            };

            var hour = 17;
            foreach (var comment in listComments)
            {
                var item = comment.Split('|');
                var content = item[0].Trim();
                var authorName = item[1].Trim();
                var author = context.Users.FirstOrDefault(u => u.UserName == authorName);

                var creationTime = item[2].Trim().ToString();
                var snippetTitle = item[3].Trim();
                var snippet = context.Snippets.FirstOrDefault(s => s.Title == snippetTitle);

                var commentDb = new Comment()
                {
                    Content = content,
                    Author = author,
                    CreationTime = new DateTime(2015, 10, 28, hour, 24, 17),
                    //CreationTime = DateTime.Parse(creationTime),
                    Snippet = snippet
                };

                context.Comments.Add(commentDb);
                hour++;
            }

            context.SaveChanges();
        }
    }
}
