using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly ClientService _clientService;
        private readonly UserValidator _userValidator;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _clientService = new ClientService(new ClientRepository(), new UserCreditService());
            _userValidator = new UserValidator();
        }

        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!_userValidator.Validate(firstName, lastName, email, dateOfBirth, out int age))
                return false;
            var client = _clientRepository.GetById(clientId);
            

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            _clientService.SetCreditLimit(user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
