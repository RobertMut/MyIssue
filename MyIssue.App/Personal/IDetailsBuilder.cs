using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.App.Personal
{
    public interface IDetailsBuilder
    {
        IDetailsBuilder SetName(string name);
        IDetailsBuilder SetSurname(string surname);
        IDetailsBuilder SetCompany(string company);
        IDetailsBuilder SetPhone(string phone);
        IDetailsBuilder SetEmail(string email);
        PersonalDetails Build();
    }
}
