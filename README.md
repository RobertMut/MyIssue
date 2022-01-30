## MyIssue
MyIssue is a solution to help tracking incidents submitted by user. \
User reports their issue by desktop application, which sends data straight to the server. At this point credentials are validated by Identity.API and (if they were valid) Main.API receives request. \
The idea of Server comes from [RFC5321](https://datatracker.ietf.org/doc/html/rfc5321#section-3).\
Whole is based on ITIL methodology, which assumes:
 - Task owner - person who received ticket and became owner of incident
 - Task asignee - person who takes its part resolving incident.
 - Task importance 

\
This repository consist of four parts:
 - [Main.API](./docs/Main.API.md) - provides required data from database,
 - [Identity.API](./docs/Identity.API.md) - used to authenticate user,
 - [Server](./docs/Server.md) - "bridge" between API and Web/DesktopApp, also connects to imap and search for new mails,
 - [Web](./docs/Web.md) - contains panel to create task, create new client and manage tasks,
 - [DesktopAPP](./docs/DesktopAPP.md) - just WPF app to send and crate new task.
 
## Screenshots
#### Web
![Web login](https://i.imgur.com/nzPBquc.png) 
![Home screen](https://i.imgur.com/aAJ3Och.png) 
![New task screen](https://i.imgur.com/guwZKEw.png) 
![User screen](https://i.imgur.com/XlOuxXj.png) 
#### DesktopApp
![Configuration Screen](https://i.imgur.com/yEaPMmj.png) 
![Main view](https://i.imgur.com/QWVlvvC.png) 
#### Server
![Console](https://i.imgur.com/EN2qsDQ.png) 

### Notes
When you are creating task, you should have client already added in database. Company name must be the same as in database.\
Default user:
```
Login: Admin
Password: 1234
```
Default client: MyIssue
