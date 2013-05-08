using System.Web.Security;
using Leikjavefur.Models;
using WebMatrix.WebData;
using System.Data.Entity;
using Leikjavefur.Contexts;

namespace Leikjavefur.DataRepository
{
    public class InitSecurityDb : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {

            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("test", false) == null)
            {
                membership.CreateUserAndAccount("test", "test");
            }
            bool adminExists = false;
            foreach(var role in roles.GetRolesForUser("test"))
            {
                if (role == "Admin") adminExists = true;
            }
            if (adminExists)
            {
                roles.AddUsersToRoles(new[] { "test" }, new[] { "admin" });
            }

        }
    }
}