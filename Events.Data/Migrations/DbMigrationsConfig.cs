using System.Collections.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Events.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig :
        DbMigrationsConfiguration<ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Seed initial data only if the database is empty

            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUserName = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = adminEmail;
                string adminRole = "Administrator";
                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassword, adminRole);
                CreateSeveralEvents(context);
            }
        }

        private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminUserName, string adminFullName, string adminPassword, string adminRole)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                FullName = adminFullName,
                Email = adminEmail
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            var userCreateResult = userManager.Create(adminUser, adminPassword);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            // Create the "Administrator" role
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // Add the "admin" user to "Administrator" role
            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateSeveralEvents(ApplicationDbContext context)
        {
            // Few upcoming events with / without comments
            context.Events.Add(new Event()
            {
               Title = "Party @ Cacao beach",
               StartDateTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
               Author = context.Users.First(),
            });

            context.Events.Add(new Event()
            {
                Title = "Slavi Trifonov's concert @ arena Armeec",
                StartDateTime = DateTime.Now.Date.AddDays(25).AddHours(11).AddMinutes(30),
                Author = context.Users.First(),
            });

            context.Events.Add(new Event()
            {
                Title = "Sky diving appointment @ Cacao beach",
                StartDateTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                Author = context.Users.First(),
            });

            context.Events.Add(new Event()
            {
                Title = "Christmas @ Home",
                //StartDateTime = new DateTime(DateTime.Now.Year, 12, 25, 9, 0, 0),
                StartDateTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                Author = context.Users.First(),
                Comments = new Collection<Comment>()
                {
                    new Comment(){ Text = "<Anonymous> comment"},
                    new Comment(){ Text = "User comment", Author = context.Users.First()}
                }
            });

            // Few passed events with / without authors and comments
            context.Events.Add(new Event()
            {
                Title = "Passed Event 1 <Anonymous>",
                StartDateTime = DateTime.Now.Date.AddDays(-2).AddHours(10).AddMinutes(30),
                Duration = TimeSpan.FromHours(1.5),
                Comments = new Collection<Comment>()
                {
                    new Comment(){ Text = "<Anonymous> comment"},
                    new Comment(){ Text = "User comment", Author = context.Users.First()}
                }
            });

            context.Events.Add(new Event()
            {
                Title = "Passed Event 2",
                StartDateTime = DateTime.Now.Date.AddDays(-15).AddHours(10).AddMinutes(30),
                Duration = TimeSpan.FromHours(3.5),
                Author = context.Users.First(),
                Comments = new Collection<Comment>()
                {
                    new Comment(){ Text = "<Anonymous> comment"},
                    new Comment(){ Text = "User comment", Author = context.Users.First()}
                }
            });

            context.Events.Add(new Event()
            {
                Title = "Passed Event 3 <Anonymous>",
                StartDateTime = DateTime.Now.Date.AddDays(-9).AddHours(10).AddMinutes(30),
                Duration = TimeSpan.FromHours(2.5),
            });

        }
    }
}
