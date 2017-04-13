namespace BookStore.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookStoreContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "LoyalCustomer"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole("LoyalCustomer");
                roleManager.Create(role);
            }

            Author samer = new Author
            {
                FullName = "Samer",
                Bio = "From the very tender and young years, Samer khouzami felt a deep " +
                "unknowing passion for artistry. He had an innate sense, which only few are " +
                "gifted with, towards visualizing effortless beauty and class.Being raised by " +
                "his beauty icon,his mother,Samer understood what timeless beauty means and " +
                "embarked on a passionate journey to become the artist that could give every " +
                "woman that beauty.From a young age,he noticed the effect a beautiful woman had " +
                "entering a room and wanted to understand her secret weapon."
            };

            Book theRaqqaDiaries = new Book
            {
                Title = "The Raqqa Diaries",
                Price = 29.99m,
                ISBN = "9781786330536",
                ImageUrl = "../../Content/Images/Books/theRaqqaDiaries.jpg",
                Description = "Since ISIS occupied Raqqa in eastern Syria, it has become one of the most isolated and fear-ridden cities on earth. BBC was able to make contact with a small activist group, Al-Sharqiya 24, and one of their members agreed to write a personal diary about his experiences. This book tells his story.",
                Language = "BG",
                IssueDate = new DateTime(2017, 03, 09),
                Quantity = 2,
                NumberOfPages = 237,
                Authors = { samer }
            };

            //
            Author juliaSamuel = new Author
            {
                FullName = "Julia Samuel",
                Bio = "Julia is a psychotherapist specialising in grief and worked as a bereavement " +
                "counsellor in the NHS paediatrics department of St Mary's Hospital, Paddington, " +
                "where she pioneered the role of maternity and paediatric psychotherapist. In 1994 " +
                "she worked to help launch and establish Child Bereavement UK (founded as the Child " +
                "Bereavement Trust), and as Founder Patron, continues to play an active role in the charity."
            };

            Book griefWorks = new Book
            {
                Title = "Grief Works",
                Price = 25.99m,
                ISBN = "9781799930536",
                ImageUrl = "../../Content/Images/Books/griefWorks.jpg",
                Description = "Death affects us all. Yet it is still the last taboo in our society, " +
                    "and grief is still profoundly misunderstood...In Grief Works we hear stories from" +
                    " those who have experienced great love and great loss - and survived. " +
                    "Stories that explain how grief unmasks our greatest fears, strips away our layers " +
                    "of protection and reveals our innermost selves. Julia Samuel, a grief psychotherapist, " +
                    "has spent twenty-five years working with the bereaved and understanding the full " +
                    "repercussions of loss. This deeply affecting book is full of psychological insights on " +
                    "how grief, if approached correctly, can heal us. Through elegant, moving stories, " +
                    "we learn how we can stop feeling awkward and uncertain about death, and not shy away " +
                    "from talking honestly with family and friends. This extraordinary book shows us how to " +
                    "live and learn from great loss.",
                Language = "BG",
                IssueDate = new DateTime(2017, 03, 05),
                Quantity = 7,
                NumberOfPages = 325,
                Authors = { juliaSamuel }
            };

            //
            Author arielLevy = new Author
            {
                FullName = "Ariel Levy",
                Bio = "Ariel Levy is a writer of uncompromising honesty, remarkable clarity and " +
                    "surprising humor...I am the better for having read this book' Lena Dunham 'Every " +
                    "deep feeling a human is capable of will be shaken loose by this short, but profound " +
                    "book' David Sedaris 'I wanted what we all want: everything. We want a mate who " +
                    "feels like family and a lover who is exotic, surprising. We want to be youthful " +
                    "adventurers and middle-aged mothers. We want intimacy and autonomy, safety and " +
                    "stimulation, reassurance and novelty, coziness and thrills. But we can't have it all.'"
            };

            Book theRulesDoNotApply = new Book
            {
                Title = "The Rules Do Not Apply",
                Price = 22.00m,
                ISBN = "9780349005294",
                ImageUrl = "../../Content/Images/Books/theRulesDoNotApply.jpg",
                Description = "The new book by New Yorker journalist and author " +
                    "of Female Chauvinist Pigs, Ariel Levy.",
                Language = "BG",
                IssueDate = new DateTime(2017, 03, 16),
                Quantity = 2,
                NumberOfPages = 278,
                Authors = { arielLevy }
            };

            //
            Author kapkaKassabova = new Author
            {
                FullName = "Kapka Kassabova",
                Bio = "When Kapka Kassabova was a child, the borderzone between Bulgaria, Turkey " +
                "and Greece was rumoured to be an easier crossing point into the West than the Berlin " +
                "Wall so it swarmed with soldiers, spies and fugitives. On holidays close to the border " +
                "on the Black Sea coast, she remembers playing on the beach, only miles from where an " +
                "electrified fence bristled, its barbs pointing inwards toward the enemy: the holiday-makers, " +
                "the potential escapees. Today, this densely forested landscape is no longer heavily " +
                "militarised, but it is scarred by its past. In Border, Kapka Kassabova sets out on a " +
                "journey to meet the people of this triple border - Bulgarians, Turks, Greeks, and the " +
                "latest wave of refugees fleeing conflict further afield. She discovers a region that has " +
                "been shaped by the successive forces of history: by its own past migration crises, by " +
                "communism, by two World wars, by the Ottoman Empire, and - older still - by the ancient " +
                "legacy of myths and legends. As Kapka Kassabova explores this enigmatic region in the " +
                "company of border guards and treasure hunters, entrepreneurs and botanists, psychic healers " +
                "and ritual fire-walkers, refugees and smugglers, she traces the physical and psychological " +
                "borders that criss-cross its villages and mountains, and goes in search of the stories that " +
                "will unlock its secrets. Border is a sharply observed portrait of a little-known corner " +
                "of Europe, and a fascinating meditation on the borderlines that exist between countries, " +
                "between cultures, between people, and within each of us."
            };

            Book border = new Book
            {
                Title = "Border",
                Price = 33.00m,
                ISBN = "9788798005294",
                ImageUrl = "../../Content/Images/Books/border.jpg",
                Description = "The new book by New Yorker journalist and author " +
                    "of Female Chauvinist Pigs, Ariel Levy.",
                Language = "BG",
                IssueDate = new DateTime(2017, 03, 16),
                Quantity = 2,
                NumberOfPages = 278,
                Authors = { kapkaKassabova }
            };

            //
            Author shashiTharoor = new Author
            {
                FullName = "Shashi Tharoor",
                Bio = "In the eighteenth century, India's share of the world economy was as large as "+
                "Europe's. By 1947, after two centuries of British rule, it had decreased six-fold. "+
                "Beyond conquest and deception, the Empire blew rebels from cannon, massacred unarmed "+
                "protesters, entrenched institutionalised racism, and caused millions to die from starvation. "+
                "British imperialism justified itself as enlightened despotism for the benefit of the governed, "+
                "but Shashi Tharoor takes on and demolishes this position, demonstrating how every supposed "+
                "imperial 'gift' - from the railways to the rule of law - was designed in Britain's interests "+
                "alone. He goes on to show how Britain's Industrial Revolution was founded on India's "+
                "deindustrialisation, and the destruction of its textile industry. In this bold and "+
                "incisive reassessment of colonialism, Tharoor exposes to devastating effect the inglorious "+
                "reality of Britain's stained Indian legacy."
            };

            Book ingloriousEmpire = new Book
            {
                Title = "Inglorious Empire",
                Price = 45.50m,
                ISBN = "9788798009994",
                ImageUrl = "../../Content/Images/Books/ingloriousEmpire.jpg",
                Description = "Inglorious Empire tells the real story of the British in India - "+
                    "from the arrival of the East India Company to the end of the Raj - revealing "+
                    "how Britain's rise was built upon its plunder of India.",
                Language = "EN",
                IssueDate = new DateTime(2016, 08, 17),
                Quantity = 10,
                NumberOfPages = 350,
                Authors = { shashiTharoor }
            };

            Book ingloriousEmpire2 = new Book
            {
                Title = "Inglorious Empire - Part 2",
                Price = 42.50m,
                ISBN = "9788798009995",
                ImageUrl = "../../Content/Images/Books/ingloriousEmpire.jpg",
                Description = "Inglorious Empire tells the real story of the British in India - " +
                   "from the arrival of the East India Company to the end of the Raj - revealing " +
                   "how Britain's rise was built upon its plunder of India.",
                Language = "EN",
                IssueDate = new DateTime(2016, 12, 22),
                Quantity = 10,
                NumberOfPages = 285,
                Authors = { shashiTharoor }
            };

            //
            Author lissaEvans = new Author
            {
                FullName = "Lissa Evans",
                Bio = "You're called Fidge and you're nearly eleven. You've been hurled into a strange world. "+
                    "You have three companions: two are unbelievably weird and the third is your awful cousin Graham. "+
                    "You have to solve a series of nearly impossible clues. You need to deal with a cruel dictator "+
                    "and three thousand Wimbley Woos (yes, you read that sentence correctly). "+
                    "And the whole situation - the whole, entire thing - is your fault. Wed Wabbit is an "+
                    "adventure story about friendship, danger and the terror of never being able to get back "+
                    "home again. And it's funny. It's seriously funny."
            };

            Book wedWabbit = new Book
            {
                Title = "Wed Wabbit",
                Price = 20.50m,
                ISBN = "9788798009994",
                ImageUrl = "../../Content/Images/Books/wedWabbit.jpg",
                Description = "A hugely funny 'down the rabbit hole' adventure from the author of "+
                "internationally bestselling 'Small Change for Stuart', which was shortlisted for "+
                "both the Carnegie Medal and Costa Award.",
                Language = "EN",
                IssueDate = new DateTime(2016, 02, 10),
                Quantity = 10,
                NumberOfPages = 130,
                Authors = { lissaEvans }
            };

            Book wedWabbit2 = new Book
            {
                Title = "Wed Wabbit 2",
                Price = 20.50m,
                ISBN = "9788799639994",
                ImageUrl = "../../Content/Images/Books/wedWabbit.jpg",
                Description = "A hugely funny 'down the rabbit hole' adventure from the author of " +
                "internationally bestselling 'Small Change for Stuart', which was shortlisted for " +
                "both the Carnegie Medal and Costa Award.",
                Language = "EN",
                IssueDate = new DateTime(2016, 06, 18),
                Quantity = 10,
                NumberOfPages = 148,
                Authors = { lissaEvans }
            };

            Book wedWabbit3 = new Book
            {
                Title = "Wed Wabbit 3",
                Price = 20.50m,
                ISBN = "9788798088894",
                ImageUrl = "../../Content/Images/Books/wedWabbit.jpg",
                Description = "A hugely funny 'down the rabbit hole' adventure from the author of " +
                "internationally bestselling 'Small Change for Stuart', which was shortlisted for " +
                "both the Carnegie Medal and Costa Award.",
                Language = "EN",
                IssueDate = new DateTime(2016, 11, 20),
                Quantity = 10,
                NumberOfPages = 228,
                Authors = { lissaEvans }
            };

            //
            context.Categories.AddOrUpdate(c => c.Name,
                    new Category { Name = "Science and Technology" },
                    new Category { Name = "Health and Fitness" },
                    new Category { Name = "Popular Psychology", Books = { griefWorks } },
                    new Category { Name = "Nature and Environment" },
                    new Category { Name = "Books for Children", Books = { wedWabbit, wedWabbit2, wedWabbit3 } },
                    new Category { Name = "Biography" },
                    new Category { Name = "Humour", Books = { theRulesDoNotApply } },
                    new Category { Name = "Business, Finance and Law" },
                    new Category { Name = "Crafts and Photography" },
                    new Category { Name = "Arts" },
                    new Category { Name = "Sport" },
                    new Category { Name = "Travel" },
                    new Category { Name = "History", Books = { ingloriousEmpire, ingloriousEmpire2 } },
                    new Category { Name = "Best Sellers", Books = { theRulesDoNotApply, griefWorks, border, ingloriousEmpire, ingloriousEmpire2 } },
                    new Category { Name = "Hardback", Books = { theRaqqaDiaries, theRulesDoNotApply, ingloriousEmpire } },
                    new Category { Name = "Politics", Books = { theRaqqaDiaries, border, ingloriousEmpire } }
                );

            context.Promotions.AddOrUpdate(p => p.Name,
                new Promotion()
                {
                    Name = "Books In 'Paperback' from last year",
                    Text = "Some info about...",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(15),
                    Discount = 10.0m,
                    Categories = { new Category
                                    {
                                        Name = "Paperback",
                                        Books = { border, griefWorks, wedWabbit, wedWabbit2, wedWabbit3 }
                                     }}
                },
                new Promotion()
                {
                    Name = "Books from 'Home and Garden' category",
                    Text = "Some info about this promotion...",
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(15),
                    Discount = 5.0m,
                    Categories = { new Category
                                    {
                                        Name = "Home and Garden",
                                        Books = { theRulesDoNotApply }
                                     }}
                }
            );

            context.SaveChanges();
        }
    }
}
