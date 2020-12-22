using System.Collections.Generic;
using System.Threading.Tasks;
using EReceipt.DAL.Entities;

namespace EReceipt.BLL.Interface
{
    public interface IAccountService
    {
        Task<string> SignIn(string email, string password);

        Task<string> SignUpPharmacy(string userName, string email, string password, string role, Pharmacy model);

        Task<string> SignUpPatient(string userName, string email, string password, string role, Patient model);

        Task<string> SignUpDoctor(string userName, string email, string password, string role, Doctor model);

        Task ChangePassword(string id, string currentPassword, string newPassword);
    }
}
