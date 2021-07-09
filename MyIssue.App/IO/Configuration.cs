using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.App.IO
{
    class Configuration
    {
		public const string emptyConfig =
@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<configuration>
	<applicationPass>{0}</applicationPass>
	<companyName>{1}</companyName>
	<serverAddress>{2}</serverAddress>
	<port>{3}</port>
	<login>{4}</login>
	<pass>{5}</pass>
	<emailAddress>{6}</emailAddress>
	<recipientAddress>{7}</recipientAddress>
	<connectionMethod>{8}</connectionMethod>
	<sslTsl>{9}</sslTsl>
</configuration>";
    }
}
