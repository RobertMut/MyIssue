using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc.Sender;
using NUnit.Framework;
using System.IO;
using MyIssue.DesktopApp.Model;
using MyIssue.DesktopApp.Model.Builders;

namespace Tests
{
    class SelectorTests
    {
        private ISelector selector;
        private SettingTextBoxes fakeSetting;
        private PersonalDetails fakeDetails;
        [SetUp]
        public void SetUp()
        {
            selector = new Selector();
            fakeSetting = ConfigValuesBuilder.Create()
                    .SetApplicationPass("1234")
                    .SetCompanyName("Lorem ipsum")
                    .SetEmailAddress("root@localhost.com")
                    .SetLogin("root")
                    .SetPass("1234")
                    .SetRecipientAddress("root@localhost.com")
                    .SetSslTsl(false)
                    .SetServerAddress("127.0.0.1")
                    .SetPort("25")
                    .SetImage(Directory.GetFiles(@"C:\Windows\Web\Screen", "*.jpg")[0])
                .Build();
            fakeDetails = PersonalDetailsBuilder.Create()
                    .SetCompany(fakeSetting.CompanyName)
                    .SetEmail(fakeSetting.EmailAddress)
                    .SetName("Lorem")
                    .SetSurname("Ipsum")
                    .SetPhone("3215697312")
                    .SetImage(fakeSetting.Image)
                .Build();
        }
        [Test]
        public void SelectorTest()
        {
            fakeSetting.ConnectionMethod = "False";
            selector.Send(fakeSetting, fakeDetails, "lorem ipsum es..");
        } 

    }
}
