using JCFNMembers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCFNMembers.Domain.Services.Abstract {
    public interface IUserService {

        //ApplicationUser GetUserById(string id);

        //ApplicationUser GetUserByEmail(string email);

        ApplicationUser GetUserProfileById(string id);

        ApplicationUser updateUser(ApplicationUser user, string password);

        void UpdateUser(ApplicationUser user);

        void PasswordRecoveryTokenRequest(string email, string token);


        //IEnumerable<ApplicationUser> GetUsersBySearchKey(ApplicationUser model);

        void updateLastAccessedDate(ApplicationUser user);

        void DeleteUserById(string Id);


        /*********************************************************************************/

        void SendMail(string email, string passcode);

        // void SendConfirmationEmail(ApplicationUser model);
        void SendErrorMessageToAdmin(ApplicationUser model, string errorMessage);


        //  void updateUserConference(ApplicationUser user);
    }
}
