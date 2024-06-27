using System;

namespace LegacyApp;

public class ClientService
{
    private IClientRepository _clientRepository;
    private ICreditLimitService _creditLimitService;

    public ClientService(IClientRepository clientRepository, ICreditLimitService creditService)
    {
        _clientRepository = clientRepository;
        _creditLimitService = creditService;
    }
    
    public Client GetClientById(int clientId)
    {
        return _clientRepository.GetById(clientId);
    }
    
    
    public void SetCreditLimit(User user)
    {
        
        if (user.Client.Type == "VeryImportantClient")
        {
            user.HasCreditLimit = false;
        }
        else if (user.Client.Type == "ImportantClient")
        {
            {
                int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }
        else
        {
            user.HasCreditLimit = true;
            {
                int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }
    }
    
}